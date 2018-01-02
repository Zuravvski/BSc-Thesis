namespace Protocol.Camera
{
    public class SubscribePosition : ICommand
    {
        public uint ID { get; protected set; }
        public bool Subscribe { get; protected set; }
    }
}
