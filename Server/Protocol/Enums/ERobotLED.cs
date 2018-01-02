using System.ComponentModel;

namespace Protocol.Enums
{
    public enum ERobotLED
    {
        [Description("00")]
        DISABLED,
        [Description("01")]
        ONLY_RED,
        [Description("02")]
        ONLY_GREEN,
        [Description("03")]
        BOTH_ENABLED
    }
}
