using System.Threading.Tasks;
using Autofac;
using Protocol.Robots;

namespace Infrastructure.Handlers.Robots
{
    public class DriveManyHandler : ICommandHandler<DriveMany>
    {
        private readonly IComponentContext _context;

        public DriveManyHandler(IComponentContext context)
        {
            _context = context;
        }

        public async Task HandleAsync(DriveMany command, string clientIP)
        {
            var driveHandler = _context.Resolve<ICommandHandler<Drive>>();

            foreach (var drive in command.DriveCommands)
            {
                await driveHandler.HandleAsync(drive, clientIP);
            }
        }
    }
}
