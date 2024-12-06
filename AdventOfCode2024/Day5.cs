using AdventOfCode2024.Helpers;

namespace AdventOfCode2024;
public class Day5 : IDay
{
    public object Exercise1(string input)
    {
        var (rules, tests) = Parse(input);
        return tests
            .Where(test =>
            {
                var map = CreateSortMap(rules, test);
                var sortable = test.Select(x => map[x]);
                var sorted = sortable.Order();
                return sorted.SequenceEqual(sortable);
            })
            .Sum(GetMiddleElement);
    }

    public object Exercise2(string input)
    {
        var (rules, tests) = Parse(input);
        return tests
            .Aggregate(0, (sum, test) =>
            {
                var map = CreateSortMap(rules, test);
                var sortable = test.Select(x => map[x]);
                var sorted = sortable.Order();
                if (!sorted.SequenceEqual(sortable))
                    sum += GetMiddleElement(sorted.Select(x => map.Single(y => y.Value == x).Key));
                return sum;
            });
    }

    private int GetMiddleElement(IEnumerable<int> test)
    {
        return test.ElementAt(test.Count() / 2);
    }

    private static (IEnumerable<Rule> rules, IEnumerable<IEnumerable<int>> tests) Parse(string input)
    {
        string[] parts = input.SplitByEmptyLine(StringSplitOptions.RemoveEmptyEntries);
        var rules = parts[0].SplitByLineBreak(StringSplitOptions.RemoveEmptyEntries).Select(x =>
        {
            var arr = x.Split('|', StringSplitOptions.RemoveEmptyEntries).Select(int.Parse);
            return new Rule(arr.ElementAt(0), arr.ElementAt(1));
        });
        var tests = parts[1].SplitByLineBreak(StringSplitOptions.RemoveEmptyEntries).Select(x => x.Split(',', StringSplitOptions.RemoveEmptyEntries).Select(int.Parse));
        return (rules, tests);
    }

    private static Dictionary<int, int> CreateSortMap(IEnumerable<Rule> rules, IEnumerable<int> test)
    {
        Dictionary<int, HashSet<int>> nodes = [];

        IEnumerable<Rule> subRules = rules.Where(x => test.Contains(x.Lesser) && test.Contains(x.Greater));

        foreach (var rule in subRules)
        {
            HashSet<int> lesser;
            if (!nodes.TryGetValue(rule.Greater, out lesser!))
            {
                lesser = [];
                nodes.Add(rule.Greater, lesser);
            }
            lesser.Add(rule.Lesser);
            nodes.TryAdd(rule.Lesser, []);
        }
        Dictionary<int, int> result = [];

        while (nodes.Count != 0)
        {
            var lowest = nodes.Where(x => x.Value.Count == 0).Single();
            nodes.Remove(lowest.Key);
            foreach (var node in nodes)
                node.Value.Remove(lowest.Key);
            result.Add(lowest.Key, result.Count);
        }

        return result;
    }

    private record Rule(int Lesser, int Greater);
}
