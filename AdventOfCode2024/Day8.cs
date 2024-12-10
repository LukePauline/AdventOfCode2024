using AdventOfCode2024.DataStructures;
using AdventOfCode2024.Helpers;

namespace AdventOfCode2024;
public class Day8 : IDay
{
    public object Exercise1(string input)
    {
        char[,] grid = Parse(input);
        BoundingBox limits = grid.GetBounds();
        Dictionary<char, IEnumerable<Point>> antennae = FindAntennae(grid);
        var antinodes = antennae.SelectMany(f => CalculateAntinodesEx1(f.Value)).Where(limits.Contains).Distinct();
        return antinodes.Count();
    }

    public object Exercise2(string input)
    {
        char[,] grid = Parse(input);
        BoundingBox limits = grid.GetBounds();
        Dictionary<char, IEnumerable<Point>> antennae = FindAntennae(grid);
        var antinodes = antennae.SelectMany(f => CalculateAntinodesEx2(f.Value, limits)).Distinct();
        return antinodes.Count();
    }

    private static IEnumerable<Point> CalculateAntinodesEx1(IEnumerable<Point> antennae)
    {
        List<Point> antinodes = [];
        for (int i = 0; i < antennae.Count(); i++)
        {
            Point antA = antennae.ElementAt(i);
            for (int j = i + 1; j < antennae.Count(); j++)
            {
                Point antB = antennae.ElementAt(j);
                var dist = antB - antA;
                antinodes.Add(antB + dist);
                antinodes.Add(antA - dist);
            }
        }
        return antinodes;
    }
    private static IEnumerable<Point> CalculateAntinodesEx2(IEnumerable<Point> antennae, BoundingBox limits)
    {
        List<Point> antinodes = [];
        for (int i = 0; i < antennae.Count(); i++)
        {
            Point antA = antennae.ElementAt(i);
            for (int j = i + 1; j < antennae.Count(); j++)
            {
                Point antB = antennae.ElementAt(j);
                var dist = antB - antA;
                Point candidateAntB = antB;
                do
                {
                    antinodes.Add(candidateAntB);
                    candidateAntB += dist;
                } while (limits.Contains(candidateAntB));
                Point candidateAntA = antA;
                do
                {
                    antinodes.Add(candidateAntA);
                    candidateAntA -= dist;
                } while (limits.Contains(candidateAntA));
            }
        }
        return antinodes;
    }

    private static char[,] Parse(string input) => input.SplitByLineBreak(StringSplitOptions.RemoveEmptyEntries).ParseGrid();

    private static Dictionary<char, IEnumerable<Point>> FindAntennae(char[,] grid) => grid
        .Cast<char>()
        .Distinct()
        .Where(c => c != '.')
        .ToDictionary(c => c, c => grid.FindAll(c));
}
