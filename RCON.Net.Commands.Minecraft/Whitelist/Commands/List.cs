using RCON.Commands.Minecraft.Java.Whitelist.Models;

namespace RCON.Commands.Minecraft.Java.Whitelist.Commands
{
    /// <summary>
    /// Command to list whitelisted players
    /// </summary>
    internal class List : CommandBase<ListResult>
    {
        /// <inheritdoc/>
        public override string Build() => "whitelist list";

        /// <inheritdoc/>
        public override ListResult Parse(string response)
        {
            // Expected format: "There are X whitelisted player(s): player1, player2, ..."
            var players = new List<string>();

            if (string.IsNullOrEmpty(response))
                return new ListResult(players.AsReadOnly());

            // Check if there are players listed
            if (!response.Contains("whitelisted player"))
                return new ListResult(players.AsReadOnly());

            // Extract the part after the colon
            var colonIndex = response.IndexOf(':');
            if (colonIndex == -1)
                return new ListResult(players.AsReadOnly());

            var playersPart = response[(colonIndex + 1)..].Trim();
            if (string.IsNullOrEmpty(playersPart))
                return new ListResult(players.AsReadOnly());

            // Split by comma and trim each player name
            var playerNames = playersPart.Split(',')
                .Select(p => p.Trim())
                .Where(p => !string.IsNullOrEmpty(p))
                .ToList();

            return new ListResult(playerNames.AsReadOnly());
        }
    }
}
