namespace Canthenos.DataAccessLibrary;

public class DotEnv : IDotEnv
{
    public void Load(string filePath)
    {
        if (!File.Exists(filePath)) return;

        foreach (var line in File.ReadAllLines(filePath))
        {
            var parts = SplitIgnoringQuote(line, "=");
            if (parts.Count != 2) continue;
            Environment.SetEnvironmentVariable(parts[0], parts[1]);
        }
    }

    private static List<string> SplitIgnoringQuote(string line, string seperator)
    {
        if (!line.Contains('\'')) return new List<string>(line.Split(seperator, StringSplitOptions.RemoveEmptyEntries));

        var qouteIndexes = new int[2] { line.IndexOf('\''), line.LastIndexOf('\'') };

        var seperatorIndexes = new List<int>();
        var lastIndex = -1;

        while (lastIndex != seperatorIndexes.Count)
        {
            var index = line.IndexOf(seperator, lastIndex + 1, StringComparison.Ordinal);

            if (index == -1) break;
            lastIndex = index;

            if (index > qouteIndexes[0] && index < qouteIndexes[1]) continue;
            seperatorIndexes.Add(index);
        }

        return SplitStringAtIndexes(line, seperatorIndexes).Select(RemoveQoutes).ToList();
    }

    private static IEnumerable<string> SplitStringAtIndexes(string input, IEnumerable<int> indexes)
    {
        var result = new List<string>();
        var startIndex = 0;

        foreach (var index in indexes.TakeWhile(index => index > startIndex || index < input.Length))
        {
            result.Add(input[startIndex..index]);
            startIndex = index + 1;
        }

        if (startIndex > input.Length) return result;
        result.Add(input[startIndex..]);

        return result;
    }

    private static string RemoveQoutes(string input)
    {
        var qouteIndexes = new[] { input.IndexOf('\''), input.LastIndexOf('\'') };
        var result = input.Where((_, i) => i != qouteIndexes[0] && i != qouteIndexes[1]).ToList();
        return string.Join("", result).Trim();
    }
}