using RCON.Core.Exceptions;
using RCON.Core.Interfaces;
using RCON.Core.Models;
using System.Net.Sockets;

namespace RCON.Core
{
    /// <summary>
    /// RCON client implementation for connecting to and communicating with RCON servers
    /// </summary>
    public class RconClient : IRconClient
    {
        private readonly string host;
        private readonly int port;
        private readonly string password;
        private readonly int timeout;
        private TcpClient? tcpClient;
        private NetworkStream? networkStream;
        private int requestId;
        private readonly SemaphoreSlim semaphore = new(1, 1);
        private bool disposed;

        internal RconClient(string host, int port, string password, int timeout)
        {
            this.host = host;
            this.port = port;
            this.password = password;
            this.timeout = timeout;
            requestId = 0;
        }

        /// <inheritdoc/>
        public bool IsConnected => tcpClient?.Connected ?? false;

        /// <inheritdoc/>
        public async Task ConnectAsync(CancellationToken cancellationToken = default)
        {
            await semaphore.WaitAsync(cancellationToken);
            try
            {
                if (IsConnected)
                    return;

                tcpClient = new TcpClient();

                using var cts = new CancellationTokenSource(timeout);
                using var linkedCts = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken, cts.Token);

                try
                {
                    await tcpClient.ConnectAsync(host, port, linkedCts.Token);
                }
                catch (Exception ex)
                {
                    throw new RconConnectionException($"Failed to connect to {host}:{port}", ex);
                }

                networkStream = tcpClient.GetStream();
                networkStream.ReadTimeout = timeout;
                networkStream.WriteTimeout = timeout;

                // Authenticate
                await AuthenticateAsync(cancellationToken);
            }
            finally
            {
                semaphore.Release();
            }
        }

        /// <inheritdoc/>
        public async Task DisconnectAsync()
        {
            await semaphore.WaitAsync();
            try
            {
                networkStream?.Close();
                tcpClient?.Close();
                networkStream = null;
                tcpClient = null;
            }
            finally
            {
                semaphore.Release();
            }
        }

        /// <inheritdoc/>
        public async Task<RconResponse> SendCommandAsync(string command, CancellationToken cancellationToken = default)
        {
            try
            {
                var response = await ExecuteCommandAsync(command, cancellationToken);
                return RconResponse.Successful(response);
            }
            catch (Exception ex)
            {
                return RconResponse.Failed(ex.Message);
            }
        }

        /// <inheritdoc/>
        public async Task<string> ExecuteCommandAsync(string command, CancellationToken cancellationToken = default)
        {
            if (!IsConnected)
                throw new RconConnectionException("Not connected to RCON server");

            await semaphore.WaitAsync(cancellationToken);
            try
            {
                var requestId = Interlocked.Increment(ref this.requestId);
                var packet = new RconPacket(requestId, (int)RconPacketType.ExecCommand, command);

                await SendPacketAsync(packet, cancellationToken);
                var response = await ReceivePacketAsync(cancellationToken);

                if (response.Id != requestId)
                    throw new RconCommandException("Response ID mismatch");

                return response.Body;
            }
            catch (Exception ex) when (ex is not RconException)
            {
                throw new RconCommandException("Failed to execute command", ex);
            }
            finally
            {
                semaphore.Release();
            }
        }

        /// <summary>
        /// Executes a typed command
        /// </summary>
        public async Task<T> ExecuteCommandAsync<T>(ICommand<T> command, CancellationToken cancellationToken = default)
        {
            var commandString = command.Build();
            var response = await ExecuteCommandAsync(commandString, cancellationToken);
            return command.Parse(response);
        }

        private async Task AuthenticateAsync(CancellationToken cancellationToken)
        {
            var requestId = Interlocked.Increment(ref this.requestId);
            var authPacket = new RconPacket(requestId, (int)RconPacketType.Auth, password);

            await SendPacketAsync(authPacket, cancellationToken);
            var response = await ReceivePacketAsync(cancellationToken);

            if (response.Id == -1 || response.Id != requestId)
                throw new RconAuthenticationException("Authentication failed - invalid password");
        }

        private async Task SendPacketAsync(RconPacket packet, CancellationToken cancellationToken)
        {
            if (networkStream == null)
                throw new RconConnectionException("Network stream is not available");

            var data = packet.ToBytes();
            await networkStream.WriteAsync(data, cancellationToken);
        }

        private async Task<RconPacket> ReceivePacketAsync(CancellationToken cancellationToken)
        {
            if (networkStream == null)
                throw new RconConnectionException("Network stream is not available");

            // Read size
            var sizeBuffer = new byte[4];
            var bytesRead = await networkStream.ReadAsync(sizeBuffer, cancellationToken);
            if (bytesRead != 4)
                throw new RconConnectionException("Failed to read packet size");

            var size = BitConverter.ToInt32(sizeBuffer, 0);

            // Read rest of packet
            var buffer = new byte[size];
            bytesRead = 0;
            while (bytesRead < size)
            {
                var read = await networkStream.ReadAsync(buffer.AsMemory(bytesRead, size - bytesRead), cancellationToken);
                if (read == 0)
                    throw new RconConnectionException("Connection closed while reading packet");
                bytesRead += read;
            }

            // Combine size and buffer
            var fullPacket = new byte[4 + size];
            Array.Copy(sizeBuffer, 0, fullPacket, 0, 4);
            Array.Copy(buffer, 0, fullPacket, 4, size);

            return RconPacket.FromBytes(fullPacket);
        }

        /// <inheritdoc/>
        public void Dispose()
        {
            if (disposed)
                return;

            DisconnectAsync().GetAwaiter().GetResult();
            semaphore.Dispose();
            disposed = true;
            GC.SuppressFinalize(this);
        }
    }
}
