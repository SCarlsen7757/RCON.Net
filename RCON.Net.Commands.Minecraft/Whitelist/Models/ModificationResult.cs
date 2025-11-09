namespace RCON.Commands.Minecraft.Java.Whitelist.Models
{
    /// <summary>
    /// Represents the result of a whitelist add/remove operation
    /// </summary>
    public class ModificationResult
    {
        /// <summary>
        /// Gets the status of the operation
        /// </summary>
        public ModificationStatus Status { get; }

        /// <summary>
        /// Gets the player name involved in the operation
        /// </summary>
        public string PlayerName { get; }

        /// <summary>
        /// Gets the message from the server
        /// </summary>
        public string Message { get; }

        /// <summary>
        /// Initializes a new instance of the ModificationResult class
        /// </summary>
        public ModificationResult(ModificationStatus status, string playerName, string message)
        {
            Status = status;
            PlayerName = playerName;
            Message = message;
        }
    }
}
