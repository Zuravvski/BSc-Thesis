using System;
using System.Threading.Tasks;
using Autofac;
using Infrastructure.Handlers;

namespace Infrastructure.Commands
{
    public class CommandDispatcher : ICommandDispatcher
    {
        private readonly IComponentContext _componentContext;

        public CommandDispatcher(IComponentContext componentContext)
        {
            _componentContext = componentContext;
        }

        public async Task DispatchAsync<T>(T command) where T : ICommand
        {
            if (command == null)
            {
                throw new ArgumentNullException();
            }

            var handlerType = typeof(ICommandHandler<>).MakeGenericType(command.GetType());
            dynamic handler = _componentContext.Resolve(handlerType);
            await handler.HandleAsync((dynamic)command);
        }
    }
}
