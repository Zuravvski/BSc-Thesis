using System;
using Core.Domain.Robots.Enums;
using Infrastructure.Extensions;
using Infrastructure.Network.Sockets;

namespace Infrastructure.Commands.Robots
{
    public class Drive : ICommand
    {
        public uint RobotID { get; set; }
        public int LeftMotor { get; set; }
        public int RightMotor { get; set; }
        public ERobotLED LEDs { get; set; }

        public Client Caller { get; set; }

        public void LoadPayload(string payload)
        {
            if (payload.Empty() || payload.Length % 8 != 0)
                throw new ArgumentException();

            RobotID = uint.Parse(payload.Substring(0, 2));
            LeftMotor = int.Parse(payload.Substring(2, 2));
            RightMotor = int.Parse(payload.Substring(4, 2));
            LEDs = (ERobotLED)int.Parse(payload.Substring(6));
        }
    }
}
