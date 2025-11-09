namespace RCON.Commands.Minecraft.Java.Whitelist.Models
{
    /// <summary>
    /// Represents the result of a whitelist on/off operation
    /// </summary>
    public class ToggleResult
    {
        /// <summary>
        /// Gets whether the whitelist is now enabled
        /// </summary>
        public bool IsEnabled { get; }

        /// <summary>
        /// Gets whether the state was changed
        /// </summary>
        public bool StateChanged { get; }

        /// <summary>
        /// Gets the message from the server
        /// </summary>
        public string Message { get; }

        /// <summary>
        /// Initializes a new instance of the ToggleResult class
        /// </summary>
        public ToggleResult(bool isEnabled, bool stateChanged, string message)
        {
            IsEnabled = isEnabled;
            StateChanged = stateChanged;
            Message = message;
        }
    }
}
