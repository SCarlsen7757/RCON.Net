namespace RCON.Commands.Minecraft.Java.Ban.Models
{
    /// <summary>
    /// Represents the status of a ban/unban operation
    /// </summary>
    public enum BanStatus
    {
        /// <summary>
        /// Player was successfully banned
        /// </summary>
        Success,

        /// <summary>
        /// The entity is already banned
        /// </summary>
        AlreadyBanned,

        /// <summary>
        /// The entity was not found or not on the ban list
        /// </summary>
        NotFound,

        /// <summary>
        /// The status is unknown
        /// </summary>
        Unknown
    }
}
