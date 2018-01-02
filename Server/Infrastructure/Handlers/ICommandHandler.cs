using System.Threading.Tasks;
using Protocol;

namespace Infrastructure.Handlers
{
    public interface ICommandHandler<in T> where T : ICommand
    {
        Task HandleAsync(T command, string clientIP);
    }
}
