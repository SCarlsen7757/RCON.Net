using RCON.Commands.Minecraft.Java.Ban.Models;

namespace RCON.Commands.Minecraft.Java.Ban.Commands
{
    /// <summary>
    /// Command to pardon a banned IP address
    /// </summary>
    internal class PardonIp : CommandBase<BanResult>
    {
        private readonly string ipAddress;

        public PardonIp(string ipAddress)
        {
            this.ipAddress = ipAddress;
        }

        /// <inheritdoc/>
        public override string Build() => $"pardon-ip {ipAddress}";

        /// <inheritdoc/>
        public override BanResult Parse(string response)
        {
            if (response.Contains("Unbanned IP", StringComparison.OrdinalIgnoreCase))
            {
                return new BanResult(BanStatus.Success,
                                     ipAddress,
                                     "",
                                     response);
            }
            else if (response.Contains("not banned", StringComparison.OrdinalIgnoreCase))
            {
                return new BanResult(BanStatus.NotFound,
                                     ipAddress,
                                     "",
                                     response);
            }

            return new BanResult(BanStatus.Unknown,
                                 ipAddress,
                                 "",
                                 response);
        }
    }
}
