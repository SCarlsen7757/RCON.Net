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

            // Extract key/value pairs where values may contain spaces (e.g. build_time)
            var dict = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);

            var pattern = MyRegex();
            var match = pattern.Match(response);
            while (match.Success)
            {
                var key = match.Groups["key"].Value.Trim();
                var value = match.Groups["value"].Value.Trim();
                dict[key] = value;
                match = match.NextMatch();
            }

            dict.TryGetValue("id", out var id);
            dict.TryGetValue("name", out var name);

            int data = 0;
            if (dict.TryGetValue("data", out var dataRaw) && int.TryParse(dataRaw.Trim(), NumberStyles.Integer, CultureInfo.InvariantCulture, out var tempData))
                data = tempData;

            dict.TryGetValue("series", out var series);
            dict.TryGetValue("protocol", out var protocolRaw);

            DateTime buildTime = DateTime.MinValue;
            if (dict.TryGetValue("build_time", out var buildRaw))
            {
                // Try exact format like: "Tue Oct0709:14:11 UTC2025"
                var formats = new[]
                {
                        "ddd MMM dd HH:mm:ss 'UTC' yyyy",
                        "ddd MMM d HH:mm:ss 'UTC' yyyy",
                        "ddd MMMddHH:mm:ss 'UTC'yyyy",
                        "ddd MMMdHH:mm:ss 'UTC'yyyy"
                    };
                DateTime.TryParseExact(buildRaw, formats, CultureInfo.InvariantCulture, DateTimeStyles.AssumeUniversal | DateTimeStyles.AdjustToUniversal, out buildTime);
            }

            double packResource = 0.0;
            if (dict.TryGetValue("pack_resource", out var pr) && double.TryParse(pr, NumberStyles.Float, CultureInfo.InvariantCulture, out var dpr))
                packResource = dpr;

            double packData = 0.0;
            if (dict.TryGetValue("pack_data", out var pd) && double.TryParse(pd, NumberStyles.Float, CultureInfo.InvariantCulture, out var dpd))
                packData = dpd;

            bool stable = false;
            if (dict.TryGetValue("stable", out var stableRaw))
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

        [GeneratedRegex(@"(?<key>\w+)\s*=\s*(?<value>.*?)(?=(\s*\w+\s*=)|$)", RegexOptions.Singleline)]
        private static partial Regex MyRegex();
    }
}
