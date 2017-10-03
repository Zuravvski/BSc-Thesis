using System.Threading.Tasks;
using Infrastructure.Commands;

namespace Infrastructure.Network.Packets.Requests
{
    public class PacketManager
    {
        private readonly ICommandDispatcher _commandDispatcher;

        public PacketManager(ICommandDispatcher commandDispatcher)
        {
            _commandDispatcher = commandDispatcher;
        }

        public async Task DispatchAsync<T>(T command) where T : ICommand
        {
            await _commandDispatcher.DispatchAsync(command);
        }

        public async Task CreateAndDispatch(Packet packet)
        {
            // Create request
            // Dispatch command if valid
        }
    }
}
