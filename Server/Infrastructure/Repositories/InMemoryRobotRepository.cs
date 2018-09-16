using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Domain.Robots;
using Core.Repositories;

namespace Infrastructure.Repositories
{
    public class InMemoryRobotRepository : IRobotRepository
    {
        private readonly ISet<Robot> _robots = new HashSet<Robot>();

        public async Task<Robot> GetRobotAsync(string ip)
        {
            return await Task.FromResult(_robots.FirstOrDefault(robot => robot.IPAddress.Equals(ip)));
        }

        public async Task<Robot> GetRobotAsync(int id)
        {
            return await Task.FromResult(_robots.FirstOrDefault(robot => robot.ID == id));
        }

        public async Task<IEnumerable<Robot>> GetAllRobotsAsync()
        {
            return await Task.FromResult(_robots);
        }

        public async Task CreateRobotAsync(Robot robot)
        {
            _robots.Add(robot);
            await Task.CompletedTask;
        }

        public async Task DeleteRobotAsync(string ip)
        {
            var robot = await GetRobotAsync(ip);
            _robots.Remove(robot);
            await Task.CompletedTask;
        }
    }
}
