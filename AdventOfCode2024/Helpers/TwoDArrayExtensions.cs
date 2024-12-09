﻿using AdventOfCode2024.DataStructures;

namespace AdventOfCode2024.Helpers;
public static class TwoDArrayExtensions
{
    public static IEnumerable<T> GetSubArrayVector<T>(this T[,] grid, int x, int y, int dx, int dy)
    {
        int magnitude = Math.Max(Math.Abs(dx), Math.Abs(dy));
        int stepX = Math.Sign(dx);
        int stepY = Math.Sign(dy);

        if (x + stepX * (magnitude - 1) < 0)
            return [];
        if (x + stepX * (magnitude - 1) >= grid.GetLength(0))
            return [];
        if (y + stepY * (magnitude - 1) < 0)
            return [];
        if (y + stepY * (magnitude - 1) >= grid.GetLength(1))
            return [];

        return Enumerable.Range(0, magnitude).Select(i => grid[x + stepX * i, y + stepY * i]);
    }
    public static IEnumerable<T> GetSubArrayVector<T>(this T[,] grid, Point position, Point vector) =>
        GetSubArrayVector(grid, position.X, position.Y, vector.X, vector.Y);

    public static Point FindInGrid<T>(this T[,] grid, T value)
    {
        for (int x = 0; x < grid.GetLength(0); x++)
        {
            for (int y = 0; y < grid.GetLength(1); y++)
            {
                if (grid[x, y]?.Equals(value) == true)
                    return (x, y);
            }
        }
        throw new InvalidDataException();
    }

    public static IEnumerable<Point> FindAll<T>(this T[,] grid, T value)
    {
        for (int x = 0; x < grid.GetLength(0); x++)
        {
            for (int y = 0; y < grid.GetLength(1); y++)
            {
                if (grid[x, y]?.Equals(value) == true)
                    yield return (x, y);
            }
        }
        throw new InvalidDataException();
    }

    public static T GetAtPoint<T>(this T[,] grid, Point point) => grid[point.X, point.Y];
}
