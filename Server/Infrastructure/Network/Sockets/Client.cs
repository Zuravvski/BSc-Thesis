using System;
using System.Net.Sockets;
using System.Threading.Tasks;
using Infrastructure.Commands;
using Infrastructure.Network.Packets.Requests;

namespace Infrastructure.Network.Sockets
{
    public class Client
    {
        public delegate Client Factory(TcpClient clientSocket);

        private readonly TcpClient _socket;
        private readonly NetworkStream _stream;
        private readonly RequestCommandFactory _commandFactory;
        private readonly ICommandDispatcher _commandDispatcher;

        private static readonly int BUFFER_SIZE = 1024;

        public bool Connected => _socket.Connected;

        public Client(TcpClient clientSocket, RequestCommandFactory requestCommandFactory, ICommandDispatcher commandDispatcher)
        {
            _socket = clientSocket;
            _stream = _socket.GetStream();
            _commandFactory = requestCommandFactory;
            _commandDispatcher = commandDispatcher;

            ReadAsync();
        }

        public async Task WriteAsync(Packet packet)
        {
            var rawData = packet.ToRawData();
            await _stream.WriteAsync(rawData, 0, rawData.Length);
        }

        private async Task ReadAsync()
        {
            while (Connected)
            {
                try
                {
                    var buffer = new byte[BUFFER_SIZE];
                    var bytesRead = await _stream.ReadAsync(buffer, 0, BUFFER_SIZE);
                    Array.Resize(ref buffer, bytesRead);
                    var packet = Packet.CreateFromRawData(buffer);

                    if (packet == null)
                        continue;

                    var command = _commandFactory.GetRequest(packet, this);
                    await _commandDispatcher.DispatchAsync(command);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    Console.WriteLine("Invalid packet");
                }
            }
        }
    }
}
