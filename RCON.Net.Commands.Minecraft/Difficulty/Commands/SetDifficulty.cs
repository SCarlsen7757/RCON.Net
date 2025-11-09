using RCON.Commands.Minecraft.Java.Difficulty.Models;

namespace RCON.Commands.Minecraft.Java.Difficulty.Commands
{
    /// <summary>
    /// Command to set the server difficulty
    /// </summary>
    internal class SetDifficulty : CommandBase<DifficultyResult>
    {
        private readonly DifficultyLevel difficulty;

        public SetDifficulty(DifficultyLevel difficulty)
        {
            this.difficulty = difficulty;
        }

        /// <inheritdoc/>
        public override string Build()
        {
            var difficultyName = difficulty switch
            {
                DifficultyLevel.Peaceful => "peaceful",
                DifficultyLevel.Easy => "easy",
                DifficultyLevel.Normal => "normal",
                DifficultyLevel.Hard => "hard",
                _ => throw new ArgumentOutOfRangeException(nameof(difficulty))
            };

            return $"difficulty {difficultyName}";
        }

        /// <inheritdoc/>
        public override DifficultyResult Parse(string response)
        {
            var changed = !response.Contains("did not change", StringComparison.OrdinalIgnoreCase);

            DifficultyLevel parsedDifficulty = ExtractDifficultyFromResponse(response);

            return new DifficultyResult(parsedDifficulty,
                                        changed,
                                        response);
        }

        private DifficultyLevel ExtractDifficultyFromResponse(string response)
        {
            return response switch
            {
                var s when s.Contains("Peaceful", StringComparison.OrdinalIgnoreCase) => DifficultyLevel.Peaceful,
                var s when s.Contains("Easy", StringComparison.OrdinalIgnoreCase) => DifficultyLevel.Easy,
                var s when s.Contains("Normal", StringComparison.OrdinalIgnoreCase) => DifficultyLevel.Normal,
                var s when s.Contains("Hard", StringComparison.OrdinalIgnoreCase) => DifficultyLevel.Hard,
                _ => difficulty
            };
        }
    }
}
