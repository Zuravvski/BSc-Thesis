using System.Threading.Tasks;
using Infrastructure.Commands;
using Infrastructure.Commands.Camera;
using Protocol;

namespace Infrastructure.Services.Camera
{
    public interface ICameraService : IService
    {
        void Start();
        Task RequestAsync(ICameraCommand command);
        void Stop();
    }
}
