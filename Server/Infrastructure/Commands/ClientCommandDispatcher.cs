using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Autofac;
using Infrastructure.Handlers;
using Protocol;

namespace Infrastructure.Commands
{
    public class ClientCommandDispatcher : IClientCommandDispatcher
    {
        private readonly IComponentContext _componentContext;
        private readonly string _clientIP;

        public ClientCommandDispatcher(IComponentContext componentContext, string ClientIP)
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

            var handlerType = typeof(IClientCommandHandler<>).MakeGenericType(command.GetType());
            dynamic handler = _componentContext.Resolve(handlerType);
            await handler.HandleAsync((dynamic)command, _clientIP);
        }
    }
}
