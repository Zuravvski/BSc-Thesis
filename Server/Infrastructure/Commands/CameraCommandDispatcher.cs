using System;
using System.Threading.Tasks;
using Autofac;
using Infrastructure.Handlers;

namespace Infrastructure.Commands
{
    public class CameraCommandDispatcher : ICameraCommandDispatcher
    {
        private readonly IComponentContext _componentContext;

        public CameraCommandDispatcher(IComponentContext componentContext)
        {
            _componentContext = componentContext;
        }

        public async Task DispatchAsync<T>(T command) where T : ICameraCommand
        {
            if (command == null)
            {
                throw new ArgumentNullException();
            }

            var handlerType = typeof(ICameraCommandHandler<>).MakeGenericType(command.GetType());
            dynamic handler = _componentContext.Resolve(handlerType);
            await handler.HandleAsync((dynamic)command);
        }
    }
}
