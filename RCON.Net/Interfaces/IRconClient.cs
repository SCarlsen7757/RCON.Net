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
    }
}
