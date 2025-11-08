namespace RCON.Core.Models
{
    /// <summary>
    /// RCON packet types as defined in the Source RCON protocol
    /// </summary>
    public enum RconPacketType
    {
        /// <summary>
        /// Authentication request
        /// </summary>
        Auth = 3,

        /// <summary>
        /// Authentication response
        /// </summary>
        AuthResponse = 2,

        /// <summary>
        /// Command execution request
        /// </summary>
        ExecCommand = 2,

        /// <summary>
        /// Command response
        /// </summary>
        ResponseValue = 0
    }
}
