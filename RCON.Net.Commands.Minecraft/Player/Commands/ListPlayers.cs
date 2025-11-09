using RCON.Commands.Minecraft.Java.Player.Models;

namespace RCON.Commands.Minecraft.Java.Player.Commands
{
    /// <summary>
    /// Command to list online players
    /// </summary>
    internal class ListPlayers : CommandBase<PlayerListResult>
    {
        private readonly bool includeUuids;

        public ListPlayers(bool includeUuids = false)
        {
            this.includeUuids = includeUuids;
        }

        /// <inheritdoc/>
        public override string Build() => includeUuids ? "list uuids" : "list";

        /// <inheritdoc/>
        public override PlayerListResult Parse(string response)
        {
            var players = new List<Models.Player>();
            int onlineCount = 0;
            int maxPlayers = 0;

            if (string.IsNullOrEmpty(response))
                return new PlayerListResult(players.AsReadOnly(), onlineCount, maxPlayers);

            // Expected format: "There are X of a max of Y players online: player1, player2, ..."
            // Extract counts
            var countMatch = System.Text.RegularExpressions.Regex.Match(response, @"There are (\d+) of a max of (\d+)");
            if (countMatch.Success)
            {
                onlineCount = int.Parse(countMatch.Groups[1].Value);
                maxPlayers = int.Parse(countMatch.Groups[2].Value);
            }

            // Extract player list
            var colonIndex = response.IndexOf(':');
            if (colonIndex == -1 || onlineCount == 0)
                return new PlayerListResult(players.AsReadOnly(), onlineCount, maxPlayers);

            var playersPart = response[(colonIndex + 1)..].Trim();
            if (string.IsNullOrEmpty(playersPart))
                return new PlayerListResult(players.AsReadOnly(), onlineCount, maxPlayers);

            // Parse players (with optional UUIDs)
            var playerEntries = playersPart.Split(',')
                                           .Select(p => p.Trim())
                                           .Where(p => !string.IsNullOrEmpty(p))
                                           .ToList();

            foreach (var entry in playerEntries)
            {
                if (includeUuids && entry.Contains('(') && entry.Contains(')'))
                {
                    // Format: "PlayerName (UUID)"
                    var parenIndex = entry.LastIndexOf('(');
                    var playerName = entry[..parenIndex].Trim();
                    var uuid = entry[(parenIndex + 1)..].TrimEnd(')').Trim();
                    players.Add(new Models.Player(playerName, uuid));
                }
                else
                {
                    players.Add(new Models.Player(entry));
                }
            }

            return new PlayerListResult(players.AsReadOnly(), onlineCount, maxPlayers);
        }
    }
}
