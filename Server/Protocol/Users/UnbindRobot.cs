namespace Protocol.Users
{
    /// <summary>
    /// The command used to unbind a given owned robot
    /// </summary>
    public sealed class UnbindRobot : ICommand
    {
        /// <summary>
        /// Robot's ID
        /// </summary>
        public int ID { get; set; }
    }
}
