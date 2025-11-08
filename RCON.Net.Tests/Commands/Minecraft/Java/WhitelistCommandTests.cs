using RCON.Commands.Minecraft.Java;

namespace RCON.Net.Tests.Commands.Minecraft.Java
{
    public class WhitelistCommandTests
    {
        [Fact]
        public void Add_WithPlayerName_ReturnsCorrectCommand()
        {
            // Arrange & Act
            var command = WhitelistCommand.Add("Player1");

            // Assert
            Assert.Equal("whitelist add Player1", command.Build());
        }

        [Fact]
        public void Add_WithNullPlayerName_ThrowsException()
        {
            // Act & Assert
            Assert.Throws<ArgumentException>(() => WhitelistCommand.Add(null!));
        }

        [Fact]
        public void Add_WithEmptyPlayerName_ThrowsException()
        {
            // Act & Assert
            Assert.Throws<ArgumentException>(() => WhitelistCommand.Add(""));
        }

        [Fact]
        public void Add_WithWhitespacePlayerName_ThrowsException()
        {
            // Act & Assert
            Assert.Throws<ArgumentException>(() => WhitelistCommand.Add("   "));
        }

        [Fact]
        public void Add_Parse_SuccessResponse()
        {
            // Arrange
            var command = WhitelistCommand.Add("Player1");
            var response = "Added Player1 to the whitelist";

            // Act
            var result = command.Parse(response);

            // Assert
            Assert.Equal(WhitelistModificationStatus.Success, result.Status);
            Assert.Equal("Player1", result.PlayerName);
            Assert.Contains("Added", result.Message);
        }

        [Fact]
        public void Add_Parse_AlreadyWhitelistedResponse()
        {
            // Arrange
            var command = WhitelistCommand.Add("Player1");
            var response = "Player is already whitelisted";

            // Act
            var result = command.Parse(response);

            // Assert
            Assert.Equal(WhitelistModificationStatus.AlreadyWhitelisted, result.Status);
            Assert.Equal("Player1", result.PlayerName);
        }

        [Fact]
        public void Add_Parse_PlayerDoesNotExistResponse()
        {
            // Arrange
            var command = WhitelistCommand.Add("Player7");
            var response = "That player does not exist";

            // Act
            var result = command.Parse(response);

            // Assert
            Assert.Equal(WhitelistModificationStatus.PlayerDoesNotExist, result.Status);
            Assert.Equal("Player7", result.PlayerName);
        }

        [Fact]
        public void List_ReturnsCorrectCommand()
        {
            // Arrange & Act
            var command = WhitelistCommand.List();

            // Assert
            Assert.Equal("whitelist list", command.Build());
        }

        [Fact]
        public void List_Parse_MultiplePlayersResponse()
        {
            // Arrange
            var command = WhitelistCommand.List();
            var response = "There are 2 whitelisted player(s): Player1, Player2";

            // Act
            var result = command.Parse(response);

            // Assert
            Assert.Equal(2, result.Count);
            Assert.Contains("Player1", result.Players);
            Assert.Contains("Player2", result.Players);
        }

        [Fact]
        public void List_Parse_NoPlayersResponse()
        {
            // Arrange
            var command = WhitelistCommand.List();
            var response = "There are 0 whitelisted player(s): ";

            // Act
            var result = command.Parse(response);

            // Assert
            Assert.Equal(0, result.Count);
        }

        [Fact]
        public void List_Parse_EmptyResponse()
        {
            // Arrange
            var command = WhitelistCommand.List();
            var response = "";

            // Act
            var result = command.Parse(response);

            // Assert
            Assert.Equal(0, result.Count);
        }

        [Fact]
        public void Off_ReturnsCorrectCommand()
        {
            // Arrange & Act
            var command = WhitelistCommand.Off();

            // Assert
            Assert.Equal("whitelist off", command.Build());
        }

        [Fact]
        public void Off_Parse_WhitelistTurnedOffResponse()
        {
            // Arrange
            var command = WhitelistCommand.Off();
            var response = "Whitelist is now turned off";

            // Act
            var result = command.Parse(response);

            // Assert
            Assert.False(result.IsEnabled);
            Assert.True(result.StateChanged);
            Assert.Contains("turned off", result.Message);
        }

        [Fact]
        public void Off_Parse_WhitelistAlreadyOffResponse()
        {
            // Arrange
            var command = WhitelistCommand.Off();
            var response = "Whitelist is already turned off";

            // Act
            var result = command.Parse(response);

            // Assert
            Assert.False(result.IsEnabled);
            Assert.False(result.StateChanged);
        }

        [Fact]
        public void On_ReturnsCorrectCommand()
        {
            // Arrange & Act
            var command = WhitelistCommand.On();

            // Assert
            Assert.Equal("whitelist on", command.Build());
        }

        [Fact]
        public void On_Parse_WhitelistTurnedOnResponse()
        {
            // Arrange
            var command = WhitelistCommand.On();
            var response = "Whitelist is now turned on";

            // Act
            var result = command.Parse(response);

            // Assert
            Assert.True(result.IsEnabled);
            Assert.True(result.StateChanged);
            Assert.Contains("turned on", result.Message);
        }

        [Fact]
        public void On_Parse_WhitelistAlreadyOnResponse()
        {
            // Arrange
            var command = WhitelistCommand.On();
            var response = "Whitelist is already turned on";

            // Act
            var result = command.Parse(response);

            // Assert
            Assert.True(result.IsEnabled);
            Assert.False(result.StateChanged);
        }

        [Fact]
        public void Reload_ReturnsCorrectCommand()
        {
            // Arrange & Act
            var command = WhitelistCommand.Reload();

            // Assert
            Assert.Equal("whitelist reload", command.Build());
        }

        [Fact]
        public void Reload_Parse_ReloadedResponse()
        {
            // Arrange
            var command = WhitelistCommand.Reload();
            var response = "Reloaded the whitelist";

            // Act
            var result = command.Parse(response);

            // Assert
            Assert.Equal("Reloaded the whitelist", result);
        }

        [Fact]
        public void Remove_WithPlayerName_ReturnsCorrectCommand()
        {
            // Arrange & Act
            var command = WhitelistCommand.Remove("Player1");

            // Assert
            Assert.Equal("whitelist remove Player1", command.Build());
        }

        [Fact]
        public void Remove_WithNullPlayerName_ThrowsException()
        {
            // Act & Assert
            Assert.Throws<ArgumentException>(() => WhitelistCommand.Remove(null!));
        }

        [Fact]
        public void Remove_WithEmptyPlayerName_ThrowsException()
        {
            // Act & Assert
            Assert.Throws<ArgumentException>(() => WhitelistCommand.Remove(""));
        }

        [Fact]
        public void Remove_Parse_SuccessResponse()
        {
            // Arrange
            var command = WhitelistCommand.Remove("Player1");
            var response = "Removed Player1 from the whitelist";

            // Act
            var result = command.Parse(response);

            // Assert
            Assert.Equal(WhitelistModificationStatus.Success, result.Status);
            Assert.Equal("Player1", result.PlayerName);
        }

        [Fact]
        public void Remove_Parse_PlayerNotWhitelistedResponse()
        {
            // Arrange
            var command = WhitelistCommand.Remove("tets");
            var response = "Player is not whitelisted";

            // Act
            var result = command.Parse(response);

            // Assert
            Assert.Equal(WhitelistModificationStatus.NotWhitelisted, result.Status);
            Assert.Equal("tets", result.PlayerName);
        }
    }
}
