namespace RCON.Commands.Minecraft.Java.Op.Models
{
    /// <summary>
    /// Represents the result of an op/deop operation
    /// </summary>
    public class OpResult
    {
        /// <summary>
        /// Gets the status of the operation
        /// </summary>
        public OpStatus Status { get; }

        /// <summary>
        /// Gets the player name
        /// </summary>
        public string PlayerName { get; }

        /// <summary>
        /// Gets the message from the server
        /// </summary>
        public string Message { get; }

        /// <summary>
        /// Initializes a new instance of the OpResult class
        /// </summary>
        public OpResult(OpStatus status, string playerName, string message)
        {
            Status = status;
            PlayerName = playerName;
            Message = message;
        }
    }
}
