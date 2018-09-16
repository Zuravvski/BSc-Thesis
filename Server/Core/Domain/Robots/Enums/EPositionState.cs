using System.ComponentModel;

namespace Core.Domain.Robots.Enums
{
    public enum EPositionState
    {
        [Description("Not Identified")]
        NOT_IDENTIFIED, 
        [Description("Pending Information")]
        PENDING_INFORMATION,
        [Description("Identified")]
        IDENTIFIED
    }
}
