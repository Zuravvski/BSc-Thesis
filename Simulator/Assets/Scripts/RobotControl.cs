using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using UnityEngine;
using Random = System.Random;

public class RobotControl : MonoBehaviour
{
    #region Public interface variables

    public float maxTorque = 300;
    public float maxAngle = 35;
    private Material _redLED;
    private Material _greenLED;
    public GameObject wheelShape;

    #endregion

    #region Network related variables

    private TcpClient _client;
    private NetworkStream _stream;
    private IFrameParser _frameParser;
    private bool _isReading;
    private bool _shouldBeDisposed;
    private readonly Queue<ICommand> _commandQueue = new Queue<ICommand>(10);

    #endregion

    #region Constants

    private static readonly Color OFF_RED_COLOR = new Color(0.427f, 0.0117f, 0.0117f);
    private static readonly Color OFF_GREEN_COLOR = new Color(0.0117f, 0.427f, 0.0117f);
    private static readonly string RED_LED_MATERIAL = "RLED_DEFAULT";
    private static readonly string GREEN_LED_MATERIAL = "GLED_DEFAULT";
    private static readonly int NUMBER_OF_SENSORS = 5;
    private static readonly int DEFAULT_BATTERY_LEVEL = 4800;
    private static readonly RobotStatusE DEFAULT_ROBOT_STATUS = RobotStatusE.CONNECTED;

    #endregion

    #region Properties

    public bool Connected
    {
        get
        {
            return _client != null &&
                   _stream != null &&
                   _client.Connected;
        }
    }

    public RobotStatusE Status { get; set; }
    public int Battery { get; set; }
    public List<uint> Sensors { get; set; }

    public WheelCollider[] Wheels { get; private set; }

    #endregion

    public void SetClient(TcpClient client)
    {
        if (_client != null)
        {
            return;
        }
        _client = client;
        _stream = _client.GetStream();
        _frameParser = new RobotFrameParser();
    }

    private void Start()
	{
	    Wheels = GetComponentsInChildren<WheelCollider>();
	    _redLED = FindMaterial(RED_LED_MATERIAL);
	    _greenLED = FindMaterial(GREEN_LED_MATERIAL);

	    Status = DEFAULT_ROBOT_STATUS;
	    Battery = DEFAULT_BATTERY_LEVEL;
        Sensors = new List<uint>(NUMBER_OF_SENSORS) { 0, 0, 0, 0, 0 };

	    foreach (var wheel in Wheels)
	    {
	        if (wheelShape == null) continue;
	        var ws = Instantiate (wheelShape);
	        ws.transform.parent = wheel.transform;
	    }
	    _isReading = false;
    }

    private void Update()
	{
        ReadClientRequest();
	    SendRobotFrame();
        HandleDisconnection();

	    if (_commandQueue.Count > 0)
	    {
	        var command = _commandQueue.Dequeue();
            command.Execute(this, _client);
	    }
    }

    public void ControlLEDs(LEDStateE state)
    {
        switch (state)
        {
            case LEDStateE.OFF:
                _redLED.color = OFF_RED_COLOR;
                _greenLED.color = OFF_GREEN_COLOR;
                break;

            case LEDStateE.RED_ON:
                _redLED.color = Color.red;
                _greenLED.color = OFF_GREEN_COLOR;
                break;

            case LEDStateE.GREEN_ON:
                _redLED.color = OFF_RED_COLOR;
                _greenLED.color = Color.green;
                break;

            case LEDStateE.BOTH_ON:
                _redLED.color = Color.red;
                _greenLED.color = Color.green;
                break;
        }
    }

    public void Move(double leftMotor, double rightMotor)
    {
        var direction = leftMotor < 0 || rightMotor < 0 ? -1 : 1;
        var angle = (float) (leftMotor - rightMotor) * maxAngle * direction;

        foreach (var wheel in Wheels)
        {
            if (wheel.transform.localPosition.z > 0)
                wheel.steerAngle = angle;

            if (wheel.transform.localPosition.x > 0)
            {
                wheel.motorTorque = (float) rightMotor * maxTorque;
            }

            if (wheel.transform.localPosition.x < 0)
            {
                wheel.motorTorque = (float) leftMotor * maxTorque;
            }

            wheel.brakeTorque = leftMotor != 0.0f || rightMotor != 0.0f ? 0 : 300;
            VisualizeWheelMovement(wheel);
        }
    }

    private Material FindMaterial(string name)
    {
        var renderers = GetComponentsInChildren<Renderer>();

        return renderers.SelectMany(r => r.materials)
                        .FirstOrDefault(material => material.name.Contains(name));
    }

    private void HandleDisconnection()
    {
        if (!_shouldBeDisposed && _client.Connected) return;
        var world = GameObject.FindWithTag("World");
        var server = world.GetComponent<SimulatorServer>();
        server.HandleDisconnection(gameObject, _client);
    }

    private void ReadClientRequest()
    {
        if (!Connected || _isReading) return;
        var buffer = new byte[128];
        _stream.BeginRead(buffer, 0, buffer.Length, ReadFrame, buffer);
        _isReading = true;
    }

    private void SendRobotFrame()
    {
        if (!Connected && _isReading) return;

        var status = ((int) Status).ToString("X2");
        var battery = ConvertToLittleEndian(Battery.ToString("X4"));
        var sensors = "";

        foreach (var sensor in Sensors)
        {
            sensors += ConvertToLittleEndian(sensor.ToString("X4"));
        }

        var frame = "[" + status + battery + sensors + "]";
        var buffer = Encoding.ASCII.GetBytes(frame);
        _stream.BeginWrite(buffer, 0, buffer.Length, null, null);
        var randomizer = new Random(Battery);
        Battery = randomizer.Next(4300, 5000);
    }

    private void ReadFrame(IAsyncResult result)
    {
        var bytesRead = _stream.EndRead(result);
        _isReading = false;

        if (bytesRead == 0)
        {
            _shouldBeDisposed = true;
            return;
        }
        var buffer = result.AsyncState as byte[];
        Array.Resize(ref buffer, bytesRead);
        var frame = Encoding.ASCII.GetString(buffer);

        try
        {
            var command = _frameParser.Parse(frame);
            if (command != null)
            {
                _commandQueue.Enqueue(command);
            }
        }
        catch (Exception e)
        {
            print("Could not parse the command: " + e);
        }
    }

    private string ConvertToLittleEndian(string value)
    {
        if (value.Length != 4) return string.Empty;
        var firstCompliment = value.Substring(0, 2);
        var secondCompliment = value.Substring(2, 2);
        return secondCompliment + firstCompliment;
    }

    private void VisualizeWheelMovement(WheelCollider wheel)
    {
        if (!wheelShape)
            return;
       
        Quaternion q;
        Vector3 p;
        wheel.GetWorldPose(out p, out q);

        var shapeTransform = wheel.transform.GetChild(0);
        shapeTransform.position = p;
        shapeTransform.rotation = q;
    }
}
