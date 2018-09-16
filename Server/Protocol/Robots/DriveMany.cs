using System.Collections.Generic;

namespace Protocol.Robots
{
    /// <summary>
    /// The command used for controlling many robot's at the same time
    /// </summary>
    public sealed class DriveMany : ICommand
    {
        /// <summary>
        /// List of Drive commands
        /// </summary>
        public IEnumerable<Drive> DriveCommands { get; set; }
    }
}
