using AdventOfCode2024.DataStructures;
using System.Collections.Immutable;
using System.Diagnostics;
using System.Text.RegularExpressions;

namespace AdventOfCode2024;
public partial class Day14 : IDay
{
    public object Exercise1(string input) => Exercise1(input, 101, 103);

    public object Exercise2(string input)
    {
        var robots = Parse(input);
        for (int i = 0; true; i++)
        {
            var positions = robots.Select(x => x.Move(101, 103, i));
            if (ContainsChristmasTree(positions))
                return i;
        }
    }

    public bool ContainsChristmasTree(IEnumerable<Point> positions)
    {
        var rows = positions.GroupBy(x => x.X, x => x.Y).ToImmutableSortedDictionary(x => x.Key, y => y.ToArray());
        foreach (var row in rows)
            foreach (var point in row.Value)
            {
                bool found = true;
                for (int i = 1; i < 4; i++)
                {
                    int triNum = 1 + 2 * i;
                    IEnumerable<int> xs = Enumerable.Range(point - triNum / 2, triNum);
                    if (!xs.All(x => rows.TryGetValue(row.Key + i, out int[]? points) && points.Contains(x)))
                    {
                        found = false;
                        break;
                    }
                }
                if (found)
                    return true;
            }
        return false;
    }

    public int Exercise1(string input, int width, int height)
    {
        var robots = Parse(input);
        var initial = robots.Select(x => x.Position);

        //Console.WriteLine();
        //Console.WriteLine("Initial");
        //DrawPositions(initial, width, height, false);

        var positions = robots
            .Select(r => r.Move(width, height, 100));

        //Console.WriteLine();
        //Console.WriteLine("After 100s");
        //DrawPositions(positions, width, height, false);
        //Console.WriteLine();
        //Console.WriteLine("Exclude mid");
        //DrawPositions(positions, width, height, true);
        //Console.WriteLine();

        var quads = positions
        .GroupBy(p =>
        {
            if (p.X < width / 2 && p.Y < height / 2) return 1;
            if (p.X > width / 2 && p.Y < height / 2) return 2;
            if (p.X < width / 2 && p.Y > height / 2) return 3;
            if (p.X > width / 2 && p.Y > height / 2) return 4;
            return 0;
        })
        .Where(x => x.Key != 0)
        .Select(g => g.Count());

        Console.WriteLine(quads.Index().Aggregate("", (str, x) => $"{str} {x.Index}:{x.Item}"));

        return quads.Aggregate((a, b) => a * b);
    }

    private void DrawPositions(IEnumerable<Point> positions, int width, int height, bool excludeMid)
    {
        for (int i = 0; i < height; i++)
        {

            Console.WriteLine();
            if (excludeMid && i == height / 2)
            {
                Console.Write(new string(Enumerable.Repeat('#', width).ToArray()));
                continue;
            }
            for (int j = 0; j < width; j++)
            {
                if (excludeMid && j == width / 2)
                    Console.Write('#');
                else if (positions.Contains((j, i)))
                    Console.Write(positions.Count(x => x == (j, i)));
                else
                    Console.Write('.');
            }
        }
    }

    private class Robot(Point initialPosition, Point velocity)
    {
        public Point Position { get; set; } = initialPosition;
        public Point Velocity = velocity;

        public Point Move(int gridWidth, int gridHeight, int times = 1)
        {
            Position += (Velocity * times);
            Position = Position with
            {
                X = Position.X % gridWidth,
                Y = Position.Y % gridHeight
            };

            if (Position.X < 0)
                Position = Position with { X = Position.X + gridWidth };
            if (Position.Y < 0)
                Position = Position with { Y = Position.Y + gridHeight };
            return Position;
        }
    }

    private static IEnumerable<Robot> Parse(string input) =>
        RobotParser().Matches(input)
        .Select(x =>
            new Robot(
                (int.Parse(x.Groups[1].Value), int.Parse(x.Groups[2].Value)),
                (int.Parse(x.Groups[3].Value), int.Parse(x.Groups[4].Value))));

    [GeneratedRegex(@"p=(\d+),(\d+) v=(-?\d+),(-?\d+)")]
    private static partial Regex RobotParser();
}
