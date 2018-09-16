using System.Threading.Tasks;
using Infrastructure.Commands.Camera;

namespace Infrastructure.Commands
{
    public interface ICameraCommandDispatcher : ICommandDispatcher
    {
        Task DispatchAsync<T>(T command) where T : ICameraCommand;
    }
}
