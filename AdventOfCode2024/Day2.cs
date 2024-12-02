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
            var direction = Math.Sign(l[1] - l[0]);
            for (int i = 1; i < l.Length; i++)
            {
                if (!Test(l[i], l[i - 1], direction))
                {
                    if (singleFixUsed)
                        return false;

                    singleFixUsed = true;

                    if (i + 1 == l.Length)
                        return true;

                    // Test remove i
                    if (i == 1)
                        direction = Math.Sign(l[2] - l[0]);
                    if (Test(l[i + 1], l[i - 1], direction))
                    {
                        i++;
                        continue;
                    }

                    // Test remove i-1
                    if (i == 1)
                    {
                        direction = Math.Sign(l[2] - l[1]);
                        continue;
                    }
                    if (i == 2)
                        direction = Math.Sign(l[2] - l[0]);
                    if (!Test(l[i], l[i - 2], direction))
                        return false;
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
