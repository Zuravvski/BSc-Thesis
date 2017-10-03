using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Domain.Robots;

namespace Core.Repositories
{
    public interface IRobotRepository : IRepository
    {
        Task<Robot> GetRobotAsync(uint id);
        Task<IEnumerable<Robot>> GetAllRobotsAsync();
        Task CreateRobotAsync(Robot robot);
        Task DeleteRobotAsync(uint id);
    }
}
