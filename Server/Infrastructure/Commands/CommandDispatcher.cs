using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Autofac;
using Infrastructure.Handlers;
using Protocol;

namespace Infrastructure.Commands
{
    public class CommandDispatcher : ICommandDispatcher
    {
        private readonly IComponentContext _componentContext;
        private string _clientIP;

        public CommandDispatcher(IComponentContext componentContext, string ClientIP)
        {
            _componentContext = componentContext;
            _clientIP = ClientIP;
            Debug.WriteLine($"Client IP from dispatcher: {_clientIP}");
        }

        public async Task DispatchAsync<T>(T command) where T : ICommand
        {
            if (command == null)
            {
                throw new ArgumentNullException();
            }

            var handlerType = typeof(ICommandHandler<>).MakeGenericType(command.GetType());
            dynamic handler = _componentContext.Resolve(handlerType);
            await handler.HandleAsync((dynamic)command, _clientIP);
        }
    }
}
