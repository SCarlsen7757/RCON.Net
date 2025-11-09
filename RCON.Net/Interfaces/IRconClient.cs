using RCON.Core.Models;

namespace RCON.Core.Interfaces
{
    /// <summary>
    /// Interface for RCON client operations
    /// </summary>
    public interface IRconClient : IDisposable
    {
        /// <summary>
        /// Gets whether the client is currently connected
        /// </summary>
        bool IsConnected { get; }

        /// <summary>
        /// Connects to the RCON server and authenticates
        /// </summary>
        Task ConnectAsync(CancellationToken cancellationToken = default);

        /// <summary>
        /// Disconnects from the RCON server
        /// </summary>
        Task DisconnectAsync();

        /// <summary>
        /// Sends a command to the RCON server
        /// </summary>
        Task<RconResponse> SendCommandAsync(string command, CancellationToken cancellationToken = default);

        /// <summary>
        /// Sends a command and returns the raw response string
        /// </summary>
        Task<string> ExecuteCommandAsync(string command, CancellationToken cancellationToken = default);

        /// <summary>
        /// Executes the specified command asynchronously and returns the result.
        /// </summary>
        /// <typeparam name="T">The type of the result returned by the command.</typeparam>
        /// <param name="command">The command to execute. Must not be <see langword="null"/>.</param>
        /// <param name="cancellationToken">A token to monitor for cancellation requests. The default value is <see cref="CancellationToken.None"/>.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the result of the command
        /// execution.</returns>
        Task<T> ExecuteCommandAsync<T>(ICommand<T> command, CancellationToken cancellationToken = default);
    }
}
