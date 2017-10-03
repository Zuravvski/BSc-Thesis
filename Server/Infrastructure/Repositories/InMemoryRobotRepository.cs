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

        public async Task<Robot> GetRobotAsync(uint id)
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

        public async Task DeleteRobotAsync(uint id)
        {
            var robot = await GetRobotAsync(id);
            _robots.Remove(robot);
            await Task.CompletedTask;
        }
    }
}
