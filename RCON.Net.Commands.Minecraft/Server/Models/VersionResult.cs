namespace RCON.Commands.Minecraft.Java.Server.Models
{
    public record VersionResult
    {
        public required string Id { get; set; }
        public required string Name { get; set; }
        public int Data { get; set; }
        public string Series { get; set; } = string.Empty;
        public required string Protocol { get; set; }
        public DateTime BuildTime { get; set; }
        public double PackResource { get; set; }
        public double PackData { get; set; }
        public bool Stable { get; set; }
    }
}
