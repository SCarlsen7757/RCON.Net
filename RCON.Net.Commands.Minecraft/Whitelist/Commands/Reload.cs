namespace RCON.Commands.Minecraft.Java.Whitelist.Commands
{
    /// <summary>
    /// Command to reload the whitelist
    /// </summary>
    internal class Reload : CommandBase<string>
    {
        /// <inheritdoc/>
        public override string Build() => "whitelist reload";

        /// <inheritdoc/>
        public override string Parse(string response) => response;
    }
}
