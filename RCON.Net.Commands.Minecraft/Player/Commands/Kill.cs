using RCON.Commands.Minecraft.Java.Player.Models;

namespace RCON.Commands.Minecraft.Java.Player.Commands
{
    /// <summary>
    /// Command to kill a player
    /// </summary>
    internal class Kill : CommandBase<PlayerActionResult>
    {
        private readonly string playerName;

        public Kill(string playerName)
        {
            this.playerName = playerName;
        }

        /// <inheritdoc/>
        public override string Build() => $"kill {playerName}";

        /// <inheritdoc/>
        public override PlayerActionResult Parse(string response)
        {
            if (response.Contains("Killed", StringComparison.OrdinalIgnoreCase))
            {
                return new PlayerActionResult(
                  PlayerActionStatus.Success,
          playerName,
                  response);
            }
            else if (response.Contains("No entity", StringComparison.OrdinalIgnoreCase))
            {
                return new PlayerActionResult(
                  PlayerActionStatus.NoEntity,
                     playerName,
           response);
            }

            return new PlayerActionResult(
               PlayerActionStatus.Unknown,
            playerName,
            response);
        }
    }
}
