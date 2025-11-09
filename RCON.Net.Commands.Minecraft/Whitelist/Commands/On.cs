using RCON.Commands.Minecraft.Java.Whitelist.Models;

namespace RCON.Commands.Minecraft.Java.Whitelist.Commands
{
    /// <summary>
    /// Command to enable the whitelist
    /// </summary>
    internal class On : CommandBase<ToggleResult>
    {
        /// <inheritdoc/>
        public override string Build() => "whitelist on";

        /// <inheritdoc/>
        public override ToggleResult Parse(string response)
        {
            if (response.Contains("already turned on"))
            {
                return new ToggleResult(true, false, response);
            }
            else if (response.Contains("now turned on"))
            {
                return new ToggleResult(true, true, response);
            }

            return new ToggleResult(true, false, response);
        }
    }
}
