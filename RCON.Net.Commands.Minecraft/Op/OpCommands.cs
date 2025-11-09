using RCON.Commands.Minecraft.Java.Op.Models;
using RCON.Core.Interfaces;

namespace RCON.Commands.Minecraft.Java.Op
{
    /// <summary>
    /// Provides a set of commands for managing operator status, including granting and revoking operator privileges.
    /// </summary>
    /// <remarks>This class offers both synchronous and asynchronous methods to interact with the operator system.
    /// Commands are created using static methods and can be executed to perform the desired operations.</remarks>
    public class OpCommands
    {
        private readonly IRconClient client;
        public OpCommands(IRconClient client) => this.client = client;

        /// <summary>
        /// Creates a command to grant operator status to a player.
        /// </summary>
        /// <param name="playerName">The name of the player to make an operator. Cannot be null, empty, or consist only of whitespace.</param>
        /// <returns>A command that, when executed, grants operator status and returns the result of the operation.</returns>
        /// <exception cref="ArgumentException">Thrown if <paramref name="playerName"/> is null, empty, or consists only of whitespace.</exception>
        public static ICommand<OpResult> GiveOperator(string playerName)
        {
            if (string.IsNullOrWhiteSpace(playerName))
                throw new ArgumentException("Player name cannot be null or empty", nameof(playerName));

            return new Commands.GiveOp(playerName);
        }

        /// <summary>
        /// Creates a command to revoke operator status from a player.
        /// </summary>
        /// <param name="playerName">The name of the player to revoke operator status from. Cannot be null, empty, or consist only of whitespace.</param>
        /// <returns>A command that, when executed, revokes operator status and returns the result of the operation.</returns>
        /// <exception cref="ArgumentException">Thrown if <paramref name="playerName"/> is null, empty, or consists only of whitespace.</exception>
        public static ICommand<OpResult> RevokeOperator(string playerName)
        {
            if (string.IsNullOrWhiteSpace(playerName))
                throw new ArgumentException("Player name cannot be null or empty", nameof(playerName));

            return new Commands.RemoveOp(playerName);
        }

        /// <summary>
        /// Grants operator status to a player asynchronously.
        /// </summary>
        /// <param name="playerName">The name of the player to make an operator.</param>
        /// <param name="ct">An optional <see cref="CancellationToken"/> to observe while waiting for the operation to complete.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the asynchronous operation. The task result contains an <see cref="OpResult"/> indicating the outcome of the operation.</returns>
        public Task<OpResult> GiveOperatorAsync(string playerName, CancellationToken ct = default) =>
            client.ExecuteCommandAsync(GiveOperator(playerName), ct);

        /// <summary>
        /// Revokes operator status from a player asynchronously.
        /// </summary>
        /// <param name="playerName">The name of the player to revoke operator status from.</param>
        /// <param name="ct">An optional <see cref="CancellationToken"/> to observe while waiting for the operation to complete.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the asynchronous operation. The task result contains an <see cref="OpResult"/> indicating the outcome of the operation.</returns>
        public Task<OpResult> RevokeOperatorAsync(string playerName, CancellationToken ct = default) =>
            client.ExecuteCommandAsync(RevokeOperator(playerName), ct);
    }
}
