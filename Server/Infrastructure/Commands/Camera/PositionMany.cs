using System.Collections.Generic;

namespace Infrastructure.Commands.Camera
{
    public class PositionMany : ICameraCommand
    {
        private IEnumerable<Position> Positions { get; set; }
    }
}
