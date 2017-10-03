using System;

namespace Infrastructure.Attributes
{
    public class PacketAttribute : Attribute
    {
        public string ID { get; set; }
    }
}
