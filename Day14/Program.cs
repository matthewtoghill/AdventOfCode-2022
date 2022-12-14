using AdventOfCode.Tools.Models;

namespace Day14;

public class Program
{
    static readonly string[] _input = Input.ReadAllLines();
    static void Main()
    {
        Console.WriteLine($"Part 1: {Part1()}");
        Console.WriteLine($"Part 2: {Part2()}");
    }

    static int Part1()
    {
        var cave = GenerateCave(false);
        var rocks = cave.Count;
        var floor = cave.Max(p => p.Y);

        // Add sand to the cave, exit when the sand has fallen below the floor height
        while (!AddSand(cave, s => s.Y > floor)) { }

        return cave.Count - rocks;
    }

    static int Part2()
    {
        var cave = GenerateCave(true);
        var rocks = cave.Count;
        var source = new Position(500, 0);

        // Add sand to the cave, exit when the cave is 'full' as it contains sand at the source
        while (!AddSand(cave, _ => cave.Contains(source))) { }

        return cave.Count - rocks;
    }

    static bool AddSand(HashSet<Position> cave, Func<Position, bool> stopAddingSandWhen)
    {
        var sand = new Position(500, 0);

        while (true)
        {
            if (stopAddingSandWhen(sand))
                return true;

            sand += (0, 1); // sand falls down 1 position
            if (!cave.Contains(sand)) continue; // check if position is blocked (exists in cave already)
            if (!cave.Contains(sand + (-1, 0))) { sand += (-1, 0); continue; } // try moving left if blocked below
            if (!cave.Contains(sand + (1, 0))) { sand += (1, 0); continue; }   // try moving right if blocked left and below

            cave.Add(sand + (0, -1)); // add sand to cave at the available position
            break;
        }

        return false; // sand added but condition not met to stop adding sand yet
    }

    static HashSet<Position> GenerateCave(bool includeFloor)
    {
        var cave = new HashSet<Position>();
        foreach (var line in _input)
        {
            var points = line.SplitAs<int>(",", " -> ").Chunk(2).ToArray();

            for (int i = 1; i < points.Length; i++)
            {
                int startX = points[i - 1][0];
                int startY = points[i - 1][1];
                int endX = points[i][0];
                int endY = points[i][1];

                if (startX > endX)
                    (startX, endX) = (endX, startX);

                if (startY > endY)
                    (startY, endY) = (endY, startY);

                for (int x = startX; x <= endX; x++)
                    for (int y = startY; y <= endY; y++)
                        cave.Add(new Position(x, y));
            }
        }

        if (includeFloor)
        {
            var minX = cave.Min(p => p.X);
            var maxX = cave.Max(p => p.X);
            var height = cave.Max(p => p.Y) + 2;

            // add floor, use the height to determine the width needed
            for (int x = minX - height; x < maxX + height; x++)
                cave.Add(new Position(x, height));
        }

        return cave;
    }
}
