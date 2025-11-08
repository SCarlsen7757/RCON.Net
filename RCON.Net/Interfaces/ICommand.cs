namespace RCON.Core.Interfaces
{
    /// <summary>
    /// Interface for RCON commands
    /// </summary>
    public interface ICommand
    {
        /// <summary>
        /// Builds the command string to send to the server
        /// </summary>
        string Build();
    }

    /// <summary>
    /// Interface for RCON commands with typed responses
    /// </summary>
    public interface ICommand<T> : ICommand
    {
        /// <summary>
        /// Parses the server response into a typed result
        /// </summary>
        T Parse(string response);
    }
}
