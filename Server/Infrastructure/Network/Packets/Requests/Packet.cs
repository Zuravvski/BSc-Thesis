using System;
using System.Text;

namespace Infrastructure.Network.Packets.Requests
{
    public class Packet
    {
        public static readonly string FRAME_BEGIN = "[";
        public static readonly string FRAME_END = "]";

        public string Payload { get; protected set; }

        protected Packet() { }

        public static Packet CreateFromRawData(byte[] data)
        {
            /*  Algorithm:
                1. Convert to sting
                2. Locate indexes of frame delimiters
                3. Validate (invalid => returns null)
                4. Initialize packet's payload
             */
            var encoded = Encoding.ASCII.GetString(data);
            var startIndex = encoded.IndexOf(FRAME_BEGIN, StringComparison.Ordinal);
            var endIndex = encoded.IndexOf(FRAME_END, StringComparison.Ordinal);

            if (startIndex == endIndex || endIndex - startIndex < 2)
                return null;

            var packetData = encoded.Substring(startIndex + 1, endIndex - 1);

            var packet = new Packet
            {
                Payload = packetData
            };
            return packet;
        }

        public static Packet Create(string payload)
        {
            return new Packet()
            {
                Payload = FRAME_BEGIN + payload + FRAME_END
            };
        }

        public byte[] ToRawData()
        {
            return Encoding.ASCII.GetBytes(Payload);
        }
    }
}
