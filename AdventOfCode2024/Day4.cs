using AdventOfCode2024.Helpers;

namespace AdventOfCode2024;
public class Day4 : IDay
{
    private (int dx, int dy)[] Checks(int l) => [
        (-l,-l),(0,-l),(l,-l),
        (-l, 0),       (l, 0),
        (-l, l),(0, l),(l, l)
    ];
    public object Exercise1(string input)
    {
        var check4 = Checks(4);
        HashSet<((int, int) start, (int, int) end)> words = [];
        var crossword = Parse(input);
        for (int i = 0; i < crossword.GetLength(0); i++)
        {
            for (int j = 0; j < crossword.GetLength(1); j++)
            {
                foreach (var (dx, dy) in check4)
                {
                    if (crossword[i, j] != 'X')
                        continue;

                    var word = new string(crossword.GetSubArrayVector(i, j, dx, dy).ToArray());
                    if (word == "XMAS")
                        words.Add(((i, j), (i + dx, j + dy)));
                }
            }
        }
        return words.Count;
    }

    public object Exercise2(string input)
    {
        //kernel-esque approach
        var crossword = Parse(input);
        int count = 0;
        for (int i = 1; i < crossword.GetLength(0) - 1; i++)
        {
            for (int j = 1; j < crossword.GetLength(1) - 1; j++)
            {
                if (crossword[i, j] != 'A')
                    continue;

                if (!(crossword[i - 1, j - 1] == 'M' && crossword[i + 1, j + 1] == 'S'
                    || crossword[i - 1, j - 1] == 'S' && crossword[i + 1, j + 1] == 'M'))
                    continue;
                if (!(crossword[i - 1, j + 1] == 'M' && crossword[i + 1, j - 1] == 'S'
                    || crossword[i - 1, j + 1] == 'S' && crossword[i + 1, j - 1] == 'M'))
                    continue;

                count++;
            }
        }
        return count;
    }

    private char[,] Parse(string input) => input
        .SplitByLineBreak(StringSplitOptions.RemoveEmptyEntries)
        .ParseGrid();
}
