using Protocol.Enums;

namespace Protocol.Users
{
    /// <summary>
    /// The response denoting whether BindCommand was successful
    /// </summary>
    public sealed class BindingReport : IResponse
    {
        /// <summary>
        /// Binding status
        /// </summary>
        public EStatus Status { get; set; }

        /// <summary>
        /// Robot's ID
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        /// [Optional] Binding error
        /// </summary>
        public string Error { get; set; }
    }
}
