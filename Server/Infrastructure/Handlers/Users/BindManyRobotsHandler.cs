using System.Linq;
using System.Threading.Tasks;
using Infrastructure.Repositories;
using Infrastructure.Services.Robots;
using Protocol.Users;

namespace Infrastructure.Handlers.Users
{
    public class BindManyRobotsHandler : ICommandHandler<BindManyRobots>
    {
        private readonly IRobotService _robotService;

        public BindManyRobotsHandler(IRobotService robotService)
        {
            _robotService = robotService;
        }

        public async Task HandleAsync(BindManyRobots command, string clientIP)
        {
            var robots = await _robotService.BrowseAsync();
            var robotsToBind = robots.Where(robot => command.robotIDs.Contains(robot.ID));
            foreach (var robot in robotsToBind)
            {
                robot.Bind(clientIP);
            }
        }
    }
}
