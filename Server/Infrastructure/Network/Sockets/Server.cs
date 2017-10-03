using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using Autofac;
using Infrastructure.IoC;
using Infrastructure.Network.Packets.Requests;
using Infrastructure.Services;
using Infrastructure.Services.Robots;

namespace Infrastructure.Network.Sockets
{
    public class Server
    {
        private readonly TcpListener _socket;
        private readonly List<Client> _clientsList;

        public bool Running => _socket.Server.Connected;

        public IContainer ApplicationContainer { get; private set; }

        public Server()
        {
            _socket = new TcpListener(IPAddress.Any, 50131);
            _clientsList = new List<Client>();
           InitializeAutofac();
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
                _clientsList.Add(client);
            }
        }

        private async void InitializeAutofac()
        {
            // Autofac configuration
            var containerBuilder = new ContainerBuilder();
            containerBuilder.RegisterType<Client>().InstancePerLifetimeScope();
            containerBuilder.RegisterInstance(new RequestCommandFactory()).SingleInstance();
            containerBuilder.RegisterModule(new ContainerModule());
            ApplicationContainer = containerBuilder.Build();

            var dataInitializer = new DataInitializer(ApplicationContainer.Resolve<IRobotService>());
            await dataInitializer.SeedAsync();
        }
    }
}
