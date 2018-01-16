using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using UnityEngine;

public class CubeScript : MonoBehaviour
{

    public float MovingSpeed = 1;

    private volatile int movement = 0;
    private TcpListener _server;
    private TcpClient _clientSocket;

	// Use this for initialization
	void Start () {	
        _server = new TcpListener(IPAddress.Parse("127.0.0.1"), 8000);
	    print("Started accepting clients");
        _server.Start();
        _server.BeginAcceptTcpClient(SocketAccepted, null);
    }

    private void SocketAccepted(IAsyncResult ar)
    {
        _clientSocket = _server.EndAcceptTcpClient(ar);
        if (_clientSocket.Connected)
        {
            print("Client connected");
            ReadLoop();
        }
        else
        {
            print("Could not accept the client");
        }
    }

    private void ReadLoop()
    {
        if (_clientSocket.Connected)
        {
            byte[] buffer = new byte[128];
            _clientSocket.GetStream().BeginRead(buffer, 0, buffer.Length, FinishReading, buffer);
        }
    }

    private void FinishReading(IAsyncResult ar)
    {
        if (_clientSocket.Connected)
        {
            byte[] receiveBuffer = (byte[]) ar.AsyncState;
            int bytesRead = _clientSocket.GetStream().EndRead(ar);
            Array.Resize(ref receiveBuffer, bytesRead);
            string line = Encoding.ASCII.GetString(receiveBuffer);
            move(line);
            ReadLoop();
        }
    }

    private void move(string command)
    {
        print("Command: " + command);
        if (!string.IsNullOrEmpty(command) && command.Length == 8)
        {
            string moveCommand = command.Replace("[", "").Replace("]", "");
            string leftMotor = moveCommand.Substring(2, 2);
            print("Left motor: " + leftMotor);

            movement = int.Parse(leftMotor);

            //string rightMotor = moveCommand.Substring(2, moveCommand.Length);
            //print("Right motor: " + rightMotor);



        }

    }

    // Update is called once per frame
    void Update ()
	{
        //transform.Translate(MovingSpeed * Input.GetAxis("Horizontal") * Time.deltaTime,
        //                       0, 
        //                       MovingSpeed * Input.GetAxis("Vertical") * Time.deltaTime);
        transform.Translate(MovingSpeed * movement * Time.deltaTime,
                        0,
                        MovingSpeed * movement  * Time.deltaTime);
	    movement = 0;
	    //print(Input.GetAxis("Horizontal"));
	}


}
