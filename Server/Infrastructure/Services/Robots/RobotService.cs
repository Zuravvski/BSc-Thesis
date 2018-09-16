using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Core.Domain.Robots;
using Core.Repositories;

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

        public async Task CreateAsync(string ip)
        {
            await _robotRepository.CreateRobotAsync(new Robot(ip));
        }

        public async Task DeleteAsync(string ip)
        {
            await _robotRepository.DeleteRobotAsync(ip);
        }

        public async Task<IEnumerable<Robot>> BrowseAsync()
        {
            return await _robotRepository.GetAllRobotsAsync();
        }

        public async Task<Robot> GetRobotAsync(int id)
        {
            return await _robotRepository.GetRobotAsync(id);
        }

        public async Task<Robot> GetRobotAsync(string ip)
        {
            return await _robotRepository.GetRobotAsync(ip);
        }
    }
}
