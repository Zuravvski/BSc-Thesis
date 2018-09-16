using System;
using System.Globalization;
using Core.Domain.Robots.Enums;

namespace Core.Domain.Robots
{
    public class Position
    {
        public double X { get; set; }
        public double Y { get; set; }
        public double Theta { get; set; }
        public EPositionState Identified { get; set; }

        public override string ToString()
        {
            return Convert.ToString(X, CultureInfo.InvariantCulture) +
                   Convert.ToString(Y, CultureInfo.InvariantCulture) +
                   Convert.ToString(Theta, CultureInfo.InvariantCulture);
        }
    }
}
