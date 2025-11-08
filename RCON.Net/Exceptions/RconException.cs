namespace RCON.Core.Exceptions
{
    /// <summary>
    /// Base exception for RCON-related errors
    /// </summary>
    public class RconException : Exception
    {
        public RconException(string message) : base(message) { }
        public RconException(string message, Exception innerException) : base(message, innerException) { }
    }

    /// <summary>
    /// Exception thrown when authentication fails
    /// </summary>
    public class RconAuthenticationException : RconException
    {
        public RconAuthenticationException(string message) : base(message) { }
    }

    /// <summary>
    /// Exception thrown when connection fails
    /// </summary>
    public class RconConnectionException : RconException
    {
        public RconConnectionException(string message) : base(message) { }
        public RconConnectionException(string message, Exception innerException) : base(message, innerException) { }
    }

    /// <summary>
    /// Exception thrown when command execution fails
    /// </summary>
    public class RconCommandException : RconException
    {
        public RconCommandException(string message) : base(message) { }
        public RconCommandException(string message, Exception innerException) : base(message, innerException) { }
    }
}
