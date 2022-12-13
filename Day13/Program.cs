using System.Text.Json.Nodes;

namespace Day13;

public class Program
{
    static readonly string[] _input = Input.ReadAllLines();
    static void Main()
    {
        Console.WriteLine($"Part 1: {Part1()}");
        Console.WriteLine($"Part 2: {Part2()}");
    }

    static IEnumerable<JsonNode?> GetPackets(string[] input)
        => input.Where(x => !string.IsNullOrWhiteSpace(x))
                .Select(x => JsonNode.Parse(x));

    static int Part1()
    {
        var packetPairs = GetPackets(_input).Chunk(2).ToList();
        var result = 0;

        for (int i = 0; i < packetPairs.Count; i++)
            result += CompareNodes(packetPairs[i][0], packetPairs[i][1]) < 0 ? i + 1 : 0;

        return result;
    }

    static int Part2()
    {
        var packets = GetPackets(_input).ToList();

        var firstDivider = JsonNode.Parse("[[2]]");
        var secondDivider = JsonNode.Parse("[[6]]");

        packets.AddRange(new[] { firstDivider, secondDivider });

        packets.Sort(CompareNodes);

        var firstIndex = packets.IndexOf(firstDivider) + 1;
        var secondIndex = packets.IndexOf(secondDivider) + 1;

        return firstIndex * secondIndex;
    }

    static int CompareNodes(JsonNode left, JsonNode right)
        => (left, right) switch
        {
            (JsonValue x, JsonValue y) => (int)x - (int)y,
            (JsonValue, JsonArray y) => CompareNodes(new JsonArray((int)left), y),
            (JsonArray x, JsonValue) => CompareNodes(x, new JsonArray((int)right)),
            (JsonNode x, JsonNode y) => x.AsArray().Zip(y.AsArray())
                                                   .Select(a => CompareNodes(a.First!, a.Second!))
                                                   .FirstOrDefault(b => b != 0, x.AsArray().Count - y.AsArray().Count)
        };
}
