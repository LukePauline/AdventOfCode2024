using AdventOfCode2024.DataStructures;
using AdventOfCode2024.Helpers;
using System.Text.RegularExpressions;

namespace AdventOfCode2024;
public partial class Day15 : IDay
{
    public object Exercise1(string input)
    {
        (Content[,] content, Point[] instructions, Point robot) = Parse(input);

        foreach (var instruction in instructions)
        {
            if (AttemptMove(content, robot, instruction))
                robot += instruction;
        }
        return content.FindAll(Content.Box).Sum(p => 100 * p.Y + p.X);
    }

    public object Exercise2(string input)
    {
        (Content[,] content, Point[] instructions, Point robot) = Parse2(input);

        foreach (var instruction in instructions)
        {
            if (AttemptMove2(content, robot, instruction))
                robot += instruction;
        }
        return content.FindAll(Content.BoxL).Sum(p => 100 * p.Y + p.X);
    }

    private static bool AttemptMove(Content[,] grid, Point location, Point instruction)
    {
        Point target = location + instruction;
        if (!grid.GetBounds().Contains(target))
            return false;

        if (grid.GetAtPoint(target) == Content.Wall)
            return false;

        if (grid.GetAtPoint(target) == Content.Box && !AttemptMove(grid, target, instruction))
            return false;

        grid[target.Y, target.X] = grid.GetAtPoint(location);
        grid[location.Y, location.X] = Content.Empty;
        return true;
    }

    private static bool AttemptMove2(Content[,] grid, Point location, Point instruction)
    {
        Point target = location + instruction;
        if (!grid.GetBounds().Contains(target))
            return false;

        if (grid.GetAtPoint(target) is Content.BoxL or Content.BoxR && instruction is (0, 1) or (0, -1))
        {
            var (targetL, targetR) = grid.GetAtPoint(target) == Content.BoxL ? (target, target + (1, 0)) : (target + (-1, 0), target);
            var tempGrid = (Content[,])grid.Clone();
            if (AttemptMove2(tempGrid, targetL, instruction) && AttemptMove2(tempGrid, targetR, instruction))
            {
                tempGrid.CopyOver(grid);
                grid[target.Y, target.X] = grid.GetAtPoint(location);
                grid[location.Y, location.X] = Content.Empty;
                return true;
            }
            return false;
        }
        else
        {
            if (grid.GetAtPoint(target) == Content.Wall)
                return false;

            if (grid.GetAtPoint(target) is Content.BoxL or Content.BoxR && !AttemptMove2(grid, target, instruction))
                return false;

            grid[target.Y, target.X] = grid.GetAtPoint(location);
            grid[location.Y, location.X] = Content.Empty;
        }
        return true;
    }

    private static void DrawState(Content[,] grid)
    {
        for (int i = 0; i < grid.GetLength(0); i++)
        {
            Console.WriteLine();
            for (int j = 0; j < grid.GetLength(1); j++)
            {
                Console.Write((char)grid[i, j]);
            }
        }
        Console.WriteLine();
        Console.WriteLine();
    }

    private enum Content
    {
        Empty = '.',
        Wall = '#',
        Box = 'O',
        BoxL = '[',
        BoxR = ']',
        Robot = '@'
    }

    private (Content[,] content, Point[] instructions, Point startLocation) Parse(string input)
    {
        var parts = input.SplitByEmptyLine();

        Content[,] content = parts[0]
            .SplitByLineBreak(StringSplitOptions.RemoveEmptyEntries)
            .Select(x => x.Select(y => (Content)y))
            .ParseGrid();

        Point startLocation = content.FindInGrid(Content.Robot);

        Point[] instructions = parts[1]
            .Select<char, Point?>(c => c switch
            {
                '<' => (-1, 0),
                '>' => (1, 0),
                '^' => (0, -1),
                'v' => (0, 1),
                _ => null
            })
            .Where(c => c != null)
            .ToArray()!;

        return (content, instructions, startLocation);
    }
    private (Content[,] content, Point[] instructions, Point startLocation) Parse2(string input)
    {
        var parts = input.SplitByEmptyLine();

        var content = parts[0]
            .SplitByLineBreak(StringSplitOptions.RemoveEmptyEntries)
            .Select(x => x.SelectMany<char, Content>(y => y switch
            {
                '.' => [Content.Empty, Content.Empty],
                '#' => [Content.Wall, Content.Wall],
                'O' => [Content.BoxL, Content.BoxR],
                '@' => [Content.Robot, Content.Empty],
                _ => throw new InvalidDataException()
            }))
            .ParseGrid();

        Point startLocation = content.FindInGrid(Content.Robot);

        Point[] instructions = parts[1]
            .Select<char, Point?>(c => c switch
            {
                '<' => (-1, 0),
                '>' => (1, 0),
                '^' => (0, -1),
                'v' => (0, 1),
                _ => null
            })
            .Where(c => c != null)
            .ToArray()!;

        return (content, instructions, startLocation);
    }

    [GeneratedRegex(@"\[\]")]
    private static partial Regex Boxes();
}
