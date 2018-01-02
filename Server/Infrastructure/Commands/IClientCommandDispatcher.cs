using System.Threading.Tasks;
using Protocol;

namespace Infrastructure.Commands
{
    public interface IClientCommandDispatcher : ICommandDispatcher
    {
        Task DispatchAsync<T>(T command) where T : ICommand;
    }
}
