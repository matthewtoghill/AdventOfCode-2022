using AdventOfCode.Tools.Models;

namespace Day18;

public class Program
{
    private static readonly HashSet<Position3D> _cubes = GetCubes(Input.ReadAllLines());
    static void Main()
    {
        Console.WriteLine($"Part 1: {Part1()}");
        Console.WriteLine($"Part 2: {Part2()}");
    }

    private static int Part1() => (_cubes.Count * 6) - _cubes.SelectMany(c => c.GetNeighbours()).Count(_cubes.Contains);

    private static int Part2()
    {
        var minRange = new Position3D(_cubes.Min(c => c.X) - 1, _cubes.Min(c => c.Y) - 1, _cubes.Min(c => c.Z) - 1);
        var maxRange = new Position3D(_cubes.Max(c => c.X) + 1, _cubes.Max(c => c.Y) + 1, _cubes.Max(c => c.Z) + 1);
        var water = FloodFill(_cubes, minRange, maxRange);
        return _cubes.SelectMany(c => c.GetNeighbours()).Count(water.Contains);
    }

    private static HashSet<Position3D> FloodFill(HashSet<Position3D> cubes, Position3D start, Position3D end)
    {
        var result = new HashSet<Position3D>();
        var frontier = new Queue<Position3D>();

        result.Add(start);
        frontier.Enqueue(start);

        while (frontier.Count > 0)
        {
            var current = frontier.Dequeue();
            foreach (var neighbour in current.GetNeighbours())
            {
                if (!result.Contains(neighbour) && !cubes.Contains(neighbour) && neighbour.IsBetween(start, end))
                {
                    result.Add(neighbour);
                    frontier.Enqueue(neighbour);
                }
            }
        }

        return result;
    }

    private static HashSet<Position3D> GetCubes(string[] input)
    {
        var result = new HashSet<Position3D>();

        foreach (var line in input)
        {
            var split = line.SplitAs<int>(",").ToArray();
            result.Add(new Position3D(split[0], split[1], split[2]));
        }

        return result;
    }
}