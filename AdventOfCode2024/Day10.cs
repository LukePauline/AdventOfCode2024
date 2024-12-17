using AdventOfCode2024.DataStructures;
using AdventOfCode2024.Helpers;

namespace AdventOfCode2024;
public class Day10 : IDay
{
    private readonly static Point[] Directions = [(0, 1), (1, 0), (0, -1), (-1, 0)];
    public object Exercise1(string input)
    {
        int[,] grid = Parse(input);
        BoundingBox bounds = grid.GetBounds();
        Point[] trailHeads = grid.FindAll(0).ToArray();
        Dictionary<Point, int> scores = [];
        foreach (Point trailHead in trailHeads)
        {
            scores.Add(trailHead, 0);
            List<Point> visited = [trailHead];
            Queue<Point> queue = [];
            queue.Enqueue(trailHead);

            while (queue.TryDequeue(out var current))
            {
                int value = grid.GetAtPoint(current);
                var neighbours = Directions
                    .Select(d => current + d)
                    .Where(bounds.Contains)
                    .Where(p => grid.GetAtPoint(p) == value + 1)
                    .Where(p => !visited.Contains(p))
                    .ToArray();

                foreach (var neighbor in neighbours)
                {
                    visited.Add(neighbor);
                    if (grid.GetAtPoint(neighbor) == 9)
                        scores[trailHead]++;
                    else
                        queue.Enqueue(neighbor);
                }
            }
        }
        return scores.Sum(x => x.Value);
    }

    public object Exercise2(string input)
    {
        int[,] grid = Parse(input);
        BoundingBox bounds = grid.GetBounds();
        Point[] trailHeads = grid.FindAll(0).ToArray();
        Dictionary<Point, int> scores = [];
        foreach (Point trailHead in trailHeads)
        {
            scores.Add(trailHead, 0);
            List<Point> visited = [trailHead];
            Queue<Point> queue = [];
            queue.Enqueue(trailHead);

            while (queue.TryDequeue(out var current))
            {
                int value = grid.GetAtPoint(current);
                var neighbours = Directions
                    .Select(d => current + d)
                    .Where(bounds.Contains)
                    .Where(p => grid.GetAtPoint(p) == value + 1)
                    .ToArray();

                foreach (var neighbor in neighbours)
                {
                    if (grid.GetAtPoint(neighbor) == 9)
                        scores[trailHead]++;
                    else
                        queue.Enqueue(neighbor);
                }
            }
        }
        return scores.Sum(x => x.Value);
    }

    private static int[,] Parse(string input) => input.SplitByLineBreak(StringSplitOptions.RemoveEmptyEntries).Select(x => x.Select(c => c - 48)).ParseGrid();

}
