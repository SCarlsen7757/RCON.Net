namespace RCON.Commands.Minecraft.Java.Player.Models
{
    /// <summary>
    /// Represents a player in the player list
    /// </summary>
    public class Player
    {
        /// <summary>
        /// Gets the player name
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Gets the player's UUID (if available)
        /// </summary>
        public string? Uuid { get; }

        /// <summary>
        /// Initializes a new instance of the Player class
        /// </summary>
        public Player(string name, string? uuid = null)
        {
            Name = name;
            Uuid = uuid;
        }
    }
}
