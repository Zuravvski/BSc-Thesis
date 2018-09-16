using Protocol.Enums;

namespace Protocol.Robots
{
    /// <summary>
    /// The command used for controlling a given robot
    /// </summary>
    public sealed class Drive : ICommand
    {
        /// <summary>
        /// Robot's ID
        /// </summary>
        public int RobotID { get; set; }

        /// <summary>
        /// Left motor's value. Valid range [-127, 128]
        /// </summary>
        public int LeftMotor { get; set; }

        /// <summary>
        /// Right motor's value. Valid range [-127, 128]
        /// </summary>
        public int RightMotor { get; set; }

        /// <summary>
        /// LED state
        /// </summary>
        public ERobotLED LED { get; set; }
    }
}
