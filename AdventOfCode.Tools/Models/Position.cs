namespace AdventOfCode.Tools.Models;

public readonly struct Position
{
    public readonly int X;
    public readonly int Y;

    public Position(int x, int y)
    {
        X = x;
        Y = y;
    }

    public Position MoveInDirection(char dir)
    {
        return char.ToLower(dir) switch
        {
            'u' => new Position(X, Y - 1),
            'd' => new Position(X, Y + 1),
            'l' => new Position(X - 1, Y),
            'r' => new Position(X + 1, Y),
            _ => new Position(X, Y),
        };
    }

    public int ManhattanDistance(Position other) => (X, Y).ManhattanDistance((other.X, other.Y));
    public double DirectDistance(Position other) => (X, Y).DirectDistance((other.X, other.Y));
    public int ChessDistance(Position other) => (X, Y).ChessDistance((other.X, other.Y));
    public bool Equals(Position other) => X == other.X && Y == other.Y;
    public override bool Equals(object? obj) => obj is Position p && Equals(p);
    public override int GetHashCode() => HashCode.Combine(X.GetHashCode(), Y.GetHashCode());
    public override string ToString() => $"({X}, {Y})";

    public void Deconstruct(out int x, out int y)
    {
        x = X;
        y = Y;
    }

    public static bool operator ==(Position left, Position right) => left.Equals(right);
    public static bool operator !=(Position left, Position right) => !left.Equals(right);
    public static Position operator +(Position left, Position right) => new(left.X + right.X, left.Y + right.Y);
    public static Position operator +(Position left, (int X, int Y) right) => new(left.X + right.X, left.Y + right.Y);
    public static Position operator -(Position p) => new(-p.X, -p.Y);
    public static Position operator -(Position left, Position right) => new(left.X - right.X, left.Y - right.Y);
    public static Position operator -(Position left, (int X, int Y) right) => new(left.X - right.X, left.Y - right.Y);
    public static Position operator *(Position left, Position right) => new(left.X * right.X, right.Y * right.Y);
    public static Position operator *(Position left, (int X, int Y) right) => new(left.X * right.X, right.Y * right.Y);
    public static Position operator /(Position left, Position right) => new(left.X / right.X, right.Y / right.Y);
    public static Position operator /(Position left, (int X, int Y) right) => new(left.X / right.X, right.Y / right.Y);

    public IEnumerable<Position> GetNeighbours(bool includeDiagonal = false) => includeDiagonal ? GetAllNeighbours() : GetAdjacent();

    public static readonly (int X, int Y)[] CartesianDirections = new[] { (-1, 0), (0, -1), (1, 0), (0, 1) };
    public static readonly (int X, int Y)[] AllDirections = new[] { (-1, -1), (-1, 0), (-1, 1), (0, -1), (0, 1), (1, -1), (1, 0), (1, 1) };

    private IEnumerable<Position> GetAdjacent()
    {
        foreach (var dir in CartesianDirections)
            yield return this + dir;
    }

    private IEnumerable<Position> GetAllNeighbours()
    {
        foreach (var dir in AllDirections)
            yield return this + dir;
    }
}
