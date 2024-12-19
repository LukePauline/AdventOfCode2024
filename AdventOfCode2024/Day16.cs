using AdventOfCode2024.DataStructures;
using AdventOfCode2024.Helpers;
using System.Diagnostics;
using System.Linq;
using System.Xml.Linq;

namespace AdventOfCode2024;
public class Day16 : IDay
{
    public object Exercise1(string input)
    {
        var grid = Parse(input);
        (Point, Point) start = (grid.FindInGrid('S'), (1, 0));
        Point end = grid.FindInGrid('E');
        return FindShortestPathDistance(grid, start, end);
    }
    private static long FindShortestPathDistance(char[,] grid, (Point, Point) start, Point end)
    {
        BoundingBox bounds = grid.GetBounds();

        SortedSet<((Point location, Point direction) key, long shortestDistance)> toProcess = new(new LocationDirectionDistanceComparer())
        {
            (start,0)
        };
        Dictionary<(Point location, Point direction), long> shortestDistances = new()
        {
            [start] = 0
        };
        HashSet<(Point location, Point direction)> processedNodes = [];

        while (toProcess.Count != 0)
        {
            var node = toProcess.First();
            processedNodes.Add(node.key);
            toProcess.Remove(node);
            var (location, direction) = node.key;
            var shortestDistance = node.shortestDistance;

            (Point, Point)[] turns = [(location, direction.RotateCcw()), (location, direction.RotateCw())];
            foreach (var turn in turns)
            {
                var newShortestDistance = shortestDistance + 1000;
                if (!shortestDistances.TryGetValue(turn, out long curr) || curr > newShortestDistance)
                {
                    toProcess.Remove((turn, curr));
                    shortestDistances[turn] = newShortestDistance;
                }
                if (!processedNodes.Contains(turn))
                    toProcess.Add((turn, shortestDistances[turn]));
            }

            var forward = (location: location + direction, direction);
            if (grid.GetAtPoint(forward.location) == '#')
                continue;

            if (!shortestDistances.TryGetValue(forward, out var currShortestDistance) || currShortestDistance > shortestDistance + 1)
            {
                toProcess.Remove((forward, currShortestDistance));
                shortestDistances[forward] = shortestDistance + 1;
            }
            if (!processedNodes.Contains(forward))
                toProcess.Add((forward, shortestDistances[forward]));
        }

        return shortestDistances.Where(x => x.Key.location == end).Min(x => x.Value);
    }

    public object Exercise2(string input)
    {
        var grid = Parse(input);
        (Point location, Point direction) start = (grid.FindInGrid('S'), (1, 0));
        Point end = grid.FindInGrid('E');
        long shortestTotalPath = (long)Exercise1(input);

        List<IEnumerable<Point>> paths = [];

        Dictionary<(Point location, Point direction), long> visited = [];
        Queue<((Point location, Point direction) key, long distance, Point[] path)> queue = new();
        queue.Enqueue((start, 0, [start.location]));
        visited[start] = 0;
        while (queue.TryDequeue(out var node))
        {
            var (location, direction) = node.key;
            var dist = node.distance;

            if (dist + 1000 < shortestTotalPath)
            {
                (Point, Point)[] turns = [(location, direction.RotateCcw()), (location, direction.RotateCw())];
                foreach (var turn in turns)
                {
                    if (!visited.TryGetValue(turn, out var prev) || dist + 1000 <= prev)
                    {
                        queue.Enqueue((turn, dist + 1000, node.path));
                        visited[turn] = dist + 1000;
                    }
                }
            }

            var forward = (location: location + direction, direction);
            if (grid.GetAtPoint(forward.location) == '#')
                continue;
            if (dist + 1 > shortestTotalPath)
                continue;

            if (!visited.TryGetValue(forward, out var prevForward) || dist + 1 <= prevForward)
            {
                Point[] newPath = [.. node.path, forward.location];
                if (forward.location == end)
                    paths.Add(newPath);
                queue.Enqueue((forward, dist + 1, newPath));
                visited[forward] = dist + 1;
            }
        }
        return paths.SelectMany(x => x).Distinct().Count();
    }

    private static char[,] Parse(string input) => input.SplitByLineBreak(StringSplitOptions.RemoveEmptyEntries).ParseGrid();

    private class LocationDirectionDistanceComparer : IComparer<((Point location, Point direction) key, long distance)>
    {
        public int Compare(((Point location, Point direction) key, long distance) x, ((Point location, Point direction) key, long distance) y)
        {
            if (x.distance == y.distance)
            {
                if (x.key == y.key)
                    return 0;
                return -1;
            }
            if (x.distance < y.distance)
                return -1;
            return 1;
        }
    }
}
