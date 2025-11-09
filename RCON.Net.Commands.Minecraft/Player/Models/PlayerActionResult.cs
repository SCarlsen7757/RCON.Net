namespace RCON.Commands.Minecraft.Java.Player.Models
{
    /// <summary>
    /// Represents the result of a player action (kick, kill, teleport)
    /// </summary>
    public class PlayerActionResult
    {
        /// <summary>
        /// Gets the status of the action
        /// </summary>
        public PlayerActionStatus Status { get; }

        /// <summary>
        /// Gets the player name
        /// </summary>
        public string PlayerName { get; }

        /// <summary>
        /// Gets the message from the server
        /// </summary>
        public string Message { get; }

        /// <summary>
        /// Initializes a new instance of the PlayerActionResult class
        /// </summary>
        public PlayerActionResult(PlayerActionStatus status, string playerName, string message)
        {
            Status = status;
            PlayerName = playerName;
            Message = message;
        }
    }
}
