using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Domain.Users;
using Infrastructure.DTO;

namespace Infrastructure.Services.Robots
{
    public interface IRobotService : IService
    {
        Task CreateAsync(uint robotID);
        Task DeleteAsync(uint robotID);
        Task BindUserAsync(uint robotID, User user);
        Task UnbindUserAsync(uint robotID, User user);
        Task<IEnumerable<RobotDTO>> BrowseAsync();
    }
}
