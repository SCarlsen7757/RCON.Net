using RCON.Commands.Minecraft.Java.Player.Models;
using RCON.Core.Interfaces;
using System.Numerics;

namespace RCON.Commands.Minecraft.Java.Player
{
    /// <summary>
    /// Provides a set of commands for managing players, including kicking, killing, listing, and teleporting.
    /// </summary>
    /// <remarks>This class offers both synchronous and asynchronous methods to interact with the player system.
    /// Commands are created using static methods and can be executed to perform the desired operations.</remarks>
    public class PlayerCommands
    {
        private readonly IRconClient client;
        public PlayerCommands(IRconClient client) => this.client = client;

        /// <summary>
        /// Creates a command to kick a player from the server.
        /// </summary>
        /// <param name="playerName">The name of the player to kick. Cannot be null, empty, or consist only of whitespace.</param>
        /// <param name="reason">Optional reason for the kick.</param>
        /// <returns>A command that, when executed, kicks the player and returns the result of the operation.</returns>
        /// <exception cref="ArgumentException">Thrown if <paramref name="playerName"/> is null, empty, or consists only of whitespace.</exception>
        public static ICommand<PlayerActionResult> KickPlayer(string playerName, string reason)
        {
            if (string.IsNullOrWhiteSpace(playerName))
                throw new ArgumentException("Player name cannot be null or empty", nameof(playerName));

            return new Commands.Kick(playerName, reason);
        }

        /// <summary>
        /// Creates a command to kill a player.
        /// </summary>
        /// <param name="playerName">The name of the player to kill. Cannot be null, empty, or consist only of whitespace.</param>
        /// <returns>A command that, when executed, kills the player and returns the result of the operation.</returns>
        /// <exception cref="ArgumentException">Thrown if <paramref name="playerName"/> is null, empty, or consists only of whitespace.</exception>
        public static ICommand<PlayerActionResult> KillPlayer(string playerName)
        {
            if (string.IsNullOrWhiteSpace(playerName))
                throw new ArgumentException("Player name cannot be null or empty", nameof(playerName));

            return new Commands.Kill(playerName);
        }

        /// <summary>
        /// Creates a command to list online players.
        /// </summary>
        /// <param name="includeUuids">If true, includes player UUIDs in the result.</param>
        /// <returns>A command that, when executed, returns the list of online players.</returns>
        public static ICommand<PlayerListResult> GetPlayerList(bool includeUuids = false)
        {
            return new Commands.ListPlayers(includeUuids);
        }

        /// <summary>
        /// Creates a command to teleport a player to specific coordinates.
        /// </summary>
        /// <param name="playerName">The name of the player to teleport. Cannot be null, empty, or consist only of whitespace.</param>
        /// <param name="destionation">XYZ coordinate.</param>
        /// <returns>A command that, when executed, teleports the player and returns the result of the operation.</returns>
        /// <exception cref="ArgumentException">Thrown if <paramref name="playerName"/> is null, empty, or consists only of whitespace.</exception>
        public static ICommand<PlayerActionResult> TeleportPlayer(string playerName, Vector3 destionation)
        {
            if (string.IsNullOrWhiteSpace(playerName))
                throw new ArgumentException("Player name cannot be null or empty", nameof(playerName));

            return new Commands.Teleport(playerName, destionation);
        }

        /// <summary>
        /// Kicks a player from the server asynchronously.
        /// </summary>
        /// <param name="playerName">The name of the player to kick.</param>
        /// <param name="reason">Optional reason for the kick.</param>
        /// <param name="ct">An optional <see cref="CancellationToken"/> to observe while waiting for the operation to complete.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the asynchronous operation. The task result contains a <see cref="PlayerActionResult"/> indicating the outcome of the operation.</returns>
        public Task<PlayerActionResult> KickPlayerAsync(string playerName, string reason, CancellationToken ct = default) =>
            client.ExecuteCommandAsync(KickPlayer(playerName, reason), ct);

        /// <summary>
        /// Kills a player asynchronously.
        /// </summary>
        /// <param name="playerName">The name of the player to kill.</param>
        /// <param name="ct">An optional <see cref="CancellationToken"/> to observe while waiting for the operation to complete.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the asynchronous operation. The task result contains a <see cref="PlayerActionResult"/> indicating the outcome of the operation.</returns>
        public Task<PlayerActionResult> KillPlayerAsync(string playerName, CancellationToken ct = default) =>
            client.ExecuteCommandAsync(KillPlayer(playerName), ct);

        /// <summary>
        /// Retrieves the list of online players asynchronously.
        /// </summary>
        /// <param name="includeUuids">If true, includes player UUIDs in the result.</param>
        /// <param name="ct">A <see cref="CancellationToken"/> that can be used to cancel the operation.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains a <see cref="PlayerListResult"/> object.</returns>
        public Task<PlayerListResult> GetPlayerListAsync(bool includeUuids = false, CancellationToken ct = default) =>
            client.ExecuteCommandAsync(GetPlayerList(includeUuids), ct);

        /// <summary>
        /// Teleports a player to specific coordinates asynchronously.
        /// </summary>
        /// <param name="playerName">The name of the player to teleport.</param>
        /// <param name="destination">XYZ coordinate.</param>
        /// <param name="ct">An optional <see cref="CancellationToken"/> to observe while waiting for the operation to complete.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the asynchronous operation. The task result contains a <see cref="PlayerActionResult"/> indicating the outcome of the operation.</returns>
        public Task<PlayerActionResult> TeleportPlayerAsync(string playerName, Vector3 destination, CancellationToken ct = default) =>
            client.ExecuteCommandAsync(TeleportPlayer(playerName, destination), ct);
    }
}
