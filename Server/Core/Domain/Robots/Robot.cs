using System;
using System.Diagnostics;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using Core.Domain.Robots.Enums;

namespace Core.Domain.Robots
{
    /// <summary>
    /// Represents Pololu3PI robot
    /// </summary>
    public class Robot
    {
        public string IPAddress { get; }
        public int Port { get; }
        public uint ID { get; }
        public ERobotLED LEDS { get; set; }
        public int LeftMotorSpeed { get; protected set; }
        public int RightMotorSpeed { get; protected set; }
        public Position Position { get; protected set; }
        public uint Battery { get; protected set; }
        public string BoundTo { get; protected set; }
        public bool Connected => _socket != null && _socket.Connected;

        private TcpClient _socket;
        private NetworkStream _stream;

        public Robot(string ip, int port = 8000)
        {
            IPAddress = ip;
            Port = port;
            ID = uint.Parse(ip.Substring(ip.LastIndexOf(".", StringComparison.Ordinal) + 1));
            BoundTo = string.Empty;
            _socket = new TcpClient();
        }

        public async Task ConnectAsync()
        {
            if (Connected) return;
            try
            {
                await _socket.ConnectAsync(IPAddress, Port);
                _stream = _socket.GetStream();
                ReadAsync();
            }
            catch (Exception e)
            {
                Debug.WriteLine($"Could not connect to the robot with ID: {ID}");
            }

        }

        public void Disconnect()
        {
            if (!Connected) return;
            _socket.Close();
            _stream.Close();
        }

        public void Bind(string userIP)
        {
            if (BoundTo.Length > 0)
            {
                return;
            }
            BoundTo = userIP;
        }

        public void Unbind()
        {
            BoundTo = string.Empty;
        }

        public async Task WriteAsync(byte[] data)
        {
            await _stream.WriteAsync(data, 0, data.Length);
        }

        private async void ReadAsync()
        {
            while (Connected)
            {
                try
                {
                    var buffer = new byte[1024];
                    var bytesRead = await _stream.ReadAsync(buffer, 0, buffer.Length);
                    Array.Resize(ref buffer, bytesRead);
                    var stringResponse = Encoding.ASCII.GetString(buffer);
                    Debug.WriteLine($"Response from robot: {stringResponse}");
                }
                catch
                {
                    Debug.WriteLine($"Fetching data from robot {ID} failed");
                }
            }
        }
    }
}
