using RCON.Commands.Minecraft.Java.Server;

namespace RCON.Net.Tests.Commands.Minecraft.Java
{
    public class ServerCommandTests
    {
        [Fact]
        public void VersionCommand_BuildsCorrectly()
        {
            var cmd = ServerCommands.GetVersion();
            Assert.Equal("version", cmd.Build());
        }

        [Fact]
        public void VersionCommand_Parse_WellFormedResponse()
        {
            DateTime buildTime = new(2025, 10, 07, 09, 14, 11, DateTimeKind.Utc);
            var response = "Server version info:id =1.21.10 name =1.21.10 data =4556 series = main protocol =773 (0x305) build_time = Tue Oct0709:14:11 UTC2025 pack_resource =69.0 pack_data =88.0 stable = yes";
            var cmd = ServerCommands.GetVersion();
            var result = cmd.Parse(response);

            Assert.Equal("1.21.10", result.Id);
            Assert.Equal("1.21.10", result.Name);
            Assert.Equal(4556, result.Data);
            Assert.Equal("main", result.Series);
            Assert.Equal("773 (0x305)", result.Protocol);
            Assert.Equal(buildTime, result.BuildTime);
            Assert.Equal(69.0, result.PackResource);
            Assert.Equal(88.0, result.PackData);
            Assert.True(result.Stable);
        }

        [Fact]
        public void VersionCommand_Parse_EmptyResponse()
        {
            var response = string.Empty;
            var cmd = ServerCommands.GetVersion();

            var ex = Assert.Throws<ArgumentException>(() => cmd.Parse(response));
            Assert.Equal("response", ex.ParamName);
        }
    }
}
