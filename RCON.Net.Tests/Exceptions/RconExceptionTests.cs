using RCON.Core.Exceptions;

namespace RCON.Net.Tests.Exceptions
{
    public class RconExceptionTests
    {
        [Fact]
        public void RconException_CanBeCreatedWithMessage()
        {
            // Arrange & Act
            var exception = new RconException("Test error");

            // Assert
            Assert.Equal("Test error", exception.Message);
        }

        [Fact]
        public void RconException_CanBeCreatedWithMessageAndInnerException()
        {
            // Arrange
            var innerException = new Exception("Inner error");

            // Act
            var exception = new RconException("Test error", innerException);

            // Assert
            Assert.Equal("Test error", exception.Message);
            Assert.Same(innerException, exception.InnerException);
        }

        [Fact]
        public void RconAuthenticationException_InheritsFromRconException()
        {
            // Arrange & Act
            var exception = new RconAuthenticationException("Auth failed");

            // Assert
            Assert.IsAssignableFrom<RconException>(exception);
            Assert.Equal("Auth failed", exception.Message);
        }

        [Fact]
        public void RconConnectionException_InheritsFromRconException()
        {
            // Arrange & Act
            var exception = new RconConnectionException("Connection failed");

            // Assert
            Assert.IsAssignableFrom<RconException>(exception);
            Assert.Equal("Connection failed", exception.Message);
        }

        [Fact]
        public void RconConnectionException_CanBeCreatedWithInnerException()
        {
            // Arrange
            var innerException = new Exception("Socket error");

            // Act
            var exception = new RconConnectionException("Connection failed", innerException);

            // Assert
            Assert.Same(innerException, exception.InnerException);
        }

        [Fact]
        public void RconCommandException_InheritsFromRconException()
        {
            // Arrange & Act
            var exception = new RconCommandException("Command failed");

            // Assert
            Assert.IsAssignableFrom<RconException>(exception);
            Assert.Equal("Command failed", exception.Message);
        }

        [Fact]
        public void RconCommandException_CanBeCreatedWithInnerException()
        {
            // Arrange
            var innerException = new Exception("Execution error");

            // Act
            var exception = new RconCommandException("Command failed", innerException);

            // Assert
            Assert.Same(innerException, exception.InnerException);
        }
    }
}
