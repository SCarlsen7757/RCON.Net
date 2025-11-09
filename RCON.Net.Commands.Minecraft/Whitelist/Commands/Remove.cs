using RCON.Commands.Minecraft.Java.Whitelist.Models;

namespace RCON.Commands.Minecraft.Java.Whitelist.Commands
{
    /// <summary>
    /// Command to remove a player from the whitelist
    /// </summary>
    internal class Remove : CommandBase<ModificationResult>
    {
        private readonly string playerName;

        public Remove(string playerName)
        {
            this.playerName = playerName;
        }

        /// <inheritdoc/>
        public override string Build() => $"whitelist remove {playerName}";

        /// <inheritdoc/>
        public override ModificationResult Parse(string response)
        {
            if (response.Contains("Removed", StringComparison.OrdinalIgnoreCase))
            {
                return new ModificationResult(
                    ModificationStatus.Success,
                    playerName,
                    response);
            }
            else if (response.Contains("not whitelisted", StringComparison.OrdinalIgnoreCase))
            {
                return new ModificationResult(
                    ModificationStatus.NotWhitelisted,
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
