using Core.Domain.Robots;
using Core.Domain.Robots.Enums;

namespace Infrastructure.DTO
{
    public class RobotDTO
    {
        public ERobotLED LEDS { get; set; }
        public int LeftMotorSpeed { get; protected set; }
        public int RightMotorSpeed { get; protected set; }
        public Position Position { get; protected set; }
        public uint Battery { get; protected set; }
    }
}
