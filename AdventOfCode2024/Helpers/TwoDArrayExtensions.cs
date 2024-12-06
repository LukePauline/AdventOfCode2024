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
}
