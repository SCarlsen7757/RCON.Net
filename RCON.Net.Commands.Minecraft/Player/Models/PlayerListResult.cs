namespace RCON.Commands.Minecraft.Java.Player.Models
{
    /// <summary>
    /// Represents the result of a list players query
    /// </summary>
    public class PlayerListResult
    {
        /// <summary>
        /// Gets the list of online players
        /// </summary>
        public IReadOnlyList<Player> Players { get; }

        /// <summary>
        /// Gets the current player count
        /// </summary>
        public int OnlineCount { get; }

        /// <summary>
        /// Gets the maximum allowed players
        /// </summary>
        public int MaxPlayers { get; }

        /// <summary>
        /// Initializes a new instance of the PlayerListResult class
        /// </summary>
        public PlayerListResult(IReadOnlyList<Player> players, int onlineCount, int maxPlayers)
        {
            Players = players ?? [];
            OnlineCount = onlineCount;
            MaxPlayers = maxPlayers;
        }
    }
}
