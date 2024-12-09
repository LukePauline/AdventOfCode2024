namespace AdventOfCode2024.DataStructures;
public record Point(int X, int Y)
{
    public static Point operator +(Point a, Point b) => new(a.X + b.X, a.Y + b.Y);
    public static Point operator -(Point a, Point b) => new(a.X - b.X, a.Y - b.Y);
    public static Point operator *(Point a, int b) => new(a.X * b, a.Y * b);

    public static implicit operator Point((int, int) value) => new(value.Item1, value.Item2);
}
