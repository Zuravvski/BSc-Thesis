using System.Collections.Generic;

namespace Protocol.Camera
{
    /// <summary>
    /// The command used to request a position of many robots at once
    /// </summary>
    public sealed class RequestManyPositions : ICommand
    {
        /// <summary>
        /// List of identifiers of robots of interest
        /// </summary>
        public List<int> IDs { get; set; }
    }
}
