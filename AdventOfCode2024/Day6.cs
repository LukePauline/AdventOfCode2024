using AdventOfCode2024.DataStructures;
using AdventOfCode2024.Helpers;


namespace AdventOfCode2024;
public class Day6 : IDay
{
    private static Point[] Directions = [(-1, 0), (0, 1), (1, 0), (0, -1)];

    public object Exercise1(string input)
    {
        var grid = Parse(input);

        HashSet<Point> visitedSqures = [];
        int turns = 0;
        Point pos = grid.FindInGrid('^');

        while (true)
        {
            visitedSqures.Add(pos);

            var nextPos = pos + GetDirection(turns);

            if (nextPos.X < 0 || nextPos.X >= grid.GetLength(0) || nextPos.Y < 0 && nextPos.Y >= grid.GetLength(1))
                return visitedSqures.Count;

            if (grid[nextPos.X, nextPos.Y] == '#')
                turns++;

            pos += GetDirection(turns);
        }
    }

    public object Exercise2(string input)
    {
        var grid = Parse(input);

        Point startPos = grid.FindInGrid('^');
        HashSet<(Point, int)> visitedSqures = [];
        HashSet<Point> newObstacles = [];
        int turns = 0;
        Point pos = startPos;

        while (true)
        {
            visitedSqures.Add((pos, turns % 4));

            var nextPos = pos + GetDirection(turns);

            if (nextPos.X < 0 || nextPos.X >= grid.GetLength(0) || nextPos.Y < 0 || nextPos.Y >= grid.GetLength(1))
            {
                return newObstacles.Count;
            }

            if (grid[nextPos.X, nextPos.Y] == '#')
            {
                turns++;
                continue;
            }
            else if (nextPos != startPos && PlacingObstactleWillResultInLoop(grid, turns, visitedSqures, pos))
                newObstacles.Add(nextPos);

            pos += GetDirection(turns);
        }
    }

    private static bool PlacingObstactleWillResultInLoop(char[,] grid, int turns, HashSet<(Point, int)> visitedSqures, Point pos)
    {
        Point startPos = pos;
        HashSet<(Point, int)> testVisitedSquares = [.. visitedSqures];
        Point newObstacle = pos + GetDirection(turns);


        while (true)
        {
            if (pos != startPos && testVisitedSquares.Contains((pos, turns % 4)))
                return true;

            testVisitedSquares.Add((pos, turns % 4));

            var nextPos = pos + GetDirection(turns);

            if (nextPos.X < 0 || nextPos.X >= grid.GetLength(0) || nextPos.Y < 0 || nextPos.Y >= grid.GetLength(1))
                return false;

            if (grid.GetAtPoint(nextPos) == '#' || nextPos == newObstacle)
            {
                turns++;
                continue;
            }

            pos = nextPos;
        }
    }

    private static void DrawOutput(char[,] grid, Point startPos, HashSet<Point> newObstacles, IEnumerable<Point> verts, IEnumerable<Point> hors)
    {
        for (int i = 0; i < grid.GetLength(0); i++)
        {
            Console.WriteLine();
            for (int j = 0; j < grid.GetLength(1); j++)
            {
                if ((i, j) == startPos)
                    Console.Write('^');
                else if (newObstacles.Contains((i, j)))
                    Console.Write('O');
                else if (verts.Contains((i, j)))
                    Console.Write('|');
                else if (hors.Contains((i, j)))
                    Console.Write('-');
                else
                    Console.Write(grid[i, j]);
            }
        }
    }

    private static char[,] Parse(string input) => input.SplitByLineBreak(StringSplitOptions.RemoveEmptyEntries).ParseGrid();

    private static Point GetDirection(int turns) => Directions[turns % 4];
}
