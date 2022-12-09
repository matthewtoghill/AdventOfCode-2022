namespace AdventOfCode.Tools;

public static class NumericExtensions
{
    public static bool IsBetween<T>(this T value, T min, T max) where T : IComparable<T>
    {
        return min.CompareTo(value) <= 0 && value.CompareTo(max) <= 0;
    }

    public static int ManhattanDistance(this (int X, int Y) startPos, (int X, int Y) endPos)
    {
        return Math.Abs(startPos.X - endPos.X) + Math.Abs(startPos.Y - endPos.Y);
    }

    public static double DirectDistance(this (int X, int Y) startPos, (int X, int Y) endPos)
    {
        return Math.Sqrt(Math.Pow(endPos.X - startPos.X, 2) + Math.Pow(endPos.Y - startPos.Y, 2));
    }

    public static int ChessDistance(this (int X, int Y) startPos, (int X, int Y) endPos)
    {
        return Math.Max(Math.Abs(startPos.X - endPos.X), Math.Abs(startPos.Y - endPos.Y));
    }
}
