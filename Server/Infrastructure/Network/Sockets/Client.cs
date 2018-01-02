using System;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using Infrastructure.Commands;
using Infrastructure.Network.Packets.Requests;
using Infrastructure.Repositories;
using Newtonsoft.Json;
using Protocol;
using Protocol.Users;

namespace Infrastructure.Network.Sockets
{
    public class Client
    {
        public delegate Client Factory(TcpClient clientSocket);

        private readonly TcpClient _socket;
        private readonly NetworkStream _stream;
        private readonly IClientCommandDispatcher _commandDispatcher;
        private readonly IClientRepository _clientRepository;

        public bool Connected => _socket != null && _socket.Connected;
        public string IPAddress { get; }


        public Client(TcpClient clientSocket, IClientRepository clientRepository, IComponentContext context)
        {
            _socket = clientSocket;
            _stream = _socket.GetStream();
            _clientRepository = clientRepository;

            IPAddress = (_socket.Client.RemoteEndPoint as IPEndPoint).Address.ToString();
            _commandDispatcher = context.Resolve<IClientCommandDispatcher>(new NamedParameter("ClientIP", IPAddress));
            ReadAsync();
        }

        public async Task WriteAsync(Packet packet)
        {
            var rawData = packet.ToRawData();
            await _stream.WriteAsync(rawData, 0, rawData.Length);
        }

        private async void ReadAsync()
        { 
            var settings = new JsonSerializerSettings() {TypeNameHandling = TypeNameHandling.All};
            while (Connected)
            {
                try
                {
                    var buffer = new byte[1024];
                    var bytesRead = await _stream.ReadAsync(buffer, 0, buffer.Length);
                    var read = Encoding.ASCII.GetString(buffer);

                    if (bytesRead == 0)
                    {
                        _socket.Close();
                        continue;
                    }

                    var command = JsonConvert.DeserializeObject<ICommand>(read, settings);
                    await _commandDispatcher.DispatchAsync(command);
                }
                catch (IOException)
                {
                    _socket.Close();
                } 
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    Console.WriteLine("Invalid packet");
                }
            }
            Debug.WriteLine($"Client with IP: {IPAddress} disconnected");
            await _clientRepository.DeleteClientAsync(IPAddress);
            await _commandDispatcher.DispatchAsync(new UnbindAllRobots());
        }
    }
}
