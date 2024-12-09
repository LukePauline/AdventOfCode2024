using AdventOfCode2024.DataStructures;
using AdventOfCode2024.Helpers;


namespace AdventOfCode2024;
public class Day6 : IDay
{
    private static Point[] Directions = [(-1, 0), (0, 1), (1, 0), (0, -1)];
    private Point? StartPos;

    public object Exercise1(string input)
    {
        var grid = Parse(input);

        return TraverseGrid(grid).Item1.Count();
    }

    public object Exercise2(string input)
    {
        var grid = Parse(input);

        IEnumerable<Point> originalPath = TraverseGrid(grid).path;

        return originalPath.Skip(1).Count(p => TraverseGrid(grid, p).loopDetected);
    }


    private (IEnumerable<Point> path, bool loopDetected) TraverseGrid(char[,] grid, Point? newObstucruction = null)
    {
        int turns = 0;
        List<(Point, int)> visitedSqures = [];
        StartPos = grid.FindInGrid('^');
        Point pos = StartPos;

        while (true)
        {
            if (visitedSqures.Contains((pos, turns % 4)))
                return (visitedSqures.Select(x => x.Item1).Distinct(), true);

            visitedSqures.Add((pos, turns % 4));

            var nextPos = pos + GetDirection(turns);

            if (nextPos.X < 0 || nextPos.X >= grid.GetLength(0) || nextPos.Y < 0 || nextPos.Y >= grid.GetLength(1))
                return (visitedSqures.Select(x => x.Item1).Distinct(), false);

            if (grid.GetAtPoint(nextPos) == '#' || nextPos == newObstucruction)
            {
                turns++;
                continue;
            }

            pos = nextPos;
        }
    }

    private static char[,] Parse(string input) => input.SplitByLineBreak(StringSplitOptions.RemoveEmptyEntries).ParseGrid();

    private static Point GetDirection(int turns) => Directions[turns % 4];
}
