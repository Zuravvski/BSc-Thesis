using System;
using Infrastructure.Extensions;
using Infrastructure.Network.Sockets;

namespace Infrastructure.Commands.Users
{
    public class BindRobot : ICommand
    {
        public uint ID { get; set; }

        public Client Caller { get; set; }

        public void LoadPayload(string payload)
        {
            var iterator = string.Empty;
            payload.ReduceFront(2, ref iterator);
            ID = uint.Parse(iterator);
        }

    }
}
