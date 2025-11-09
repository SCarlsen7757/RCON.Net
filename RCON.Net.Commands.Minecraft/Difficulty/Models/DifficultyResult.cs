namespace RCON.Commands.Minecraft.Java.Difficulty.Models
{
    /// <summary>
    /// Represents the result of a difficulty change operation
    /// </summary>
    public class DifficultyResult
    {
        /// <summary>
        /// Gets the new difficulty level
        /// </summary>
        public DifficultyLevel Difficulty { get; }

        /// <summary>
        /// Gets whether the difficulty was changed
        /// </summary>
        public bool Changed { get; }

        /// <summary>
        /// Gets the message from the server
        /// </summary>
        public string Message { get; }

        /// <summary>
        /// Initializes a new instance of the DifficultyResult class
        /// </summary>
        public DifficultyResult(DifficultyLevel difficulty, bool changed, string message)
        {
            Difficulty = difficulty;
            Changed = changed;
            Message = message;
        }
    }
}
