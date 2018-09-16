using Protocol.Utils;

namespace Protocol.Camera
{
    /// <summary>
    /// Container class describing position of a robot with given ID
    /// </summary>
    public sealed class PositionUpdate : IResponse
    {
        /// <summary>
        /// Robot's ID
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        /// Robots position
        /// </summary>
        public Position Position { get; set; }
    }
}
