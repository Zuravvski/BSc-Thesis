using System;
using System.Globalization;
using System.Text.RegularExpressions;

public class RobotFrameParser : IFrameParser
{
    private static readonly Regex FRAME_REGEX = new Regex(@"\[([0-9a-fA-F]){6}\]");

    public ICommand Parse(string frame)
    {
        if (!IsValid(frame))
        {
            throw new ArgumentException("Invalid frame!");
        }

        var led = int.Parse(frame.Substring(1, 2), NumberStyles.AllowHexSpecifier, CultureInfo.InvariantCulture);
        var left = int.Parse(frame.Substring(3, 2), NumberStyles.AllowHexSpecifier, CultureInfo.InvariantCulture);
        var right = int.Parse(frame.Substring(5, 2), NumberStyles.AllowHexSpecifier, CultureInfo.InvariantCulture);

        if (left > 0xFF || right > 0xFF)
        {
            throw new ArgumentException("Invalid speed value!");
        }

        var leftMotorDirection = 1;
        var rightMotorDirection = 1;

        if (left > 0x7F)
        {
            left = 0xFF - left + 1;
            leftMotorDirection *= -1;
        }

        if (right > 0x7F)
        {
            right = 0xFF - right + 1;
            rightMotorDirection *= -1;
        }

        return new MoveRobot()
        {
            LED = (LEDStateE) led,
            LeftMotor = MapValue(left, 0, 0x7F, 0, 1.0f) * leftMotorDirection,
            RightMotor = MapValue(right, 0, 0x7F, 0, 1.0f) * rightMotorDirection
        };
    }

    private static bool IsValid(string frame)
    {
        return !string.IsNullOrEmpty(frame) && FRAME_REGEX.IsMatch(frame);
    }

    private static double MapValue(double value, double fromSource, double toSource, double fromTarget, double toTarget)
    {
        return (value - fromSource) / (toSource - fromSource) * (toTarget - fromTarget) + fromTarget;
    }
}
