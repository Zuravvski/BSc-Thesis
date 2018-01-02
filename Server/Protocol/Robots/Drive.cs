using Protocol.Enums;

namespace Protocol.Robots
{
    public class Drive : ICommand
    {
        public uint RobotID { get; set; }
        public int LeftMotor { get; set; }
        public int RightMotor { get; set; }
        public ERobotLED LEDs { get; set; }
    }
}
