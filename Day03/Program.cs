namespace Day03;

public static class Program
{
    private static readonly string[] input = Input.ReadAllLines();
    private static void Main()
    {
        Console.WriteLine($"Part 1: {Part1(input)}");
        Console.WriteLine($"Part 2: {Part2(input)}");
    }

    private static int Part1(string[] bags) => bags.Sum(x => HalveAndGetIntersections(x).First().GetPriorityScore());
    private static int Part2(string[] bags) => bags.Chunk(3).Sum(x => x[0].Intersect(x[1]).Intersect(x[2]).First().GetPriorityScore());

    private static char[] HalveAndGetIntersections(string line) => GetIntersections(line[..(line.Length / 2)], line[(line.Length / 2)..]);
    private static char[] GetIntersections(string left, string right) => left.Intersect(right).ToArray();
    private static int GetPriorityScore(this char c) => char.IsLower(c) ? c - 'a' + 1 : c - 'A' + 27;
}
