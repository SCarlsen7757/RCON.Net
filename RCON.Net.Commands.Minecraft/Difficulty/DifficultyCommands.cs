using RCON.Commands.Minecraft.Java.Difficulty.Models;
using RCON.Core.Interfaces;

namespace RCON.Commands.Minecraft.Java.Difficulty
{
    /// <summary>
    /// Provides a set of commands for managing server difficulty settings.
    /// </summary>
    /// <remarks>This class offers both synchronous and asynchronous methods to interact with the difficulty system.
    /// Commands are created using static methods and can be executed to perform the desired operations.</remarks>
    public class DifficultyCommands
    {
        private readonly IRconClient client;
        public DifficultyCommands(IRconClient client) => this.client = client;

        /// <summary>
        /// Creates a command to set the server difficulty.
        /// </summary>
        /// <param name="difficulty">The difficulty level to set.</param>
        /// <returns>A command that, when executed, sets the difficulty and returns the result of the operation.</returns>
        public static ICommand<DifficultyResult> SetDifficulty(DifficultyLevel difficulty)
        {
            return new Commands.SetDifficulty(difficulty);
        }

        /// <summary>
        /// Sets the server difficulty asynchronously.
        /// </summary>
        /// <param name="difficulty">The difficulty level to set.</param>
        /// <param name="ct">An optional <see cref="CancellationToken"/> to observe while waiting for the operation to complete.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the asynchronous operation. The task result contains a <see cref="DifficultyResult"/> indicating the outcome of the operation.</returns>
        public Task<DifficultyResult> SetDifficultyAsync(DifficultyLevel difficulty, CancellationToken ct = default) =>
            client.ExecuteCommandAsync(SetDifficulty(difficulty), ct);
    }
}
