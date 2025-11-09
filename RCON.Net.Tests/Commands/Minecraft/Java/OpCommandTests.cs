using RCON.Commands.Minecraft.Java.Op;
using RCON.Commands.Minecraft.Java.Op.Models;

namespace RCON.Net.Tests.Commands.Minecraft.Java
{
    public class OpCommandTests
    {
        [Fact]
        public void GiveOperator_WithPlayerName_ReturnsCorrectCommand()
        {
            // Arrange & Act
            var command = OpCommands.GiveOperator("TestPlayer");

            // Assert
            Assert.Equal("op TestPlayer", command.Build());
        }

        [Fact]
        public void GiveOperator_WithNullPlayerName_ThrowsException()
        {
            // Act & Assert
            Assert.Throws<ArgumentException>(() => OpCommands.GiveOperator(null!));
        }

        [Fact]
        public void GiveOperator_WithEmptyPlayerName_ThrowsException()
        {
            // Act & Assert
            Assert.Throws<ArgumentException>(() => OpCommands.GiveOperator(""));
        }

        [Fact]
        public void GiveOperator_Parse_SuccessResponse()
        {
            // Arrange
            var command = OpCommands.GiveOperator("TestPlayer");
            var response = "Made TestPlayer a server operator";

            // Act
            var result = command.Parse(response);

            // Assert
            Assert.Equal(OpStatus.Success, result.Status);
            Assert.Equal("TestPlayer", result.PlayerName);
        }

        [Fact]
        public void GiveOperator_Parse_AlreadyOperatorResponse()
        {
            // Arrange
            var command = OpCommands.GiveOperator("TestPlayer");
            var response = "Nothing changed. The player already is an operator";

            // Act
            var result = command.Parse(response);

            // Assert
            Assert.Equal(OpStatus.NoChange, result.Status);
            Assert.Equal("TestPlayer", result.PlayerName);
        }

        [Fact]
        public void GiveOperator_Parse_PlayerNotFoundResponse()
        {
            // Arrange
            var command = OpCommands.GiveOperator("NonExistent");
            var response = "No player was found";

            // Act
            var result = command.Parse(response);

            // Assert
            Assert.Equal(OpStatus.NotFound, result.Status);
        }

        [Fact]
        public void RevokeOperator_WithPlayerName_ReturnsCorrectCommand()
        {
            // Arrange & Act
            var command = OpCommands.RevokeOperator("TestPlayer");

            // Assert
            Assert.Equal("deop TestPlayer", command.Build());
        }

        [Fact]
        public void RevokeOperator_Parse_SuccessResponse()
        {
            // Arrange
            var command = OpCommands.RevokeOperator("TestPlayer");
            var response = "Made TestPlayer no longer a server operator";

            // Act
            var result = command.Parse(response);

            // Assert
            Assert.Equal(OpStatus.Success, result.Status);
            Assert.Equal("TestPlayer", result.PlayerName);
        }

        [Fact]
        public void RevokeOperator_Parse_NotOperatorResponse()
        {
            // Arrange
            var command = OpCommands.RevokeOperator("TestPlayer");
            var response = "Nothing changed. The player is not an operator";

            // Act
            var result = command.Parse(response);

            // Assert
            Assert.Equal(OpStatus.NoChange, result.Status);
            Assert.Equal("TestPlayer", result.PlayerName);
        }
    }
}
