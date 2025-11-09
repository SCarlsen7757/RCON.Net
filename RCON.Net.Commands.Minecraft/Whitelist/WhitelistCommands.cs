using RCON.Commands.Minecraft.Java.Whitelist.Commands;
using RCON.Commands.Minecraft.Java.Whitelist.Models;
using RCON.Core.Interfaces;

namespace RCON.Commands.Minecraft.Java.Whitelist
{
    /// <summary>
    /// Provides a set of commands for managing a whitelist, including adding, removing, and listing entries, as well as
    /// enabling, disabling, and reloading the whitelist configuration.
    /// </summary>
    /// <remarks>This class offers both synchronous and asynchronous methods to interact with the whitelist.
    /// Commands are created using static methods and can be executed to perform the desired operations. Asynchronous
    /// methods are provided for scenarios where non-blocking operations are required.</remarks>
    public class WhitelistCommands
    {
        private readonly IRconClient client;
        public WhitelistCommands(IRconClient client) => this.client = client;

        /// <summary>
        /// Retrieves a command to list all entries in the whitelist.
        /// </summary>
        /// <returns>A command that, when executed, returns the result of the whitelist entries as a <see
        /// cref="ListResult"/>.</returns>
        public static ICommand<ListResult> GetPlayers()
        {
            return new List();
        }

        /// <summary>
        /// Creates a command to add a player to the whitelist.
        /// </summary>
        /// <param name="playerName">The name of the player to be added to the whitelist. Cannot be null, empty, or consist only of whitespace.</param>
        /// <returns>A command that, when executed, adds the specified player to the whitelist and returns the result of the
        /// operation.</returns>
        /// <exception cref="ArgumentException">Thrown if <paramref name="playerName"/> is null, empty, or consists only of whitespace.</exception>
        public static ICommand<ModificationResult> AddPlayer(string playerName)
        {
            if (string.IsNullOrWhiteSpace(playerName))
                throw new ArgumentException("Player name cannot be null or empty", nameof(playerName));

            return new Add(playerName);
        }

        /// <summary>
        /// Removes a player from the whitelist based on the specified player name.
        /// </summary>
        /// <param name="playerName">The name of the player to remove from the whitelist. Cannot be null, empty, or consist only of whitespace.</param>
        /// <returns>An <see cref="ICommand{T}"/> representing the operation to remove the player from the whitelist, where the
        /// result indicates the outcome of the modification.</returns>
        /// <exception cref="ArgumentException">Thrown if <paramref name="playerName"/> is null, empty, or consists only of whitespace.</exception>
        public static ICommand<ModificationResult> RemovePlayer(string playerName)
        {
            if (string.IsNullOrWhiteSpace(playerName))
                throw new ArgumentException("Player name cannot be null or empty", nameof(playerName));

            return new Remove(playerName);
        }

        /// <summary>
        /// Creates and returns a command to enable the whitelist feature.
        /// </summary>
        /// <returns>An <see cref="ICommand{T}"/> instance that represents the operation to enable the whitelist,  returning a
        /// <see cref="ToggleResult"/> indicating the outcome.</returns>
        public static ICommand<ToggleResult> Enable()
        {
            return new On();
        }

        /// <summary>
        /// Creates a command to disable the whitelist.
        /// </summary>
        /// <returns>An <see cref="ICommand{ToggleResult}"/> that represents the operation to turn off the whitelist.</returns>
        public static ICommand<ToggleResult> Disable()
        {
            return new Off();
        }

        /// <summary>
        /// Creates and returns a command to reload the whitelist configuration.
        /// </summary>
        /// <returns>An <see cref="ICommand{T}"/> instance that represents the reload operation for the whitelist.</returns>
        public static ICommand<string> ReloadConfiguration()
        {
            return new Reload();
        }

        /// <summary>
        /// Adds the specified player to the whitelist asynchronously.
        /// </summary>
        /// <remarks>This method sends a command to add the specified player to the whitelist. Ensure the
        /// player name is valid and conforms to the expected format.</remarks>
        /// <param name="playerName">The name of the player to add to the whitelist. Cannot be null or empty.</param>
        /// <param name="ct">An optional <see cref="CancellationToken"/> to observe while waiting for the operation to complete.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the asynchronous operation. The task result contains a <see
        /// cref="ModificationResult"/> indicating the outcome of the operation.</returns>
        public Task<ModificationResult> AddPlayerAsync(string playerName, CancellationToken ct = default) =>
            client.ExecuteCommandAsync(AddPlayer(playerName), ct);

        /// <summary>
        /// Removes the specified player from the whitelist asynchronously.
        /// </summary>
        /// <remarks>This method sends a command to remove the specified player from the whitelist. Ensure
        /// the player name is valid and matches the format expected by the server.</remarks>
        /// <param name="playerName">The name of the player to remove from the whitelist. Cannot be null or empty.</param>
        /// <param name="ct">An optional <see cref="CancellationToken"/> to observe while waiting for the operation to complete.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the asynchronous operation. The task result contains a <see
        /// cref="ModificationResult"/> indicating the outcome of the operation.</returns>
        public Task<ModificationResult> RemovePlayerAsync(string playerName, CancellationToken ct = default) =>
            client.ExecuteCommandAsync(RemovePlayer(playerName), ct);

        /// <summary>
        /// Retrieves the current whitelist entries asynchronously.
        /// </summary>
        /// <remarks>This method sends a command to retrieve the list of entries in the whitelist. The
        /// operation can be canceled by passing a cancellation token.</remarks>
        /// <param name="ct">A <see cref="CancellationToken"/> that can be used to cancel the operation.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains a <see
        /// cref="ListResult"/> object representing the current whitelist entries.</returns>
        public Task<ListResult> GetPlayersAsync(CancellationToken ct = default) =>
            client.ExecuteCommandAsync(GetPlayers(), ct);

        /// <summary>
        /// Enables the whitelist feature asynchronously.
        /// </summary>
        /// <remarks>This method sends a command to enable the whitelist feature. Ensure that the client
        /// is properly configured before invoking this method.</remarks>
        /// <param name="ct">A <see cref="CancellationToken"/> that can be used to cancel the operation.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the asynchronous operation, containing a <see
        /// cref="ToggleResult"/> that indicates the result of the operation.</returns>
        public Task<ToggleResult> EnableAsync(CancellationToken ct = default) =>
            client.ExecuteCommandAsync(Enable(), ct);

        /// <summary>
        /// Disables the whitelist feature asynchronously.
        /// </summary>
        /// <remarks>This method sends a command to disable the whitelist feature. Ensure that the client
        /// is properly configured before invoking this method.</remarks>
        /// <param name="ct">A <see cref="CancellationToken"/> that can be used to cancel the operation.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains a <see
        /// cref="ToggleResult"/> indicating the outcome of the operation.</returns>
        public Task<ToggleResult> DisableAsync(CancellationToken ct = default) =>
            client.ExecuteCommandAsync(Disable(), ct);

        /// <summary>
        /// Reloads the whitelist configuration asynchronously.
        /// </summary>
        /// <remarks>This method sends a command to reload the whitelist configuration. The operation is
        /// performed asynchronously, and the result of the reload is returned as a string.</remarks>
        /// <param name="ct">A <see cref="CancellationToken"/> that can be used to cancel the operation.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains a string indicating the outcome
        /// of the reload operation.</returns>
        public Task<string> ReloadConfigurationAsync(CancellationToken ct = default) =>
            client.ExecuteCommandAsync(ReloadConfiguration(), ct);
    }
}
