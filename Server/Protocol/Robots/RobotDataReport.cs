using Protocol.Enums;
using Protocol.Utils;

namespace Protocol.Robots
{
    /// <summary>
    /// The request containing a given robot's specific information
    /// </summary>
    public sealed class RobotDataReport : IResponse
    {
        /// <summary>
        /// Robot's ID
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        /// LED state
        /// </summary>
        public ERobotLED LED { get; set; }

        /// <summary>
        /// Left motor's value. Range: [-127, 128]
        /// </summary>
        public int LeftMotor { get; set; }

        /// <summary>
        /// Right motor's value. Range: [-127, 128]
        /// </summary>
        public int RightMotor { get; set; }

        /// <summary>
        /// Robot's position
        /// </summary>
        public Position Position { get; set; }

        /// <summary>
        /// Robot's battery voltage
        /// </summary>
        public int Battery { get; set; }
    }
}
