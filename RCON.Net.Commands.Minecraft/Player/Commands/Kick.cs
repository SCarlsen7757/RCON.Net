using RCON.Commands.Minecraft.Java.Player.Models;

namespace RCON.Commands.Minecraft.Java.Player.Commands
{
    /// <summary>
    /// Command to kick a player from the server
    /// </summary>
    internal class Kick : CommandBase<PlayerActionResult>
    {
        private readonly string playerName;
        private readonly string reason;

        public Kick(string playerName, string reason = "")
        {
            this.playerName = playerName;
            this.reason = reason;
        }

        /// <inheritdoc/>
        public override string Build() => string.IsNullOrEmpty(reason)
       ? $"kick {playerName}"
      : $"kick {playerName} {reason}";

        /// <inheritdoc/>
        public override PlayerActionResult Parse(string response)
        {
            if (response.Contains("Kicked", StringComparison.OrdinalIgnoreCase))
            {
                return new PlayerActionResult(
                 PlayerActionStatus.Success,
                    playerName,
              response);
            }
            else if (response.Contains("No player", StringComparison.OrdinalIgnoreCase))
            {
                return new PlayerActionResult(
                   PlayerActionStatus.NotFound,
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
