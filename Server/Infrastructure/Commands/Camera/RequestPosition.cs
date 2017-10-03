using System;
using Infrastructure.Extensions;
using Infrastructure.Network.Sockets;

namespace Infrastructure.Commands.Camera
{
    public class RequestPosition : ICommand
    {
        public int ID { get; set; }

        public Client Caller { get; set; }

        public void LoadPayload(string payload)
        {
            if (payload.Empty() && payload.Length % 2 != 0)
                throw new ArgumentException();

            ID = Convert.ToInt32(payload);
        }
    }
}
