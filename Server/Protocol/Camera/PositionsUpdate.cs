using System.Collections.Generic;


namespace Protocol.Camera
{
    /// <summary>
    /// Response containing updated positions of robots with given IDs
    /// </summary>
    public sealed class PositionsUpdate : IResponse
    {
        /// <summary>
        /// List of position
        /// </summary>
        public List<PositionUpdate> Positions { get; set; }
    }
}
