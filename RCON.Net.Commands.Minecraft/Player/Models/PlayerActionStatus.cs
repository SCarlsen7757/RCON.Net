namespace RCON.Commands.Minecraft.Java.Player.Models
{
    /// <summary>
    /// Represents the status of a player action
    /// </summary>
    public enum PlayerActionStatus
    {
        /// <summary>
        /// Action was successful
        /// </summary>
        Success,

        /// <summary>
        /// Player not found
        /// </summary>
        NotFound,

        /// <summary>
        /// Entity not found
        /// </summary>
        EntityNotFound,

        /// <summary>
        /// No entity was found
        /// </summary>
        NoEntity,

        /// <summary>
        /// The status is unknown
        /// </summary>
        Unknown
    }
}
