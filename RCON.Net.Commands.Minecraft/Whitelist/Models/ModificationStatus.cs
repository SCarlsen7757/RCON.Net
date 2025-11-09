namespace RCON.Commands.Minecraft.Java.Whitelist.Models
{
    /// <summary>
    /// Represents the status of a whitelist modification operation
    /// </summary>
    public enum ModificationStatus
    {
        /// <summary>
        /// The operation was successful
        /// </summary>
        Success,

        /// <summary>
        /// The player was already on the whitelist (add operation)
        /// </summary>
        AlreadyWhitelisted,

        /// <summary>
        /// The player is not on the whitelist (remove operation)
        /// </summary>
        NotWhitelisted,

        /// <summary>
        /// The player does not exist
        /// </summary>
        PlayerDoesNotExist,

        /// <summary>
        /// Unknown status
        /// </summary>
        Unknown
    }
}
