namespace RCON.Core.Models
{
    /// <summary>
    /// Represents a response from an RCON command
    /// </summary>
    public class RconResponse
    {
        public bool Success { get; set; }
        public string Message { get; set; } = string.Empty;
        public string? ErrorMessage { get; set; }

        public RconResponse(bool success, string message, string? errorMessage = null)
        {
            Success = success;
            Message = message;
            ErrorMessage = errorMessage;
        }

        public static RconResponse Successful(string message) => new(true, message);
        public static RconResponse Failed(string errorMessage) => new(false, string.Empty, errorMessage);
    }
}
