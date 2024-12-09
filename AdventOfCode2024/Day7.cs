using AdventOfCode2024.Helpers;

namespace AdventOfCode2024;
public class Day7 : IDay
{
    public object Exercise1(string input) =>
        Parse(input).Where(x => FindInTree(x.value, x.operands, ["+", "*"]) != null).Sum(x => x.value);

    public object Exercise2(string input) =>
        Parse(input).Where(x => FindInTree(x.value, x.operands, ["+", "*", "||"]) != null).Sum(x => x.value);

    private IEnumerable<string>? FindInTree(long value, IEnumerable<long> operands, IEnumerable<string> operators)
    {
        return EvalBranch(operands.First(), operands.Skip(1), []);
        IEnumerable<string>? EvalBranch(long root, IEnumerable<long> operands, IEnumerable<string> usedOperators)
        {
            foreach (var op in operators)
            {
                long eval = Eval(root, operands.First(), op);
                if (operands.Count() == 1)
                {
                    if (eval == value)
                        return [.. operators, op];
                    continue;
                }
                var recurse = EvalBranch(eval, operands.Skip(1), [.. usedOperators, op]);
                if (recurse != null)
                    return recurse;
            }
            return null;
        }
    }

    private IEnumerable<(long value, IEnumerable<long> operands)> Parse(string input) => input.SplitByLineBreak(StringSplitOptions.RemoveEmptyEntries)
        .Select(x =>
        {
            var parts = x.Split(':');
            return (long.Parse(parts[0]), parts[1].Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(long.Parse));
        });

    private static long Eval(long a, long b, string op) => op switch
    {
        "+" => a + b,
        "*" => a * b,
        "||" => long.Parse(a.ToString() + b.ToString()),
        _ => throw new NotImplementedException()
    };
}
