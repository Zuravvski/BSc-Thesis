using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Core.Domain.Robots;
using Core.Domain.Users;
using Core.Repositories;
using Infrastructure.DTO;
using Infrastructure.Repositories;

namespace Infrastructure.Services.Robots
{
    public class RobotService : IRobotService
    {
        private readonly IRobotRepository _robotRepository;
        private readonly IMapper _mapper;

        public RobotService(IRobotRepository robotRepository, IMapper mapper)
        {
            _robotRepository = robotRepository;
            _mapper = mapper;
        }

        public async Task CreateAsync(uint robotID)
        {
            await _robotRepository.CreateRobotAsync(new Robot(robotID));
        }

        public async Task DeleteAsync(uint robotID)
        {
            await _robotRepository.DeleteRobotAsync(robotID);
        }

        public async Task BindUserAsync(uint robotID, User user)
        {
            var robot = await _robotRepository.GetRobotAsync(robotID);
            robot?.BindUser(user);
        }

        public async Task UnbindUserAsync(uint robotID, User user)
        {
            var robot = await _robotRepository.GetRobotAsync(robotID);
            robot?.UnbindUser(user);
        }

        public async Task<IEnumerable<RobotDTO>> BrowseAsync()
        {
            var robots = await _robotRepository.GetAllRobotsAsync();
            return _mapper.Map<IEnumerable<Robot>, IEnumerable<RobotDTO>>(robots);
        }
    }
}
