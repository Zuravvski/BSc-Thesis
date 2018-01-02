using System;
using System.Linq;
using System.Threading.Tasks;
using Infrastructure.Services.Robots;

namespace Infrastructure.Services
{
    public class DataInitializer : IDataInitializer
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

            const string basicIPAddress = "192.168.0.";

            await _robotService.CreateAsync("127.0.0.1");
            for (uint i = 0; i < 10; i++)
            {
                await _robotService.CreateAsync(basicIPAddress + Convert.ToString(i + 30));
            }

            robots = await _robotService.BrowseAsync();
            foreach (var robot in robots)
            {
                await robot.ConnectAsync();
            }
        }
    }
}
