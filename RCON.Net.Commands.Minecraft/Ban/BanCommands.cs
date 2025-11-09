using RCON.Commands.Minecraft.Java.Ban.Models;
using RCON.Core.Interfaces;

namespace RCON.Commands.Minecraft.Java.Ban
{
    /// <summary>
    /// Provides a set of commands for managing bans, including banning players/IPs, listing bans, and pardoning players/IPs.
    /// </summary>
    /// <remarks>This class offers both synchronous and asynchronous methods to interact with the ban system.
    /// Commands are created using static methods and can be executed to perform the desired operations.</remarks>
    public class BanCommands
    {
        private readonly IRconClient client;
        public BanCommands(IRconClient client) => this.client = client;

        /// <summary>
        /// Creates a command to ban a player.
        /// </summary>
        /// <param name="playerName">The name of the player to ban. Cannot be null, empty, or consist only of whitespace.</param>
        /// <param name="reason">Optional reason for the ban.</param>
        /// <returns>A command that, when executed, bans the specified player and returns the result of the operation.</returns>
        /// <exception cref="ArgumentException">Thrown if <paramref name="playerName"/> is null, empty, or consists only of whitespace.</exception>
        public static ICommand<BanResult> BanPlayer(string playerName, string reason = "")
        {
            if (string.IsNullOrWhiteSpace(playerName))
                throw new ArgumentException("Player name cannot be null or empty", nameof(playerName));

            return new Commands.Ban(playerName, reason);
        }

        /// <summary>
        /// Creates a command to ban an IP address.
        /// </summary>
        /// <param name="ipAddress">The IP address to ban. Cannot be null, empty, or consist only of whitespace.</param>
        /// <param name="reason">Optional reason for the ban.</param>
        /// <returns>A command that, when executed, bans the specified IP address and returns the result of the operation.</returns>
        /// <exception cref="ArgumentException">Thrown if <paramref name="ipAddress"/> is null, empty, or consists only of whitespace.</exception>
        public static ICommand<BanResult> BanIpAddress(string ipAddress, string reason = "")
        {
            if (string.IsNullOrWhiteSpace(ipAddress))
                throw new ArgumentException("IP address cannot be null or empty", nameof(ipAddress));

            return new Commands.BanIp(ipAddress, reason);
        }

        /// <summary>
        /// Creates a command to list banned IP addresses.
        /// </summary>
        /// <returns>A command that, when executed, returns the list of banned entries.</returns>
        public static ICommand<BanListResult> GetBanListIps()
        {
            return new Commands.BanListIps();
        }

        /// <summary>
        /// Creates a command to list banned players .
        /// </summary>
        /// <returns>A command that, when executed, returns the list of banned entries.</returns>
        public static ICommand<BanListResult> GetBanListPlayers()
        {
            return new Commands.BanListPlayers();
        }

        /// <summary>
        /// Creates a command to pardon a banned player.
        /// </summary>
        /// <param name="playerName">The name of the player to pardon. Cannot be null, empty, or consist only of whitespace.</param>
        /// <returns>A command that, when executed, pardons the specified player and returns the result of the operation.</returns>
        /// <exception cref="ArgumentException">Thrown if <paramref name="playerName"/> is null, empty, or consists only of whitespace.</exception>
        public static ICommand<BanResult> PardonPlayer(string playerName)
        {
            if (string.IsNullOrWhiteSpace(playerName))
                throw new ArgumentException("Player name cannot be null or empty", nameof(playerName));

            return new Commands.Pardon(playerName);
        }

        /// <summary>
        /// Creates a command to pardon a banned IP address.
        /// </summary>
        /// <param name="ipAddress">The IP address to pardon. Cannot be null, empty, or consist only of whitespace.</param>
        /// <returns>A command that, when executed, pardons the specified IP address and returns the result of the operation.</returns>
        /// <exception cref="ArgumentException">Thrown if <paramref name="ipAddress"/> is null, empty, or consists only of whitespace.</exception>
        public static ICommand<BanResult> PardonIpAddress(string ipAddress)
        {
            if (string.IsNullOrWhiteSpace(ipAddress))
                throw new ArgumentException("IP address cannot be null or empty", nameof(ipAddress));

            return new Commands.PardonIp(ipAddress);
        }

        /// <summary>
        /// Bans a player asynchronously.
        /// </summary>
        /// <param name="playerName">The name of the player to ban.</param>
        /// <param name="reason">Optional reason for the ban.</param>
        /// <param name="ct">An optional <see cref="CancellationToken"/> to observe while waiting for the operation to complete.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the asynchronous operation. The task result contains a <see cref="BanResult"/> indicating the outcome of the operation.</returns>
        public Task<BanResult> BanPlayerAsync(string playerName, string reason, CancellationToken ct = default) =>
            client.ExecuteCommandAsync(BanPlayer(playerName, reason), ct);

        /// <summary>
        /// Bans an IP address asynchronously.
        /// </summary>
        /// <param name="ipAddress">The IP address to ban.</param>
        /// <param name="reason">Optional reason for the ban.</param>
        /// <param name="ct">An optional <see cref="CancellationToken"/> to observe while waiting for the operation to complete.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the asynchronous operation. The task result contains a <see cref="BanResult"/> indicating the outcome of the operation.</returns>
        public Task<BanResult> BanIpAddressAsync(string ipAddress, string reason, CancellationToken ct = default) =>
            client.ExecuteCommandAsync(BanIpAddress(ipAddress, reason), ct);

        /// <summary>
        /// Retrieves the list of banned players asynchronously.
        /// </summary>
        /// <param name="ct">A <see cref="CancellationToken"/> that can be used to cancel the operation.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains a <see cref="BanListResult"/> object.</returns>
        public Task<BanListResult> GetBanListIpsAsync(CancellationToken ct = default) =>
            client.ExecuteCommandAsync(GetBanListIps(), ct);

        /// <summary>
        /// Retrieves the list of banned IP addresses asynchronously.
        /// </summary>
        /// <param name="ct">A <see cref="CancellationToken"/> that can be used to cancel the operation.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains a <see cref="BanListResult"/> object.</returns>
        public Task<BanListResult> GetBanListPlayersAsync(CancellationToken ct = default) =>
            client.ExecuteCommandAsync(GetBanListPlayers(), ct);

        /// <summary>
        /// Pardons a banned player asynchronously.
        /// </summary>
        /// <param name="playerName">The name of the player to pardon.</param>
        /// <param name="ct">A <see cref="CancellationToken"/> that can be used to cancel the operation.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the asynchronous operation. The task result contains a <see cref="BanResult"/> indicating the outcome of the operation.</returns>
        public Task<BanResult> PardonPlayerAsync(string playerName, CancellationToken ct = default) =>
            client.ExecuteCommandAsync(PardonPlayer(playerName), ct);

        /// <summary>
        /// Pardons a banned IP address asynchronously.
        /// </summary>
        /// <param name="ipAddress">The IP address to pardon.</param>
        /// <param name="ct">A <see cref="CancellationToken"/> that can be used to cancel the operation.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the asynchronous operation. The task result contains a <see cref="BanResult"/> indicating the outcome of the operation.</returns>
        public Task<BanResult> PardonIpAddressAsync(string ipAddress, CancellationToken ct = default) =>
            client.ExecuteCommandAsync(PardonIpAddress(ipAddress), ct);
    }
}
