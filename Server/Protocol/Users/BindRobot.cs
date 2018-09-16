namespace Protocol.Users
{
    /// <summary>
    /// The command used to bind a given robot
    /// </summary>
    public sealed class BindRobot : ICommand
    {
        /// <summary>
        /// Robot's ID
        /// </summary>
        public int ID { get; set; }
    }
}
