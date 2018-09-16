using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Domain.Robots;

namespace Infrastructure.Services.Robots
{
    public interface IRobotService : IService
    {
        // CRUD
        Task CreateAsync(string ip);
        Task DeleteAsync(string ip);
        Task<Robot> GetRobotAsync(int id);
        Task<Robot> GetRobotAsync(string ip);
        Task<IEnumerable<Robot>> BrowseAsync();
    }
}
