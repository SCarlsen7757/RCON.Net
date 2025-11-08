using RCON.Core.Models;

namespace RCON.Net.Tests.Models
{
    public class RconPacketTests
    {
        [Fact]
        public void Constructor_SetsPropertiesCorrectly()
        {
            // Arrange & Act
            var packet = new RconPacket(1, 2, "test");

            // Assert
            Assert.Equal(1, packet.Id);
            Assert.Equal(2, packet.Type);
            Assert.Equal("test", packet.Body);
        }

        [Fact]
        public void Constructor_WithNullBody_SetsEmptyString()
        {
            // Arrange & Act
            var packet = new RconPacket(1, 2, null!);

            // Assert
            Assert.Equal(string.Empty, packet.Body);
        }

        [Fact]
        public void ToBytes_CreatesValidPacket()
        {
            // Arrange
            var packet = new RconPacket(123, 2, "test command");

            // Act
            var bytes = packet.ToBytes();

            // Assert
            Assert.NotNull(bytes);
            Assert.True(bytes.Length > 0);

            // Check size field (first 4 bytes)
            var size = BitConverter.ToInt32(bytes, 0);
            Assert.Equal(bytes.Length - 4, size);
        }

        [Fact]
        public void FromBytes_ToBytes_RoundTrip()
        {
            // Arrange
            var originalPacket = new RconPacket(456, 3, "test message");

            // Act
            var bytes = originalPacket.ToBytes();
            var reconstructedPacket = RconPacket.FromBytes(bytes);

            // Assert
            Assert.Equal(originalPacket.Id, reconstructedPacket.Id);
            Assert.Equal(originalPacket.Type, reconstructedPacket.Type);
            Assert.Equal(originalPacket.Body, reconstructedPacket.Body);
        }

        [Fact]
        public void FromBytes_WithEmptyBody_ReturnsPacketWithEmptyBody()
        {
            // Arrange
            var originalPacket = new RconPacket(789, 2, string.Empty);

            // Act
            var bytes = originalPacket.ToBytes();
            var reconstructedPacket = RconPacket.FromBytes(bytes);

            // Assert
            Assert.Equal(string.Empty, reconstructedPacket.Body);
        }
    }
}
