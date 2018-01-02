using System.Threading.Tasks;
using Infrastructure.Services.Robots;
using Protocol.Users;

namespace Infrastructure.Handlers.Users
{
    public class BindRobotHandler : IClientCommandHandler<BindRobot>
    {
        private readonly IRobotService _robotService;

        public BindRobotHandler(IRobotService robotService)
        {
            _robotService = robotService;
        }

        public async Task HandleAsync(BindRobot command, string clientIP)
        {
            var robot = await _robotService.GetRobotAsync(command.ID);
            robot?.Bind(clientIP);
        }
    }
}
