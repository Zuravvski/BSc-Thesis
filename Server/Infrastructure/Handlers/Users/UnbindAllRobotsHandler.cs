using System.Linq;
using System.Threading.Tasks;
using Infrastructure.Services.Robots;
using Protocol.Users;

namespace Infrastructure.Handlers.Users
{
    public class UnbindAllRobotsHandler : IClientCommandHandler<UnbindAllRobots>
    {
        private readonly IRobotService _robotService;

        public UnbindAllRobotsHandler(IRobotService robotService)
        {
            _robotService = robotService;
        }

        public async Task HandleAsync(UnbindAllRobots command, string clientIP)
        {
            var robots = await _robotService.BrowseAsync();
            var boundRobots = robots.Where(robot => robot.BoundTo == clientIP);
            foreach (var robot in boundRobots)
            {
                robot.Unbind();
            }
        }
    }
}
