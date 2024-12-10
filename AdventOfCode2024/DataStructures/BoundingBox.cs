using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2024.DataStructures;
public record BoundingBox()
{
    public Point TopLeft => new(Left, Top);
    public Point TopRight => new(Right, Top);
    public Point BottomLeft => new(Left, Bottom);
    public Point BottomRight => new(Right, Bottom);
    
    public int Top { get; }
    public int Bottom { get; }
    public int Left { get; }
    public int Right { get; }

    public BoundingBox(Point a, Point b) : this()
    {
        Top = Math.Min(a.Y, b.Y);
        Bottom = Math.Max(a.Y, b.Y);
        Left = Math.Min(a.X, b.X);
        Right = Math.Max(a.X, b.X);
    }

    public BoundingBox(int x1, int y1, int x2, int y2) : this()
    {
        Top = Math.Min(y1, y2);
        Bottom = Math.Max(y1, y2);
        Left = Math.Min(x1, x2);
        Right = Math.Max(x1, x2);
    }

    public bool Contains(Point point) => point.X >= Left && point.X < Right && point.Y >= Top && point.Y < Bottom;
}
