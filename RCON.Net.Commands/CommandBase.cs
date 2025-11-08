using RCON.Core.Interfaces;

namespace RCON.Commands
{
    /// <summary>
    /// Base class for RCON commands
    /// </summary>
    public abstract class CommandBase : ICommand
    {
        /// <inheritdoc/>
        public abstract string Build();
    }

    /// <summary>
    /// Base class for RCON commands with typed responses
    /// </summary>
    public abstract class CommandBase<T> : ICommand<T>
    {
        /// <inheritdoc/>
        public abstract string Build();

        /// <inheritdoc/>
        public abstract T Parse(string response);
    }
}
