using RCON.Commands.Minecraft.Java.Player.Models;
using System.Numerics;

namespace RCON.Commands.Minecraft.Java.Player.Commands
{
    /// <summary>
    /// Command to teleport a player
    /// </summary>
    internal class Teleport : CommandBase<PlayerActionResult>
    {
        private readonly string playerName;
        private readonly Vector3 destination;

        public Teleport(string playerName, Vector3 destination)
        {
            this.playerName = playerName;
            this.destination = destination;
        }

        /// <inheritdoc/>
        public override string Build() => $"teleport {playerName} {destination.X} {destination.Y} {destination.Z}";

        /// <inheritdoc/>
        public override PlayerActionResult Parse(string response)
        {
            if (response.Contains("Teleported", StringComparison.OrdinalIgnoreCase))
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
