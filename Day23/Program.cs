using AdventOfCode.Tools.Models;

namespace Day23;

public class Program
{
    private static readonly string[] _input = Input.ReadAllLines();
    private static void Main()
    {
        var (emptyAfter10Rounds, totalRoundsUntilNoMovements) = SimulateElfMovements();
        Console.WriteLine($"Part 1: {emptyAfter10Rounds}");
        Console.WriteLine($"Part 1: {totalRoundsUntilNoMovements}");
    }

    private static (int, int) SimulateElfMovements()
    {
        var elves = LoadStartingElfPositions();
        var directionIndex = 0; // 0 = N, 1 = S, 2 = W, 3 = E
        var directions = new List<(int X, int Y)[]>
        {
            new[] { (-1, -1), (0, -1), (1, -1) },   // north: NW / N / NE
            new[] { (-1,  1), (0,  1), (1,  1) },   // south: SW / S / SE
            new[] { (-1, -1), (-1, 0), (-1, 1) },   // west:  NW / W / SW
            new[] { (1,  -1), (1,  0), (1,  1) }    // east:  NE / E / SE
        };

        var round = 0;
        var emptyAfter10Rounds = 0;

        while(true)
        {
            round++;
            var proposedLocations = new List<(Position From, Position To)>();

            foreach (var elf in elves)
            {
                // If the elf has no neighbours then it stays where it is
                if (!elf.GetNeighbours(true).Any(elves.Contains)) continue;

                // Check each direction, the directionIndex is used to rotate which direction is checked first
                for (int i = 0; i < 4; i++)
                {
                    // Check each of the 3 points in a direction (e.g. NW/N/NE for north) for any elves
                    // if none found then propose the elf moves in that direction
                    if (!directions[(directionIndex + i) % 4].Any(d => elves.Contains(elf + d)))
                    {
                        // add a proposed movement To a new location
                        proposedLocations.Add((elf, elf + directions[(directionIndex + i) % 4][1]));
                        break;
                    }
                }
            }

            directionIndex = (directionIndex + 1) % 4;

            // Get the list of locations where only 1 elf has proposed to move there
            var validLocations = proposedLocations.GroupBy(p => p.To)
                                                  .Where(g => g.Count() == 1)
                                                  .Select(x => x.First()).ToList();

            // Move the elves to their new locations where the location was valid
            foreach (var (from, to) in validLocations)
            {
                elves.Remove(from);
                elves.Add(to);
            }


            if (round == 10)
            {
                var width = elves.Min(e => e.X) - elves.Max(e => e.X) - 1;
                var height = elves.Min(e => e.Y) - elves.Max(e => e.Y) - 1;
                emptyAfter10Rounds = (width * height) - elves.Count;
            }

            // No elves moved, exit the loop
            if (validLocations.Count == 0) break;
        }

        return (emptyAfter10Rounds, round);
    }

    private static HashSet<Position> LoadStartingElfPositions()
    {
        var elves = new HashSet<Position>();
        for (int row = 0; row < _input.Length; row++)
        {
            for (int col = 0; col < _input[0].Length; col++)
            {
                if (_input[row][col] == '#')
                    elves.Add(new(col, row));
            }
        }
        return elves;
    }
}
