using RCON.Commands.Minecraft.Java.Ban;
using RCON.Commands.Minecraft.Java.Ban.Models;

namespace RCON.Net.Tests.Commands.Minecraft.Java
{
    public class BanCommandTests
    {
        [Fact]
        public void BanPlayer_WithPlayerName_ReturnsCorrectCommand()
        {
            // Arrange & Act
            var command = BanCommands.BanPlayer("TestPlayer");

            // Assert
            Assert.Equal("ban TestPlayer", command.Build());
        }

        [Fact]
        public void BanPlayer_WithPlayerNameAndReason_ReturnsCorrectCommand()
        {
            // Arrange & Act
            var command = BanCommands.BanPlayer("TestPlayer", "Griefing");

            // Assert
            Assert.Equal("ban TestPlayer Griefing", command.Build());
        }

        [Fact]
        public void BanPlayer_WithNullPlayerName_ThrowsException()
        {
            // Act & Assert
            Assert.Throws<ArgumentException>(() => BanCommands.BanPlayer(null!));
        }

        [Fact]
        public void BanPlayer_WithEmptyPlayerName_ThrowsException()
        {
            // Act & Assert
            Assert.Throws<ArgumentException>(() => BanCommands.BanPlayer(""));
        }

        [Fact]
        public void BanPlayer_Parse_SuccessResponse()
        {
            // Arrange
            var command = BanCommands.BanPlayer("TestPlayer", "Griefing");
            var response = "Banned player TestPlayer";

            // Act
            var result = command.Parse(response);

            // Assert
            Assert.Equal(BanStatus.Success, result.Status);
            Assert.Equal("TestPlayer", result.Target);
            Assert.Equal("Griefing", result.Reason);
        }

        [Fact]
        public void BanPlayer_Parse_AlreadyBannedResponse()
        {
            // Arrange
            var command = BanCommands.BanPlayer("TestPlayer");
            var response = "Player is already banned";

            // Act
            var result = command.Parse(response);

            // Assert
            Assert.Equal(BanStatus.AlreadyBanned, result.Status);
        }

        [Fact]
        public void BanIpAddress_WithIpAndReason_ReturnsCorrectCommand()
        {
            // Arrange & Act
            var command = BanCommands.BanIpAddress("192.168.1.100", "Hacking");

            // Assert
            Assert.Equal("ban-ip 192.168.1.100 Hacking", command.Build());
        }

        [Fact]
        public void BanIpAddress_WithIpOnly_ReturnsCorrectCommand()
        {
            // Arrange & Act
            var command = BanCommands.BanIpAddress("192.168.1.100");

            // Assert
            Assert.Equal("ban-ip 192.168.1.100", command.Build());
        }

        [Fact]
        public void BanIpAddress_Parse_SuccessResponse()
        {
            // Arrange
            var command = BanCommands.BanIpAddress("10.10.10.10", "test");
            var response = "Banned IP 10.10.10.10: test";

            // Act
            var result = command.Parse(response);

            // Assert
            Assert.Equal(BanStatus.Success, result.Status);
            Assert.Equal("10.10.10.10", result.Target);
        }

        [Fact]
        public void BanListPlayers_ReturnsCorrectCommand()
        {
            // Arrange & Act
            var command = BanCommands.GetBanListPlayers();

            // Assert
            Assert.Equal("banlist players", command.Build());
        }

        [Fact]
        public void BanListPlayers_Parse_MultiplePlayersResponse()
        {
            // Arrange
            var command = BanCommands.GetBanListPlayers();
            var response = "There are 1 ban(s):TestPlayer was banned by Rcon: ips";

            // Act
            var result = command.Parse(response);

            // Assert
            Assert.Equal(1, result.Count);
            Assert.Contains("TestPlayer", result.Entries);
        }

        [Fact]
        public void BanListPlayers_Parse_NoPlayersResponse()
        {
            // Arrange
            var command = BanCommands.GetBanListPlayers();
            var response = "There are no bans";

            // Act
            var result = command.Parse(response);

            // Assert
            Assert.Equal(0, result.Count);
        }

        [Fact]
        public void BanListIps_ReturnsCorrectCommand()
        {
            // Arrange & Act
            var command = BanCommands.GetBanListIps();

            // Assert
            Assert.Equal("banlist ips", command.Build());
        }

        [Fact]
        public void BanListIps_Parse_MultipleIpsResponse()
        {
            // Arrange
            var command = BanCommands.GetBanListIps();
            var response = "There are 1 ban(s):10.10.10.10 was banned by Rcon: test";

            // Act
            var result = command.Parse(response);

            // Assert
            Assert.Equal(1, result.Count);
            Assert.Contains("10.10.10.10", result.Entries);
        }

        [Fact]
        public void PardonPlayer_WithPlayerName_ReturnsCorrectCommand()
        {
            // Arrange & Act
            var command = BanCommands.PardonPlayer("TestPlayer");

            // Assert
            Assert.Equal("pardon TestPlayer", command.Build());
        }

        [Fact]
        public void PardonPlayer_Parse_SuccessResponse()
        {
            // Arrange
            var command = BanCommands.PardonPlayer("TestPlayer");
            var response = "Unbanned TestPlayer";

            // Act
            var result = command.Parse(response);

            // Assert
            Assert.Equal(BanStatus.Success, result.Status);
            Assert.Equal("TestPlayer", result.Target);
        }

        [Fact]
        public void PardonPlayer_Parse_NotBannedResponse()
        {
            // Arrange
            var command = BanCommands.PardonPlayer("TestPlayer");
            var response = "Player is not banned";

            // Act
            var result = command.Parse(response);

            // Assert
            Assert.Equal(BanStatus.NotFound, result.Status);
        }

        [Fact]
        public void PardonIpAddress_WithIpAddress_ReturnsCorrectCommand()
        {
            // Arrange & Act
            var command = BanCommands.PardonIpAddress("10.10.10.10");

            // Assert
            Assert.Equal("pardon-ip 10.10.10.10", command.Build());
        }

        [Fact]
        public void PardonIpAddress_Parse_SuccessResponse()
        {
            // Arrange
            var command = BanCommands.PardonIpAddress("10.10.10.10");
            var response = "Unbanned IP 10.10.10.10";

            // Act
            var result = command.Parse(response);

            // Assert
            Assert.Equal(BanStatus.Success, result.Status);
            Assert.Equal("10.10.10.10", result.Target);
        }
    }
}
