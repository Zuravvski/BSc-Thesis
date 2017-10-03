using System.Linq;
using System.Threading.Tasks;
using Infrastructure.Services.Robots;

namespace Infrastructure.Services
{
    public class DataInitializer : IService
    {
        private readonly IRobotService _robotService;

        public DataInitializer(IRobotService robotService)
        {
            _robotService = robotService;
        }

        public async Task SeedAsync()
        {
            var robots = await _robotService.BrowseAsync();
            if (robots.Any())
            {
                return;
            }

            for (uint i = 0; i < 10; i++)
            {
                await _robotService.CreateAsync(i + 30);
            }
        }
    }
}
