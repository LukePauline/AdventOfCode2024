namespace AdventOfCode2024.Helpers;
public static class StringExtensions
{
    public static string[] SplitByLineBreak(this string input, StringSplitOptions stringSplitOptions = StringSplitOptions.None) =>
        input.Split(new[] { "\r\n", "\r", "\n" }, stringSplitOptions);
    public static string[] SplitByEmptyLine(this string input, StringSplitOptions stringSplitOptions = StringSplitOptions.None) =>
        input.Split(new[] { "\r\n\r\n", "\r\r", "\n\n" }, stringSplitOptions);
    public static string ReplaceMany(this string input, Dictionary<string, string> replacements) =>
        replacements.Aggregate(input, (i, r) => i.Replace(r.Key, r.Value));

    public static string ReplaceFirstOf(this string input, Dictionary<string, string> keyValues)
    {
        var ranks = keyValues
            .Select(kv => (kv, index: input.IndexOf(kv.Key)))
            .Where(x => x.index >= 0);

        if (!ranks.Any())
            return input;

        var first = ranks
            .Aggregate((x, y) => x.index < y.index ? x : y);

        return input.Remove(first.index) + keyValues[first.kv.Key] + input.Substring(first.index + first.kv.Value.Length);
    }

    public static string ReplaceLastOf(this string input, Dictionary<string, string> keyValues)
    {
        var ranks = keyValues
            .Select(kv => (kv, index: input.LastIndexOf(kv.Key)))
            .Where(x => x.index >= 0);

        if (!ranks.Any())
            return input;

        var last = ranks
            .Aggregate((x, y) => x.index > y.index ? x : y);

        return input.Remove(last.index) + keyValues[last.kv.Key] + input.Substring(last.index + last.kv.Value.Length);
    }
}