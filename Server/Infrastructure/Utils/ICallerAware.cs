using Infrastructure.Network.Sockets;

namespace Infrastructure.Utils
{
    public interface ICallerAware
    {
        Client Caller { get; set; }
    }
}
