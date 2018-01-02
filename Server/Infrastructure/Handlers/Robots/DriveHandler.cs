using System.Diagnostics;
using System.Threading.Tasks;
using Infrastructure.Extensions;
using Infrastructure.Network.Packets.Requests;
using Infrastructure.Services.Robots;
using Protocol.Robots;

namespace Infrastructure.Handlers.Robots
{
    public class DriveHandler : ICommandHandler<Drive>
    {
        private readonly IRobotService _robotService;

        public DriveHandler(IRobotService robotService)
        {
            _robotService = robotService;
        }

        public async Task HandleAsync(Drive command, string clientIP)
        {
            var robot = await _robotService.GetRobotAsync(command.RobotID);
            if (robot == null)
            {
                return;
            }

            if (robot.BoundTo == clientIP)
            {
                var packet = Packet.Create($"{command.LEDs.Description()}{command.LeftMotor}{command.RightMotor}");
                await robot.WriteAsync(packet.ToRawData());
                Debug.WriteLine($"Drive payload: {packet.Payload}");
            }
            else
            {
                Debug.WriteLine($"Client does not own the robot with ID {command.RobotID}");
            }
        }
    }
}
