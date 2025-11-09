using RCON.Commands.Minecraft.Java.Op.Models;

namespace RCON.Commands.Minecraft.Java.Op.Commands
{
    /// <summary>
    /// Command to grant operator status to a player
    /// </summary>
    internal class GiveOp : CommandBase<OpResult>
    {
        private readonly string playerName;

        public GiveOp(string playerName)
        {
            this.playerName = playerName;
        }

        /// <inheritdoc/>
        public override string Build() => $"op {playerName}";

        /// <inheritdoc/>
        public override OpResult Parse(string response)
        {
            if (response.Contains("Made", StringComparison.OrdinalIgnoreCase) &&
                response.Contains("server operator", StringComparison.OrdinalIgnoreCase))
            {
                return new OpResult(
                    OpStatus.Success,
                    playerName,
                    response);
            }
            else if (response.Contains("Nothing changed", StringComparison.OrdinalIgnoreCase) &&
                response.Contains("already", StringComparison.OrdinalIgnoreCase))
            {
                return new OpResult(
                    OpStatus.NoChange,
                    playerName,
                    response);
            }
            else if (response.Contains("No player", StringComparison.OrdinalIgnoreCase))
            {
                return new OpResult(
                    OpStatus.NotFound,
                    playerName,
                    response);
            }

            return new OpResult(
                OpStatus.Unknown,
                playerName,
                response);
        }
    }
}
