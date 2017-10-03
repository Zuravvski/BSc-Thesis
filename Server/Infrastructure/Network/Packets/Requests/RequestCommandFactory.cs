using System;
using System.ComponentModel;
using Infrastructure.Commands;
using Infrastructure.Commands.Camera;
using Infrastructure.Commands.Robots;
using Infrastructure.Commands.Users;
using Infrastructure.Extensions;
using Infrastructure.Network.Sockets;

namespace Infrastructure.Network.Packets.Requests
{
    public class RequestCommandFactory
    {
        public ICommand GetRequest(Packet packet, Client caller)
        {
            ICommand result;
            var req = string.Empty;
            var payload = packet.Payload.ReduceFront(2, ref req);
            var requestType = EnumExtensions.GetValueFromDescription<RequestType>(req); 

            switch (requestType)
            {
                case RequestType.BIND_ROBOT:
                    result = new BindRobot();
                    break;

                case RequestType.UNBIND_ROBOT:
                    result = new UnbindRobot();
                    break;

                case RequestType.DRIVE:
                    result = new Drive();
                    break;

                case RequestType.SUBSCRIBE_POSITION:
                    result = new SubscribePosition();
                    break;

                default:
                    throw new InvalidEnumArgumentException();
            }

            result.LoadPayload(payload);
            result.Caller = caller;

            return result;
        }
    }
}
