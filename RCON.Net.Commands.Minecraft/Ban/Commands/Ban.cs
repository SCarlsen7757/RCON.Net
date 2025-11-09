using RCON.Commands.Minecraft.Java.Ban.Models;

namespace RCON.Commands.Minecraft.Java.Ban.Commands
{
    /// <summary>
    /// Command to ban a player
    /// </summary>
    internal class Ban : CommandBase<BanResult>
    {
        private readonly string playerName;
        private readonly string reason;

        public Ban(string playerName, string reason = "")
        {
            this.playerName = playerName;
            this.reason = reason;
        }

        /// <inheritdoc/>
        public override string Build() => string.IsNullOrEmpty(reason) ? $"ban {playerName}" : $"ban {playerName} {reason}";

        /// <inheritdoc/>
        public override BanResult Parse(string response)
        {
            if (response.Contains("Banned player", StringComparison.OrdinalIgnoreCase))
            {
                return new BanResult(BanStatus.Success,
                                     playerName,
                                     reason,
                                     response);
            }
            else if (response.Contains("already banned", StringComparison.OrdinalIgnoreCase))
            {
                return new BanResult(BanStatus.AlreadyBanned,
                                     playerName,
                                     reason,
                                     response);
            }
            else if (response.Contains("not found", StringComparison.OrdinalIgnoreCase) ||
               response.Contains("no player", StringComparison.OrdinalIgnoreCase))
            {
                return new BanResult(BanStatus.NotFound,
                                     playerName,
                                     reason,
                                     response);
            }

            return new BanResult(BanStatus.Unknown,
                                 playerName,
                                 reason,
                                 response);
        }
    }
}
