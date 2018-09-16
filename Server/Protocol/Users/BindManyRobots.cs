using System.Collections.Generic;

namespace Protocol.Users
{
    /// <summary>
    /// The command used for binding many robot's at the same time
    /// </summary>
    public sealed class BindManyRobots : ICommand
    {
        /// <summary>
        /// Identifiers of robots to be bound
        /// </summary>
        public List<int> robotIDs { get; set; }
    }
}
