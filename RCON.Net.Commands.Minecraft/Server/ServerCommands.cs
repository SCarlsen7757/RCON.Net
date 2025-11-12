using RCON.Commands.Minecraft.Java.Server.Commands;
using RCON.Commands.Minecraft.Java.Server.Models;
using RCON.Core.Interfaces;
using System.Threading;
using System.Threading.Tasks;

namespace RCON.Commands.Minecraft.Java.Server
{
    public class ServerCommands
    {
        private readonly IRconClient client;
        public ServerCommands(IRconClient client) => this.client = client;

        public static ICommand<VersionResult> GetVersion()
        {
            return new Commands.Version();
        }

        public Task<VersionResult> GetVersionAsync(CancellationToken ct = default) =>
            client.ExecuteCommandAsync(GetVersion(), ct);
    }
}
