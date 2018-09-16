namespace Protocol.Camera
{
    /// <summary>
    /// The command used for enabling/disabling cyclic position update of a given robot 
    /// </summary>
    public sealed class SubscribePosition : ICommand
    {
        /// <summary>
        /// Robot's ID
        /// </summary>
        public int ID { get; protected set; }

        /// <summary>
        /// Enable/Disable cyclic position update
        /// </summary>
        public bool Subscribe { get; protected set; }
    }
}
