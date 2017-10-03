using System.ComponentModel;

namespace Infrastructure.Network.Packets.Requests
{
    public enum RequestType
    {
        [Description("00")]
        UNBIND_ROBOT,
        [Description("01")]
        BIND_ROBOT,
        [Description("02")]
        DRIVE,
        [Description("03")]
        SUBSCRIBE_POSITION,
        [Description("04")]
        UNSUBSCRIBE_POSITION,
        [Description("05")]
        GET_POSITION,
    }
}
