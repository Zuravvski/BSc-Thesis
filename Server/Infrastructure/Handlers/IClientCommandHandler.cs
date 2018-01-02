using System.Threading.Tasks;
using Protocol;

namespace Infrastructure.Handlers
{
    public interface IClientCommandHandler<in T> : ICommandHandler where T : ICommand
    {
        Task HandleAsync(T command, string clientIP);
    }
}
