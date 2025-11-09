using RCON.Commands.Minecraft.Java.Ban.Models;

namespace RCON.Commands.Minecraft.Java.Ban.Commands
{
    /// <summary>
    /// Command to list banned players or IP addresses
    /// </summary>
    internal class BanListPlayers : CommandBase<BanListResult>
    {
        /// <inheritdoc/>
        public override string Build() => "banlist players";

        /// <inheritdoc/>
        public override BanListResult Parse(string response)
        {
            var entries = new List<string>();

            if (string.IsNullOrEmpty(response) || response.Contains("There are no bans", StringComparison.OrdinalIgnoreCase))
                return new BanListResult(entries.AsReadOnly());

            // Expected format: "There are X ban(s): entry1, entry2, ..."
            var colonIndex = response.IndexOf(':');
            if (colonIndex == -1)
                return new BanListResult(entries.AsReadOnly());

            var entriesPart = response[(colonIndex + 1)..].Trim();
            if (string.IsNullOrEmpty(entriesPart))
                return new BanListResult(entries.AsReadOnly());

            // Handle format with ban reason: "entry was banned by Admin: reason"
            var entryList = entriesPart.Split(',')
                .Select(e =>
                {
                    var entry = e.Trim();
                    // Extract just the entry name/ip, remove the ban reason if present
                    var wasBannedIndex = entry.IndexOf(" was banned by", StringComparison.OrdinalIgnoreCase);
                    if (wasBannedIndex > 0)
                        entry = entry[..wasBannedIndex];
                    return entry;
                })
                .Where(e => !string.IsNullOrEmpty(e))
                .ToList();

            return new BanListResult(entryList.AsReadOnly());
        }
    }
}
