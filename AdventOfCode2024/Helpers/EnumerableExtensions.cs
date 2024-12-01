namespace AdventOfCode2024.Helpers;
public static class EnumerableExtensions
{
    public static IEnumerable<T> SelectNth<T>(this IEnumerable<T> enumerable, int n, int offset = 0)
    {
        for (int i = offset; i < enumerable.Count(); i += n)
            yield return enumerable.ElementAt(i);
    }
}
