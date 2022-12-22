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

    public Position MoveInDirection(char direction)
    {
        return char.ToLower(direction) switch
        {
            'u' => new(X, Y - 1),
            'd' => new(X, Y + 1),
            'l' => new(X - 1, Y),
            'r' => new(X + 1, Y),
            _ => new(X, Y),
        };
    }

    public Position MoveInOppositeDirection(char direction)
    {
        return char.ToLower(direction) switch
        {
            'u' => new(X, Y + 1),
            'd' => new(X, Y - 1),
            'l' => new(X + 1, Y),
            'r' => new(X - 1, Y),
            _ => new(X, Y),
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

    public static bool operator ==(Position a, Position b) => a.Equals(b);
    public static bool operator !=(Position a, Position b) => !a.Equals(b);
    public static Position operator +(Position a, Position b) => new(a.X + b.X, a.Y + b.Y);
    public static Position operator +(Position a, (int X, int Y) b) => new(a.X + b.X, a.Y + b.Y);
    public static Position operator -(Position p) => new(-p.X, -p.Y);
    public static Position operator -(Position a, Position b) => new(a.X - b.X, a.Y - b.Y);
    public static Position operator -(Position a, (int X, int Y) b) => new(a.X - b.X, a.Y - b.Y);
    public static Position operator *(Position a, Position b) => new(a.X * b.X, b.Y * b.Y);
    public static Position operator *(Position a, (int X, int Y) b) => new(a.X * b.X, b.Y * b.Y);
    public static Position operator /(Position a, Position b) => new(a.X / b.X, b.Y / b.Y);
    public static Position operator /(Position a, (int X, int Y) b) => new(a.X / b.X, b.Y / b.Y);

    public bool IsBetween(Position min, Position max)
        => min.X <= X && X <= max.X
        && min.Y <= Y && Y <= max.Y;

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
