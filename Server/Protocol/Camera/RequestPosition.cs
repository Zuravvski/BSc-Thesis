namespace Protocol.Camera
{
    /// <summary>
    /// The command used to request a position of the robot with given ID
    /// </summary>
    public sealed class RequestPosition : ICommand
    {
        /// <summary>
        /// Robot's ID
        /// </summary>
        public int ID { get; set; }
    }
}
