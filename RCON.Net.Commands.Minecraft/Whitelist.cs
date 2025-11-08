using RCON.Commands;
using RCON.Core.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace RCON.Commands.Minecraft.Java
{
    /// <summary>
    /// Represents the result of a whitelist list operation
    /// </summary>
    public class WhitelistListResult
    {
        /// <summary>
        /// Gets the list of whitelisted players
        /// </summary>
        public IReadOnlyList<string> Players { get; }

        /// <summary>
        /// Gets the count of whitelisted players
        /// </summary>
        public int Count => Players.Count;

        /// <summary>
        /// Initializes a new instance of the WhitelistListResult class
        /// </summary>
        public WhitelistListResult(IReadOnlyList<string> players)
        {
            Players = players ?? new List<string>();
        }
    }

    /// <summary>
    /// Represents the result of a whitelist add/remove operation
    /// </summary>
    public class WhitelistModificationResult
    {
        /// <summary>
        /// Gets the status of the operation
        /// </summary>
        public WhitelistModificationStatus Status { get; }

        /// <summary>
        /// Gets the player name involved in the operation
        /// </summary>
        public string PlayerName { get; }

        /// <summary>
        /// Gets the message from the server
        /// </summary>
        public string Message { get; }

        /// <summary>
        /// Initializes a new instance of the WhitelistModificationResult class
        /// </summary>
        public WhitelistModificationResult(WhitelistModificationStatus status, string playerName, string message)
        {
            Status = status;
            PlayerName = playerName;
            Message = message;
        }
    }

    /// <summary>
    /// Represents the status of a whitelist modification operation
    /// </summary>
    public enum WhitelistModificationStatus
    {
        /// <summary>
        /// The operation was successful
        /// </summary>
        Success,

        /// <summary>
        /// The player was already on the whitelist (add operation)
        /// </summary>
        AlreadyWhitelisted,

        /// <summary>
        /// The player is not on the whitelist (remove operation)
        /// </summary>
        NotWhitelisted,

        /// <summary>
        /// The player does not exist
        /// </summary>
        PlayerDoesNotExist,

        /// <summary>
        /// Unknown status
        /// </summary>
        Unknown
    }

    /// <summary>
    /// Represents the result of a whitelist on/off operation
    /// </summary>
    public class WhitelistToggleResult
    {
        /// <summary>
        /// Gets whether the whitelist is now enabled
        /// </summary>
        public bool IsEnabled { get; }

        /// <summary>
        /// Gets whether the state was changed
        /// </summary>
        public bool StateChanged { get; }

        /// <summary>
        /// Gets the message from the server
        /// </summary>
        public string Message { get; }

        /// <summary>
        /// Initializes a new instance of the WhitelistToggleResult class
        /// </summary>
        public WhitelistToggleResult(bool isEnabled, bool stateChanged, string message)
        {
            IsEnabled = isEnabled;
            StateChanged = stateChanged;
            Message = message;
        }
    }

    /// <summary>
    /// Whitelist command for Minecraft Java servers
    /// </summary>
    public static class WhitelistCommand
    {
        /// <summary>
        /// Creates a command to list all whitelisted players
        /// </summary>
        public static ICommand<WhitelistListResult> List()
        {
            return new WhitelistListCommand();
        }

        /// <summary>
        /// Creates a command to add a player to the whitelist
        /// </summary>
        public static ICommand<WhitelistModificationResult> Add(string playerName)
        {
            if (string.IsNullOrWhiteSpace(playerName))
                throw new ArgumentException("Player name cannot be null or empty", nameof(playerName));

            return new WhitelistAddCommand(playerName);
        }

        /// <summary>
        /// Creates a command to remove a player from the whitelist
        /// </summary>
        public static ICommand<WhitelistModificationResult> Remove(string playerName)
        {
            if (string.IsNullOrWhiteSpace(playerName))
                throw new ArgumentException("Player name cannot be null or empty", nameof(playerName));

            return new WhitelistRemoveCommand(playerName);
        }

        /// <summary>
        /// Creates a command to enable the whitelist
        /// </summary>
        public static ICommand<WhitelistToggleResult> On()
        {
            return new WhitelistOnCommand();
        }

        /// <summary>
        /// Creates a command to disable the whitelist
        /// </summary>
        public static ICommand<WhitelistToggleResult> Off()
        {
            return new WhitelistOffCommand();
        }

        /// <summary>
        /// Creates a command to reload the whitelist
        /// </summary>
        public static ICommand<string> Reload()
        {
            return new WhitelistReloadCommand();
        }
    }

    /// <summary>
    /// Command to list whitelisted players
    /// </summary>
    internal class WhitelistListCommand : CommandBase<WhitelistListResult>
    {
        /// <inheritdoc/>
        public override string Build() => "whitelist list";

        /// <inheritdoc/>
        public override WhitelistListResult Parse(string response)
        {
            // Expected format: "There are X whitelisted player(s): player1, player2, ..."
            var players = new List<string>();

            if (string.IsNullOrEmpty(response))
                return new WhitelistListResult(players.AsReadOnly());

            // Check if there are players listed
            if (!response.Contains("whitelisted player"))
                return new WhitelistListResult(players.AsReadOnly());

            // Extract the part after the colon
            var colonIndex = response.IndexOf(':');
            if (colonIndex == -1)
                return new WhitelistListResult(players.AsReadOnly());

            var playersPart = response.Substring(colonIndex + 1).Trim();
            if (string.IsNullOrEmpty(playersPart))
                return new WhitelistListResult(players.AsReadOnly());

            // Split by comma and trim each player name
            var playerNames = playersPart.Split(',')
                .Select(p => p.Trim())
                .Where(p => !string.IsNullOrEmpty(p))
                .ToList();

            return new WhitelistListResult(playerNames.AsReadOnly());
        }
    }

    /// <summary>
    /// Command to add a player to the whitelist
    /// </summary>
    internal class WhitelistAddCommand : CommandBase<WhitelistModificationResult>
    {
        private readonly string playerName;

        public WhitelistAddCommand(string playerName)
        {
            this.playerName = playerName;
        }

        /// <inheritdoc/>
        public override string Build() => $"whitelist add {playerName}";

        /// <inheritdoc/>
        public override WhitelistModificationResult Parse(string response)
        {
            if (response.Contains("Added"))
            {
                return new WhitelistModificationResult(
                    WhitelistModificationStatus.Success,
                    playerName,
                    response);
            }
            else if (response.Contains("already whitelisted"))
            {
                return new WhitelistModificationResult(
                    WhitelistModificationStatus.AlreadyWhitelisted,
                    playerName,
                    response);
            }
            else if (response.Contains("does not exist"))
            {
                return new WhitelistModificationResult(
                    WhitelistModificationStatus.PlayerDoesNotExist,
                    playerName,
                    response);
            }

            return new WhitelistModificationResult(
                WhitelistModificationStatus.Unknown,
                playerName,
                response);
        }
    }

    /// <summary>
    /// Command to remove a player from the whitelist
    /// </summary>
    internal class WhitelistRemoveCommand : CommandBase<WhitelistModificationResult>
    {
        private readonly string playerName;

        public WhitelistRemoveCommand(string playerName)
        {
            this.playerName = playerName;
        }

        /// <inheritdoc/>
        public override string Build() => $"whitelist remove {playerName}";

        /// <inheritdoc/>
        public override WhitelistModificationResult Parse(string response)
        {
            if (response.Contains("Removed"))
            {
                return new WhitelistModificationResult(
                    WhitelistModificationStatus.Success,
                    playerName,
                    response);
            }
            else if (response.Contains("not whitelisted"))
            {
                return new WhitelistModificationResult(
                    WhitelistModificationStatus.NotWhitelisted,
                    playerName,
                    response);
            }

            return new WhitelistModificationResult(
                WhitelistModificationStatus.Unknown,
                playerName,
                response);
        }
    }

    /// <summary>
    /// Command to enable the whitelist
    /// </summary>
    internal class WhitelistOnCommand : CommandBase<WhitelistToggleResult>
    {
        /// <inheritdoc/>
        public override string Build() => "whitelist on";

        /// <inheritdoc/>
        public override WhitelistToggleResult Parse(string response)
        {
            if (response.Contains("already turned on"))
            {
                return new WhitelistToggleResult(true, false, response);
            }
            else if (response.Contains("now turned on"))
            {
                return new WhitelistToggleResult(true, true, response);
            }

            return new WhitelistToggleResult(true, false, response);
        }
    }

    /// <summary>
    /// Command to disable the whitelist
    /// </summary>
    internal class WhitelistOffCommand : CommandBase<WhitelistToggleResult>
    {
        /// <inheritdoc/>
        public override string Build() => "whitelist off";

        /// <inheritdoc/>
        public override WhitelistToggleResult Parse(string response)
        {
            if (response.Contains("already turned off"))
            {
                return new WhitelistToggleResult(false, false, response);
            }
            else if (response.Contains("now turned off"))
            {
                return new WhitelistToggleResult(false, true, response);
            }

            return new WhitelistToggleResult(false, false, response);
        }
    }

    /// <summary>
    /// Command to reload the whitelist
    /// </summary>
    internal class WhitelistReloadCommand : CommandBase<string>
    {
        /// <inheritdoc/>
        public override string Build() => "whitelist reload";

        /// <inheritdoc/>
        public override string Parse(string response) => response;
    }
}
