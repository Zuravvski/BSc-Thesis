using System.Collections.Generic;

namespace Protocol.Users
{
    public class BindManyRobots : ICommand
    {
        public ISet<uint> robotIDs { get; set; }
    }
}
