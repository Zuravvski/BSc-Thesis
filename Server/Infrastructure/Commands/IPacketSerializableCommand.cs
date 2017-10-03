using Infrastructure.Utils;

namespace Infrastructure.Commands
{
    public interface IPacketSerializableCommand : ICallerAware
    {
        void LoadPayload(string payload);
    }
}
