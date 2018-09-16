using System.Collections.Generic;

namespace Protocol.Robots
{
    /// <summary>
    /// The respose containing available robots IDs
    /// </summary>
    public sealed class AvailableRobots : IResponse
    {
        public List<int> IDs { get; set; }
    }
}
