namespace RCON.Commands.Minecraft.Java.Op.Models
{
    /// <summary>
    /// Represents the status of an op/deop operation
    /// </summary>
    public enum OpStatus
    {
        /// <summary>
        /// Operation was successful
        /// </summary>
        Success,

        /// <summary>
        /// Nothing changed - already in desired state
        /// </summary>
        NoChange,

        /// <summary>
        /// Player not found
        /// </summary>
        NotFound,

        /// <summary>
        /// The status is unknown
        /// </summary>
        Unknown
    }
}
