using RCON.Core.Interfaces;

namespace RCON.Core
{
    /// <summary>
    /// Builder for creating RCON client instances
    /// </summary>
    public class RconClientBuilder
    {
        private string? host;
        private int? port;
        private string? password;
        private int timeout = 5000; // Default 5 second timeout

        /// <summary>
        /// Sets the host address of the RCON server
        /// </summary>
        public RconClientBuilder WithHost(string host)
        {
            this.host = host;
            return this;
        }

        /// <summary>
        /// Sets the port of the RCON server
        /// </summary>
        public RconClientBuilder WithPort(int port)
        {
            if (port <= 0 || port > 65535)
                throw new ArgumentOutOfRangeException(nameof(port), "Port must be between 1 and 65535");

            this.port = port;
            return this;
        }

        /// <summary>
        /// Sets the RCON password
        /// </summary>
        public RconClientBuilder WithPassword(string password)
        {
            this.password = password;
            return this;
        }

        /// <summary>
        /// Sets the connection timeout in milliseconds
        /// </summary>
        public RconClientBuilder WithTimeout(int timeoutMilliseconds)
        {
            if (timeoutMilliseconds <= 0)
                throw new ArgumentOutOfRangeException(nameof(timeoutMilliseconds), "Timeout must be greater than 0");

            timeout = timeoutMilliseconds;
            return this;
        }

        /// <summary>
        /// Builds and returns the configured RCON client
        /// </summary>
        public IRconClient Build()
        {
            if (string.IsNullOrWhiteSpace(host))
                throw new InvalidOperationException("Host must be specified");

            if (string.IsNullOrWhiteSpace(password))
                throw new InvalidOperationException("Password must be specified");

            if (port == null)
                throw new InvalidOperationException("Port must be specified");

            return new RconClient(host, (int)port, password, timeout);
        }

        /// <summary>
        /// Creates a new RCON client builder
        /// </summary>
        public static RconClientBuilder Create() => new();
    }
}
