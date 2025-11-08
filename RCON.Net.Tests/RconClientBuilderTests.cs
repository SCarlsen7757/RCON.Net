using RCON.Core;

namespace RCON.Net.Tests
{
    public class RconClientBuilderTests
    {
        [Fact]
        public void Create_ReturnsNewBuilder()
        {
            // Act
            var builder = RconClientBuilder.Create();

            // Assert
            Assert.NotNull(builder);
        }

        [Fact]
        public void WithHost_SetsHost()
        {
            // Arrange
            var builder = RconClientBuilder.Create();

            // Act
            var result = builder.WithHost("localhost");

            // Assert
            Assert.Same(builder, result); // Should return same instance for fluent API
        }

        [Fact]
        public void WithPort_SetsPort()
        {
            // Arrange
            var builder = RconClientBuilder.Create();

            // Act
            var result = builder.WithPort(25575);

            // Assert
            Assert.Same(builder, result);
        }

        [Fact]
        public void WithPort_InvalidPort_ThrowsException()
        {
            // Arrange
            var builder = RconClientBuilder.Create();

            // Act & Assert
            Assert.Throws<ArgumentOutOfRangeException>(() => builder.WithPort(0));
            Assert.Throws<ArgumentOutOfRangeException>(() => builder.WithPort(-1));
            Assert.Throws<ArgumentOutOfRangeException>(() => builder.WithPort(65536));
        }

        [Fact]
        public void WithPassword_SetsPassword()
        {
            // Arrange
            var builder = RconClientBuilder.Create();

            // Act
            var result = builder.WithPassword("secret");

            // Assert
            Assert.Same(builder, result);
        }

        [Fact]
        public void WithTimeout_SetsTimeout()
        {
            // Arrange
            var builder = RconClientBuilder.Create();

            // Act
            var result = builder.WithTimeout(10000);

            // Assert
            Assert.Same(builder, result);
        }

        [Fact]
        public void WithTimeout_InvalidTimeout_ThrowsException()
        {
            // Arrange
            var builder = RconClientBuilder.Create();

            // Act & Assert
            Assert.Throws<ArgumentOutOfRangeException>(() => builder.WithTimeout(0));
            Assert.Throws<ArgumentOutOfRangeException>(() => builder.WithTimeout(-1));
        }

        [Fact]
        public void Build_WithoutHost_ThrowsException()
        {
            // Arrange
            var builder = RconClientBuilder.Create()
             .WithPassword("secret");

            // Act & Assert
            Assert.Throws<InvalidOperationException>(() => builder.Build());
        }

        [Fact]
        public void Build_WithoutPassword_ThrowsException()
        {
            // Arrange
            var builder = RconClientBuilder.Create()
              .WithHost("localhost");

            // Act & Assert
            Assert.Throws<InvalidOperationException>(() => builder.Build());
        }

        [Fact]
        public void Build_WithValidConfiguration_ReturnsClient()
        {
            // Arrange
            var builder = RconClientBuilder.Create()
           .WithHost("localhost")
                .WithPort(25575)
       .WithPassword("secret")
          .WithTimeout(5000);

            // Act
            var client = builder.Build();

            // Assert
            Assert.NotNull(client);
            Assert.False(client.IsConnected);
        }

        [Fact]
        public void FluentAPI_ChainsCorrectly()
        {
            // Act
            var client = RconClientBuilder.Create()
                .WithHost("localhost")
       .WithPort(25575)
              .WithPassword("secret")
       .WithTimeout(10000)
      .Build();

            // Assert
            Assert.NotNull(client);
        }
    }
}
