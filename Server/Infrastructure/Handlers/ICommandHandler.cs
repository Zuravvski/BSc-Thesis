using System.Threading.Tasks;
using Infrastructure.Commands;

namespace Infrastructure.Handlers
{
    public interface ICommandHandler<in T> where T : ICommand
    {
        Task HandleAsync(T command);
    }
}
