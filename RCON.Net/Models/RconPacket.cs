namespace RCON.Core.Models
{
    /// <summary>
    /// Represents an RCON packet with ID, type, and body
    /// </summary>
    public class RconPacket
    {
        public int Id { get; set; }
        public int Type { get; set; }
        public string Body { get; set; } = string.Empty;

        public RconPacket(int id, int type, string body)
        {
            Id = id;
            Type = type;
            Body = body ?? string.Empty;
        }

        /// <summary>
        /// Converts the packet to bytes for transmission
        /// </summary>
        public byte[] ToBytes()
        {
            var bodyBytes = System.Text.Encoding.UTF8.GetBytes(Body);
            var size = sizeof(int) + sizeof(int) + bodyBytes.Length + 2; // +2 for null terminators

            var buffer = new byte[size + sizeof(int)]; // +4 for size field

            using var ms = new MemoryStream(buffer);
            using var writer = new BinaryWriter(ms);

            writer.Write(size);
            writer.Write(Id);
            writer.Write(Type);
            writer.Write(bodyBytes);
            writer.Write((byte)0); // Null terminator
            writer.Write((byte)0); // Null terminator

            return buffer;
        }

        /// <summary>
        /// Creates an RconPacket from received bytes
        /// </summary>
        public static RconPacket FromBytes(byte[] data)
        {
            using var ms = new MemoryStream(data);
            using var reader = new BinaryReader(ms);

            var size = reader.ReadInt32();
            var id = reader.ReadInt32();
            var type = reader.ReadInt32();

            var bodyLength = size - sizeof(int) - sizeof(int) - 2;
            var bodyBytes = reader.ReadBytes(bodyLength);
            var body = System.Text.Encoding.UTF8.GetString(bodyBytes);

            return new RconPacket(id, type, body);
        }
    }
}
