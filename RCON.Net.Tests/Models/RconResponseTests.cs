using RCON.Core.Models;

namespace RCON.Net.Tests.Models
{
    public class RconResponseTests
    {
        [Fact]
        public void Constructor_SetsPropertiesCorrectly()
        {
            // Arrange & Act
            var response = new RconResponse(true, "Success message", null);

            // Assert
            Assert.True(response.Success);
            Assert.Equal("Success message", response.Message);
            Assert.Null(response.ErrorMessage);
        }

        [Fact]
        public void Successful_CreatesSuccessfulResponse()
        {
            // Arrange & Act
            var response = RconResponse.Successful("Command executed");

            // Assert
            Assert.True(response.Success);
            Assert.Equal("Command executed", response.Message);
            Assert.Null(response.ErrorMessage);
        }

        [Fact]
        public void Failed_CreatesFailedResponse()
        {
            // Arrange & Act
            var response = RconResponse.Failed("Connection error");

            // Assert
            Assert.False(response.Success);
            Assert.Equal(string.Empty, response.Message);
            Assert.Equal("Connection error", response.ErrorMessage);
        }
    }
}
