using AdventOfCode2024.Helpers;

namespace AdventOfCode2024;
public class Day1 : IDay
{
    public object Exercise1(string input)
    {
        var lists = Parse(input);

        int[] list1 = [.. lists.Item1.OrderBy(x => x)];
        int[] list2 = [.. lists.Item2.OrderBy(x => x)];

        int sum = 0;

        for (int i = 0; i < list1.Length; i++)
            sum += Math.Abs(list1[i] - list2[i]);

        return sum;
    }

    public object Exercise2(string input)
    {
        var (list1, list2) = Parse(input);
        var freq1 = list1.GroupBy(x => x).ToDictionary(x => x.Key, x => x.Count());
        var freq2 = list2.GroupBy(x => x).ToDictionary(x => x.Key, x => x.Count());

        return freq1.Sum(x => freq2.TryGetValue(x.Key, out int freq) ? x.Value * freq * x.Key : 0);
    }

    private static (IEnumerable<int>, IEnumerable<int>) Parse(string input)
    {
        var ids = input.SplitByLineBreak(StringSplitOptions.RemoveEmptyEntries)
            .SelectMany(x => x.Split("   ")
                .Select(y => int.Parse(y)));
        return (ids.SelectNth(2), ids.SelectNth(2, 1));
    }
}
