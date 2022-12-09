using AdventOfCode.Tools.Models;

var instructions = Input.ReadAllLines();

Console.WriteLine($"Part 1: {SimulateRope(2)}");
Console.WriteLine($"Part 2: {SimulateRope(10)}");

int SimulateRope(int knotCount)
{
    var knots = new Position[knotCount];
    HashSet<Position> visited = new() { knots[^1] };

    foreach (var line in instructions)
    {
        var steps = int.Parse(line[1..]);

        for (int i = 0; i < steps; i++)
        {
            knots[0] = knots[0].MoveInDirection(line[0]);

            for (int k = 1; k < knots.Length; k++)
                knots[k] = MoveFollower(knots[k - 1], knots[k]);

            visited.Add(knots[^1]);
        }
    }

    return visited.Count;
}

Position MoveFollower(Position leader, Position follower)
{
    var distance = leader.ChessDistance(follower);

    if (distance <= 1)
        return follower;

    var (distX, distY) = leader - follower;
    return new(follower.X + Math.Sign(distX), follower.Y + Math.Sign(distY));
}
