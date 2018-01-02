using System.Threading.Tasks;
using Infrastructure.Repositories;
using Infrastructure.Services.Robots;
using Protocol.Users;

namespace Infrastructure.Handlers.Users
{
    public class UnbindRobotHandler : ICommandHandler<UnbindRobot>
    {
        private readonly IRobotService _robotService;

        public UnbindRobotHandler(IRobotService robotService)
        {
            _robotService = robotService;
        }

        public async Task HandleAsync(UnbindRobot command, string clientIP)
        {
            var robot = await _robotService.GetRobotAsync(command.ID);
            robot?.Unbind();
        }
    }
}
