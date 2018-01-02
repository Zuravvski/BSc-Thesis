using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Infrastructure.Services.Robots;
using Protocol.Robots;

namespace Infrastructure.Handlers.Robots
{
    public class BrowseAvailableRobotsHandler : ICommandHandler<BrowseAvailableRobots>
    {
        private readonly IRobotService _robotService;

        public BrowseAvailableRobotsHandler(IRobotService robotService)
        {
            _robotService = robotService;
        }

        public async Task HandleAsync(BrowseAvailableRobots command, string clientIP)
        {
            var robots = await _robotService.BrowseAsync();
            var availableRobots = robots.Where(robot => robot.BoundTo == string.Empty && robot.Connected);

            // Print out for now
            Debug.WriteLine("Available robots:"); 
            foreach (var robot in availableRobots)
            {
                Debug.WriteLine($"IP: {robot.IPAddress} ID: {robot.ID}");
            }
        }
    }
}
