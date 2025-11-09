using RCON.Commands.Minecraft.Java.Ban.Models;

namespace RCON.Commands.Minecraft.Java.Ban.Commands
{
    /// <summary>
    /// Command to ban an IP address
    /// </summary>
    internal class BanIp : CommandBase<BanResult>
    {
        private readonly string ipAddress;
        private readonly string reason;

        public BanIp(string ipAddress, string reason = "")
        {
            this.ipAddress = ipAddress;
            this.reason = reason;
        }

        /// <inheritdoc/>
        public override string Build() => string.IsNullOrEmpty(reason) ? $"ban-ip {ipAddress}" : $"ban-ip {ipAddress} {reason}";

        /// <inheritdoc/>
        public override BanResult Parse(string response)
        {
            if (response.Contains("Banned IP", StringComparison.OrdinalIgnoreCase))
            {
                return new BanResult(BanStatus.Success,
                                     ipAddress,
                                     reason,
                                     response);
            }
            else if (response.Contains("already banned", StringComparison.OrdinalIgnoreCase))
            {
                return new BanResult(BanStatus.AlreadyBanned,
                                     ipAddress,
                                     reason,
                                     response);
            }

            return new BanResult(BanStatus.Unknown,
                                 ipAddress,
                                 reason,
                                 response);
        }
    }
}
