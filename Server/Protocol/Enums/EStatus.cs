using System.ComponentModel;

namespace Protocol.Enums
{
    /// <summary>
    /// Command's execution status
    /// </summary>
    public enum EStatus
    {
        /// <summary>
        /// Command has been executed successfully
        /// </summary>
        [Description("OK")]
        ACK,

        /// <summary>
        /// Command's execution has failed
        /// </summary>
        [Description("NACK")]
        NACK
    }
}
