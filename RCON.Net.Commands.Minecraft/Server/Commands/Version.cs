using RCON.Commands.Minecraft.Java.Server.Models;
using System.Globalization;
using System.Text.RegularExpressions;

namespace RCON.Commands.Minecraft.Java.Server.Commands
{
    public partial class Version : CommandBase<VersionResult>
    {
        public override string Build() => "version";

        public override VersionResult Parse(string response)
        {
            if (string.IsNullOrEmpty(response))
                throw new ArgumentException("Response cannot be null or empty", nameof(response));

            var pattern = MyRegex();
            var match = pattern.Match(response);

            var id = match.Groups["id"].Value?.Trim();
            var name = match.Groups["name"].Value?.Trim();

            int data = 0;
            var dataRaw = match.Groups["data"].Value;
            if (!string.IsNullOrEmpty(dataRaw) && int.TryParse(dataRaw.Trim(), NumberStyles.Integer, CultureInfo.InvariantCulture, out var tempData))
                data = tempData;

            var series = match.Groups["series"].Value?.Trim();
            var protocolRaw = match.Groups["protocol"].Value?.Trim();

            DateTime buildTime = DateTime.MinValue;
            var buildRaw = match.Groups["build_time"].Value;
            if (!string.IsNullOrEmpty(buildRaw))
            {
                // Expected formats like: "Tue Oct 07 09:14:11 UTC 2025"
                var formats = new[]
                {
                        "ddd MMM dd HH:mm:ss 'UTC' yyyy",
                        "ddd MMM d HH:mm:ss 'UTC' yyyy",
                        "ddd MMMddHH:mm:ss 'UTC'yyyy",
                        "ddd MMMdHH:mm:ss 'UTC'yyyy"
                    };
                DateTime.TryParseExact(buildRaw.Trim(), formats, CultureInfo.InvariantCulture, DateTimeStyles.AssumeUniversal | DateTimeStyles.AdjustToUniversal, out buildTime);
            }

            double packResource = 0.0;
            var pr = match.Groups["pack_resource"].Value;
            if (!string.IsNullOrEmpty(pr) && double.TryParse(pr.Trim(), NumberStyles.Float, CultureInfo.InvariantCulture, out var dpr))
                packResource = dpr;

            double packData = 0.0;
            var pd = match.Groups["pack_data"].Value;
            if (!string.IsNullOrEmpty(pd) && double.TryParse(pd.Trim(), NumberStyles.Float, CultureInfo.InvariantCulture, out var dpd))
                packData = dpd;

            bool stable = false;
            var stableRaw = match.Groups["stable"].Value;
            if (!string.IsNullOrEmpty(stableRaw))
                stable = stableRaw.StartsWith("y", StringComparison.OrdinalIgnoreCase) || stableRaw.Equals("true", StringComparison.OrdinalIgnoreCase);

            return new VersionResult()
            {
                Id = id ?? string.Empty,
                Name = name ?? string.Empty,
                Data = data,
                Series = series ?? string.Empty,
                Protocol = protocolRaw ?? string.Empty,
                BuildTime = buildTime,
                PackResource = packResource,
                PackData = packData,
                Stable = stable
            };
        }

        [GeneratedRegex(@".*?\bid\s*=\s*(?<id>.*?)(?=name\s*=|$)\s*name\s*=\s*(?<name>.*?)(?=data\s*=|$)\s*data\s*=\s*(?<data>.*?)(?=series\s*=|$)\s*series\s*=\s*(?<series>.*?)(?=protocol\s*=|$)\s*protocol\s*=\s*(?<protocol>.*?)(?=build_time\s*=|$)\s*build_time\s*=\s*(?<build_time>.*?)(?=pack_resource\s*=|$)\s*pack_resource\s*=\s*(?<pack_resource>.*?)(?=pack_data\s*=|$)\s*pack_data\s*=\s*(?<pack_data>.*?)(?=stable\s*=|$)\s*stable\s*=\s*(?<stable>.*?)(?=$)", RegexOptions.Singleline | RegexOptions.IgnoreCase)]
        private static partial Regex MyRegex();
    }
}
