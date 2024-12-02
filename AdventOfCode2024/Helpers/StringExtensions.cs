namespace AdventOfCode2024.Helpers;
public static class StringExtensions
{
    public static string[] SplitByLineBreak(this string input, StringSplitOptions stringSplitOptions = StringSplitOptions.None) =>
        input.Split(["\r\n", "\r", "\n"], stringSplitOptions);
    public static string[] SplitByEmptyLine(this string input, StringSplitOptions stringSplitOptions = StringSplitOptions.None) =>
        input.Split(["\r\n\r\n", "\r\r", "\n\n"], stringSplitOptions);
    public static string ReplaceMany(this string input, Dictionary<string, string> replacements) =>
        replacements.Aggregate(input, (i, r) => i.Replace(r.Key, r.Value));
}