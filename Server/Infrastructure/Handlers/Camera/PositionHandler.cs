using System;
using System.Linq;
using System.Threading.Tasks;
using Core.Domain.Robots.Enums;
using Infrastructure.Services.Robots;
using Position = Infrastructure.Commands.Camera.Position;

namespace Infrastructure.Handlers.Camera
{
    public class PositionHandler : ICameraCommandHandler<Position>
    {
        private readonly IRobotService _robotService;

        public PositionHandler(IRobotService robotService)
        {
            _robotService = robotService;
        }

        public async Task HandleAsync(Position command)
        {
            var robots = await _robotService.BrowseAsync();
            var connected = robots.Where(robot => robot.Connected);

            // TODO: This should be normal for to avoid redundant loops
            var done = false;

            foreach (var robot in connected)
            {
                var position = robot.Position;
                if (Math.Abs(command.X - position.X) < 100 && Math.Abs(command.Y - position.Y) < 100 && !done)
                {
                    robot.Position.X = command.X;
                    robot.Position.Y = command.Y;
                    robot.Position.Theta = command.Theta;
                    robot.Position.Identified = command.Identified
                        ? EPositionState.IDENTIFIED
                        : EPositionState.NOT_IDENTIFIED;
                    done = true;
                }
                else if (position.Identified == EPositionState.PENDING_INFORMATION && !done)
                {
                    robot.Position.X = command.X;
                    robot.Position.Y = command.Y;
                    robot.Position.Theta = command.Theta;
                    robot.Position.Identified = command.Identified
                        ? EPositionState.IDENTIFIED
                        : EPositionState.NOT_IDENTIFIED;
                    done = true;
                }
            }
        }
    }
}
