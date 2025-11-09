using RCON.Commands.Minecraft.Java.Whitelist.Models;

namespace RCON.Commands.Minecraft.Java.Whitelist.Commands
{
    /// <summary>
    /// Command to disable the whitelist
    /// </summary>
    internal class Off : CommandBase<ToggleResult>
    {
        /// <inheritdoc/>
        public override string Build() => "whitelist off";

        /// <inheritdoc/>
        public override ToggleResult Parse(string response)
        {
            if (response.Contains("already turned off"))
            {
                return new ToggleResult(false, false, response);
            }
            else if (response.Contains("now turned off"))
            {
                return new ToggleResult(false, true, response);
            }

            return new ToggleResult(false, false, response);
        }
    }
}
