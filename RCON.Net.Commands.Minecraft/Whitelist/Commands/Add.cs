using RCON.Commands.Minecraft.Java.Whitelist.Models;

namespace RCON.Commands.Minecraft.Java.Whitelist.Commands
{
    /// <summary>
    /// Command to add a player to the whitelist
    /// </summary>
    internal class Add : CommandBase<ModificationResult>
    {
        private readonly string playerName;

        public Add(string playerName)
        {
            this.playerName = playerName;
        }

        /// <inheritdoc/>
        public override string Build() => $"whitelist add {playerName}";

        /// <inheritdoc/>
        public override ModificationResult Parse(string response)
        {
            if (response.Contains("Added", StringComparison.OrdinalIgnoreCase))
            {
                return new ModificationResult(
                    ModificationStatus.Success,
                    playerName,
                    response);
            }
            else if (response.Contains("already whitelisted", StringComparison.OrdinalIgnoreCase))
            {
                return new ModificationResult(
                    ModificationStatus.AlreadyWhitelisted,
                    playerName,
                    response);
            }
            else if (response.Contains("does not exist", StringComparison.OrdinalIgnoreCase))
            {
                return new ModificationResult(
                    ModificationStatus.PlayerDoesNotExist,
                    playerName,
                    response);
            }

            return new ModificationResult(
                ModificationStatus.Unknown,
                playerName,
                response);
        }
    }
}
