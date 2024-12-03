using AdventOfCode2024.Helpers;

namespace AdventOfCode2024;
public class Day2 : IDay
{
    public object Exercise1(string input)
    {
        var data = Parse(input);
        return data.Count(CheckLineNoReplace);
    }

    public object Exercise2(string input)
    {
        var data = Parse(input);
        return data.Count(BruteForceCheck);
    }

    private static bool Test(int a, int b, int direction)
    {
        int diff = a - b;
        return diff != 0 && Math.Abs(diff) <= 3 && Math.Sign(diff) == direction;
    }

    private int[][] Parse(string input) => input
        .SplitByLineBreak(StringSplitOptions.RemoveEmptyEntries)
        .Select(x => x.Split(' ', StringSplitOptions.RemoveEmptyEntries)
            .Select(y => int.Parse(y))
            .ToArray())
        .ToArray();

    private bool BruteForceCheck(int[] line)
    {
        for (int i = 0; i < line.Length; i++)
        {
            int[] candidate = [.. line[..i], .. line[(i + 1)..]];
            if (CheckLineNoReplace(candidate))
                return true;
        }
        return false;
    }

    private static bool CheckLineNoReplace(int[] l)
    {
        var direction = Math.Sign(Enumerable.Range(1, l.Length - 1).Select(i => Math.Sign(l[i] - l[i - 1])).Sum());
        for (int i = 1; i < l.Length; i++)
        {
            if (!Test(l[i], l[i - 1], direction))
                return false;
        }
        return true;
    }
}
