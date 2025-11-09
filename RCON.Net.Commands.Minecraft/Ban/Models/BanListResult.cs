namespace RCON.Commands.Minecraft.Java.Ban.Models
{
    /// <summary>
    /// Represents the result of a banlist query
    /// </summary>
    public class BanListResult
    {
        /// <summary>
        /// Gets the list of banned entries (player names or IP addresses)
        /// </summary>
        public IReadOnlyList<string> Entries { get; }

        /// <summary>
        /// Gets the count of banned entries
        /// </summary>
        public int Count => Entries.Count;

        /// <summary>
        /// Initializes a new instance of the BanListResult class
        /// </summary>
        public BanListResult(IReadOnlyList<string> entries)
        {
            Entries = entries ?? [];
        }
    }
}
