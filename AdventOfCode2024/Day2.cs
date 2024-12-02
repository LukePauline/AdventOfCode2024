using AdventOfCode2024.Helpers;

namespace AdventOfCode2024;
public class Day2 : IDay
{
    public object Exercise1(string input)
    {
        var data = Parse(input);
        return data.Count(l =>
        {
            int direction = Math.Sign(l[1] - l[0]);
            for (int i = 1; i < l.Length; i++)
            {
                if (!Test(l[i], l[i - 1], direction))
                    return false;
            }
            return true;
        });
    }

    public object Exercise2(string input)
    {
        var data = Parse(input);
        return data.Count(l =>
        {
            bool singleFixUsed = false;
            var direction = Math.Sign(l.Skip(1).Take(3).Select((x, i) => Math.Sign(x - l[i])).Average());
            for (int i = 1; i < l.Length; i++)
            {
                if (!Test(l[i], l[i - 1], direction))
                {
                    if (singleFixUsed)
                        return false;
                    if (i + 1 == l.Length)
                        return true;
                    // Test remove i
                    if (Test(l[i + 1], l[i - 1], direction))
                        i++;
                    // Test remove i-1
                    else if (i >= 2 || !Test(l[i], l[i - 2], direction))
                        return false;

                    singleFixUsed = true;
                }
            }
            return true;
        });
    }

    private static bool Test(int a, int b, int direction)
    {
        int diff = a - b;
        return Math.Abs(diff) <= 3 && Math.Sign(diff) == direction;
    }

    private int[][] Parse(string input) => input
        .SplitByLineBreak(StringSplitOptions.RemoveEmptyEntries)
        .Select(x => x.Split(' ', StringSplitOptions.RemoveEmptyEntries)
            .Select(y => int.Parse(y))
            .ToArray())
        .ToArray();
}
