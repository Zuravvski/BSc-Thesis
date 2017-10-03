using System;
using System.Threading.Tasks;
using Infrastructure.Commands.Robots;
using Infrastructure.Services.Robots;
using Infrastructure.Services.Users;

namespace Infrastructure.Handlers.Robots
{
    public class DriveHandler : ICommandHandler<Drive>
    {
        private readonly IRobotService _robotService;
        private readonly IUserService _userService;

        public DriveHandler(IRobotService robotService, IUserService userService)
        {
            _robotService = robotService;
            _userService = userService;
        }

        public async Task HandleAsync(Drive command)
        {
            await Task.Run(() =>
            {
                Console.WriteLine($"Driving robot with ID: {command.RobotID}.");
                Console.WriteLine($"Left motor: {command.LeftMotor}");
                Console.WriteLine($"Right motor: {command.RightMotor}");
                Console.WriteLine($"LEDs: {command.LEDs}");
            });
        }
    }
}
