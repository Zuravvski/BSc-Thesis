using System.Threading.Tasks;

namespace Infrastructure.Commands
{
    public interface ICameraCommandDispatcher : ICommandDispatcher
    {
        Task DispatchAsync<T>(T command) where T : ICameraCommand;
    }
}
