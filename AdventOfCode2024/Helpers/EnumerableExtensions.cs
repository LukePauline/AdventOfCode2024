namespace AdventOfCode2024.Helpers;
public static class EnumerableExtensions
{
    public static IEnumerable<T> SelectNth<T>(this IEnumerable<T> enumerable, int n, int offset = 0)
    {
        for (int i = offset; i < enumerable.Count(); i += n)
            yield return enumerable.ElementAt(i);
    }

    public static T[,] ParseGrid<T>(this IEnumerable<IEnumerable<T>> enumerable)
    {
        var rows = enumerable.Count();
        var cols = enumerable.First().Count();
        T[,] grid = new T[rows, cols];
        for (int i = 0; i < rows; i++)
        {
            var row = enumerable.ElementAt(i);
            if (row.Count() > cols)
                throw new InvalidDataException($"Enumerable does not conform to a grid on line {i}");
            for (int j = 0; j < cols; j++)
            {
                grid[i, j] = row.ElementAt(j);
            }
        }
        return grid;
    }
}
