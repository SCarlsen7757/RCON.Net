using RCON.Commands.Minecraft.Java.Ban.Models;

namespace RCON.Commands.Minecraft.Java.Ban.Commands
{
    /// <summary>
    /// Command to pardon a banned player
    /// </summary>
    internal class Pardon : CommandBase<BanResult>
    {
        private readonly string playerName;

        public Pardon(string playerName)
        {
            this.playerName = playerName;
        }

        /// <inheritdoc/>
        public override string Build() => $"pardon {playerName}";

        /// <inheritdoc/>
        public override BanResult Parse(string response)
        {
            if (response.Contains("Unbanned", StringComparison.OrdinalIgnoreCase))
            {
                return new BanResult(BanStatus.Success,
                                     playerName,
                                     "",
                                     response);
            }
            else if (response.Contains("not banned", StringComparison.OrdinalIgnoreCase))
            {
                return new BanResult(BanStatus.NotFound,
                                     playerName,
                                     "",
                                     response);
            }

            return new BanResult(BanStatus.Unknown,
                                 playerName,
                                 "",
                                 response);
        }
    }
}
