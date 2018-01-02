using System.Collections.Generic;

namespace Protocol.Robots
{
    public class DriveMany : ICommand
    {
        public IEnumerable<Drive> DriveCommands { get; set; }
    }
}
