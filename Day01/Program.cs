namespace Day01;

public class Program
{
    private static readonly string input = File.ReadAllText(@"..\..\..\..\data\day01.txt");
    private static void Main()
    {
        var elves = input.Split(new[] { "\n\n" }, StringSplitOptions.RemoveEmptyEntries)
                         .Select(x => x.Split('\n').Select(int.Parse).Sum())
                         .OrderDescending()
                         .ToList();

        Console.WriteLine($"Part 1: {elves[0]}");
        Console.WriteLine($"Part 2: {elves.Take(3).Sum()}");
    }
}