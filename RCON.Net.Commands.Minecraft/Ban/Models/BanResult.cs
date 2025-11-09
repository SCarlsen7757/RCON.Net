namespace RCON.Commands.Minecraft.Java.Ban.Models
{
    /// <summary>
    /// Represents the result of a ban/unban operation
    /// </summary>
    public class BanResult
    {
        /// <summary>
        /// Gets the status of the operation
        /// </summary>
        public BanStatus Status { get; }

        /// <summary>
        /// Gets the target (player name or IP address)
        /// </summary>
        public string Target { get; }

        /// <summary>
        /// Gets the ban reason
        /// </summary>
        public string Reason { get; }

        /// <summary>
        /// Gets the message from the server
        /// </summary>
        public string Message { get; }

        /// <summary>
        /// Initializes a new instance of the BanResult class
        /// </summary>
        public BanResult(BanStatus status, string target, string reason, string message)
        {
            Status = status;
            Target = target;
            Reason = reason;
            Message = message;
        }
    }
}
