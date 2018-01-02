using System.Threading.Tasks;
using Infrastructure.Commands;

namespace Infrastructure.Handlers
{
    public interface ICameraCommandHandler<in T> : ICommandHandler where T : ICameraCommand
    {
        Task HandleAsync(T command);
    }
}
