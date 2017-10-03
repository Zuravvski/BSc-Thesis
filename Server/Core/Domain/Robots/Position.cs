using System;
using System.Globalization;

namespace Core.Domain.Robots
{
    public struct Position
    {
        double X;
        double Y;
        double Angle;

        public override string ToString()
        {
            return Convert.ToString(X, CultureInfo.InvariantCulture) +
                   Convert.ToString(Y, CultureInfo.InvariantCulture) +
                   Convert.ToString(Angle, CultureInfo.InvariantCulture);
        }
    }
}
