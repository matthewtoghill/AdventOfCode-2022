using AdventOfCode.Tools.Models;

namespace Day12;

public class Program
{
    private static readonly string[] _input = Input.ReadAllLines();
    private static void Main()
    {
        var (grid, bottom, top) = ParseGrid(_input);
        Console.WriteLine($"Part 1: {StepsToGoalBFS(grid, top, bottom, 0)}"); // start from the top, get distance to bottom
        Console.WriteLine($"Part 2: {StepsToGoalBFS(grid, top, bottom, 1)}"); // start from the top, get distance to bottom or first at height 1
    }

    private static int StepsToGoalBFS(int[,] grid, Position start, Position goal, int exitOnFirstOfHeight)
    {
        var frontier = new Queue<Position>();
        frontier.Enqueue(start);
        var cameFrom = new Dictionary<Position, Position>();
        var end = goal;

        while (frontier.Count > 0)
        {
            var current = frontier.Dequeue();

            // End the search if the current location is the goal
            // or the current height is the first instance of the required height
            if (current == goal || grid[current.X, current.Y] == exitOnFirstOfHeight)
            {
                end = current;
                break;
            }

            // Create the list of adjacent neighbours from the current grid position
            // where the height difference is 1 or less (and neighbour is within grid)
            var neighbours = new List<Position>();
            foreach (var neighbour in current.GetNeighbours())
            {
                try
                {
                    var heightDiff = grid[current.X, current.Y] - grid[neighbour.X, neighbour.Y];
                    if (heightDiff <= 1)
                        neighbours.Add(neighbour);
                } catch (IndexOutOfRangeException) { }
            }

            // Check if the neighbour has been reached yet, if not add it to the cameFrom dictionary
            foreach (var neighbour in neighbours)
            {
                if (!cameFrom.ContainsKey(neighbour))
                {
                    frontier.Enqueue(neighbour);
                    cameFrom[neighbour] = current;
                }
            }
        }

        // Follow path backwards to get the path length
        var path = new List<Position>();
        var step = end;
        while (step != start)
        {
            path.Add(step);
            step = cameFrom[step];
        }

        return path.Count;
    }

    private static (int[,] grid, Position start, Position end) ParseGrid(string[] input)
    {
        var grid = new int[input.Length, input[0].Length];
        var start = new Position(0, 0);
        var end = new Position(0, 0);

        for (int row = 0; row < input.Length; row++)
        {
            for (int col = 0; col < input[row].Length; col++)
            {
                var c = input[row][col];
                if (c == 'S') start = new Position(row, col);
                if (c == 'E') end = new Position(row, col);
                grid[row, col] = GetHeight(c);
            }
        }

        return (grid, start, end);
    }

    private static int GetHeight(char c) => c switch
    {
        'S' => 0,
        'E' => 27,
        _ => c - 'a' + 1
    };
}
