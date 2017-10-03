using System;
using Infrastructure.Extensions;
using Infrastructure.Network.Sockets;

namespace Infrastructure.Commands.Users
{
    public class UnbindRobot : ICommand
    {
        public uint ID { get; set; }

        public Client Caller { get; set; }

        public void LoadPayload(string payload)
        {
            if (payload.Empty() && payload.Length % 2 != 0)
                throw new ArgumentException();

            ID = Convert.ToUInt32(payload);
        }
    }
}
