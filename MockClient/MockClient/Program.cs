using System;
using System.Net.Sockets;
using System.Text;
using Newtonsoft.Json;
using Protocol;
using Protocol.Enums;
using Protocol.Robots;
using Protocol.Users;

namespace MockClient
{
    public class Program
    {
        static void Main(string[] args)
        {
            var client = new TcpClient("127.0.0.1", 50131);
            var settings = new JsonSerializerSettings()
            {
                TypeNameHandling = TypeNameHandling.All,
                //Formatting = Formatting.Indented,
            };
            var read = Console.ReadLine();
            while (!read.ToLower().Equals("q"))
            {
                ICommand command = null;

                switch (read.ToLower())
                {
                    case "drive":
                        command = Drive();
                        break;

                    case "bind":
                        command = Bind();
                        break;

                    case "unbind":
                        command = Unbind();
                        break;

                    case "browse":
                        command = new BrowseAvailableRobots();
                        break;
                }

                if (command != null)
                {
                    var json = JsonConvert.SerializeObject(command, settings);
                    Console.WriteLine(json);
                    var buffer = Encoding.ASCII.GetBytes(json);
                    client.GetStream().Write(buffer, 0, buffer.Length);
                }

                read = Console.ReadLine();
            }
        }

        private static ICommand Drive()
        {
            var drive = new Drive()
            {
                RobotID = 1,
                LEDs = ERobotLED.ONLY_GREEN,
                LeftMotor = 0xF0,
                RightMotor = 0
            };
            return drive;
        }

        private static ICommand Bind()
        {
            var bind = new BindRobot() {ID = 1};
            return bind;
        }

        private static ICommand Unbind()
        {
            var unbind = new UnbindRobot() {ID = 1};
            return unbind;
        }
    }
}
