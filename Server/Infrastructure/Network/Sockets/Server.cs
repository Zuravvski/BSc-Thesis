using System;
using System.Net;
using System.Net.Sockets;
using Autofac;
using Infrastructure.IoC;
using Infrastructure.Repositories;
using Infrastructure.Services;

namespace Infrastructure.Network.Sockets
{
    public class Server
    {
        private readonly TcpListener _socket;
        private readonly IClientRepository _clientRepository;

        public bool Running => _socket.Server.Connected;

        public IContainer ApplicationContainer { get; private set; }

        public Server()
        {
            _socket = new TcpListener(IPAddress.Any, 50131);
            InitializeAutofac();
            _clientRepository = ApplicationContainer.Resolve<IClientRepository>();
        }

        public void Start()
        {
            _socket.Start();
            Console.WriteLine("Server started!");
            Accept();
        }

        private async void Accept()
        {
            Console.WriteLine("Started accepting clients...");
            while (true)
            {
                var clientSocket = await _socket.AcceptTcpClientAsync();
                var clientFactory = ApplicationContainer.Resolve<Client.Factory>();
                var client = clientFactory.Invoke(clientSocket);
                await _clientRepository.CreateClientAsync(client);
            }
        }

        private async void InitializeAutofac()
        {
            // Autofac configuration
            var containerBuilder = new ContainerBuilder();
            containerBuilder.RegisterType<Client>().InstancePerLifetimeScope();
            containerBuilder.RegisterModule(new ContainerModule());
            ApplicationContainer = containerBuilder.Build();
 
            // Initializing test data
            await ApplicationContainer.Resolve<IDataInitializer>().SeedAsync();
        }
    }
}
