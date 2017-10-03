using System;
using Infrastructure.Extensions;
using Infrastructure.Network.Sockets;

namespace Infrastructure.Commands.Camera
{
    public class SubscribePosition : ICommand
    {
        public uint ID { get; protected set; }
        public bool Subscribe { get; protected set; }

        public Client Caller { get; set; }

        public void LoadPayload(string payload)
        {
            if (payload.Empty() && payload.Length % 3 != 0)
                throw new ArgumentException();

            ID = Convert.ToUInt32(payload.Substring(0, 2));
            Subscribe = payload[2] == '1';
        }
    }
}
