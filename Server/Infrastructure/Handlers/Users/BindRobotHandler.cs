using System.Threading.Tasks;
using Infrastructure.Commands.Users;
using Infrastructure.Services.Robots;
using Infrastructure.Services.Users;

namespace Infrastructure.Handlers.Users
{
    public class BindRobotHandler : ICommandHandler<BindRobot>
    {
        private readonly IRobotService _robotService;
        private readonly IUserService _userService;

        public BindRobotHandler(IRobotService robotService, IUserService userService)
        {
            _robotService = robotService;
            _userService = userService;
        }

        public async Task HandleAsync(BindRobot command)
        {
            //var user = await _userService.GetUser(command.Caller);
            //await _robotService.BindUserAsync(command.ID, user);
        }
    }
}
