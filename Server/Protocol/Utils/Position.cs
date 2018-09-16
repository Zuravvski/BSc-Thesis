using System;
using System.Globalization;

namespace Protocol.Utils
{
    /// <summary>
    /// Container class describing position of an object in 3D space
    /// </summary>
    public sealed class Position
    {
        /// <summary>
        /// Object's X-axis position
        /// </summary>
        public double X { get; set; }

        /// <summary>
        /// Object's Y-axis position
        /// </summary>
        public double Y { get; set; }

        /// <summary>
        /// Object's rotation in Z-axis
        /// </summary>
        public double Theta { get; set; }

        public override string ToString()
        {
            return Convert.ToString(X, CultureInfo.InvariantCulture) +
                   Convert.ToString(Y, CultureInfo.InvariantCulture) +
                   Convert.ToString(Theta, CultureInfo.InvariantCulture);
        }
    }
}
