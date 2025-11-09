using RCON.Commands.Minecraft.Java.Difficulty;
using RCON.Commands.Minecraft.Java.Difficulty.Models;

namespace RCON.Net.Tests.Commands.Minecraft.Java
{
    public class DifficultyCommandTests
    {
        [Fact]
        public void SetDifficulty_WithPeaceful_ReturnsCorrectCommand()
        {
            // Arrange & Act
            var command = DifficultyCommands.SetDifficulty(DifficultyLevel.Peaceful);

            // Assert
            Assert.Equal("difficulty peaceful", command.Build());
        }

        [Fact]
        public void SetDifficulty_WithEasy_ReturnsCorrectCommand()
        {
            // Arrange & Act
            var command = DifficultyCommands.SetDifficulty(DifficultyLevel.Easy);

            // Assert
            Assert.Equal("difficulty easy", command.Build());
        }

        [Fact]
        public void SetDifficulty_WithNormal_ReturnsCorrectCommand()
        {
            // Arrange & Act
            var command = DifficultyCommands.SetDifficulty(DifficultyLevel.Normal);

            // Assert
            Assert.Equal("difficulty normal", command.Build());
        }

        [Fact]
        public void SetDifficulty_WithHard_ReturnsCorrectCommand()
        {
            // Arrange & Act
            var command = DifficultyCommands.SetDifficulty(DifficultyLevel.Hard);

            // Assert
            Assert.Equal("difficulty hard", command.Build());
        }

        [Fact]
        public void SetDifficulty_Parse_NormalResponse()
        {
            // Arrange
            var command = DifficultyCommands.SetDifficulty(DifficultyLevel.Normal);
            var response = "The difficulty has been set to Normal";

            // Act
            var result = command.Parse(response);

            // Assert
            Assert.Equal(DifficultyLevel.Normal, result.Difficulty);
            Assert.True(result.Changed);
        }

        [Fact]
        public void SetDifficulty_Parse_HardResponse()
        {
            // Arrange
            var command = DifficultyCommands.SetDifficulty(DifficultyLevel.Hard);
            var response = "The difficulty has been set to Hard";

            // Act
            var result = command.Parse(response);

            // Assert
            Assert.Equal(DifficultyLevel.Hard, result.Difficulty);
            Assert.True(result.Changed);
        }

        [Fact]
        public void SetDifficulty_Parse_EasyResponse()
        {
            // Arrange
            var command = DifficultyCommands.SetDifficulty(DifficultyLevel.Easy);
            var response = "The difficulty has been set to Easy";

            // Act
            var result = command.Parse(response);

            // Assert
            Assert.Equal(DifficultyLevel.Easy, result.Difficulty);
            Assert.True(result.Changed);
        }

        [Fact]
        public void SetDifficulty_Parse_PeacefulResponse()
        {
            // Arrange
            var command = DifficultyCommands.SetDifficulty(DifficultyLevel.Peaceful);
            var response = "The difficulty has been set to Peaceful";

            // Act
            var result = command.Parse(response);

            // Assert
            Assert.Equal(DifficultyLevel.Peaceful, result.Difficulty);
            Assert.True(result.Changed);
        }

        [Fact]
        public void SetDifficulty_Parse_AlreadySetResponse()
        {
            // Arrange
            var command = DifficultyCommands.SetDifficulty(DifficultyLevel.Hard);
            var response = "The difficulty did not change; it is already set to hard";

            // Act
            var result = command.Parse(response);

            // Assert
            Assert.Equal(DifficultyLevel.Hard, result.Difficulty);
            Assert.False(result.Changed);
        }
    }
}
