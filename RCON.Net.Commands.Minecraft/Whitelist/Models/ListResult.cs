namespace RCON.Commands.Minecraft.Java.Whitelist.Models
{
    /// <summary>
    /// Represents the result of a whitelist list operation
    /// </summary>
    public class ListResult
    {
        /// <summary>
        /// Gets the list of whitelisted players
        /// </summary>
        public IReadOnlyList<string> Players { get; }

        /// <summary>
        /// Gets the count of whitelisted players
        /// </summary>
        public int Count => Players.Count;

        /// <summary>
        /// Initializes a new instance of the ListResult class
        /// </summary>
        public ListResult(IReadOnlyList<string> players)
        {
            Players = players ?? [];
        }
    }
}
