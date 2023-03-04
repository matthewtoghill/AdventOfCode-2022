namespace Day16;

public class Program
{
    private static readonly string[] _input = Input.ReadAllLines();
    private static void Main()
    {
        Console.WriteLine($"Part 1: {Solve(_input, false, 30)}");
        Console.WriteLine($"Part 2: {Solve(_input, true, 26)}");
    }

    private static int Solve(string[] input, bool withElephant, int time)
    {
        var cave = ParseValves(input);
        var start = cave.Valves.Single(x => x.Name == "AA");
        var valvesToOpen = cave.Valves.Where(valve => valve.FlowRate > 0).ToArray();
        var cache = new Dictionary<string, int>();

        if (!withElephant)
            return MaxFlow(cache, cave, start, valvesToOpen.ToHashSet(), time);

        return Combinations(valvesToOpen).Max(x => MaxFlow(cache, cave, start, x.Human, time)
                                                 + MaxFlow(cache, cave, start, x.Elephant, time));
    }

    private static IEnumerable<(HashSet<Valve> Human, HashSet<Valve> Elephant)> Combinations(Valve[] valves)
    {
        var maxMask = 1 << (valves.Length - 1);

        for (var mask = 0; mask < maxMask; mask++)
        {
            var elephant = new HashSet<Valve>();
            var human = new HashSet<Valve>();

            elephant.Add(valves[0]);

            for (var ivalve = 1; ivalve < valves.Length; ivalve++)
            {
                if ((mask & (1 << ivalve)) == 0)
                {
                    human.Add(valves[ivalve]);
                }
                else
                {
                    elephant.Add(valves[ivalve]);
                }
            }

            yield return (human, elephant);
        }
    }

    private static int MaxFlow(Dictionary<string, int> cache,
                               Cave cave,
                               Valve currentValve,
                               HashSet<Valve> valves,
                               int remainingTime)
    {
        string key = $"{remainingTime}-{currentValve.Id}-{string.Join("-", valves.OrderBy(x => x.Id).Select(x => x.Id))}";

        if (!cache.ContainsKey(key))
        {
            var flowFromValve = currentValve.FlowRate * remainingTime;
            var flowFromRest = 0;

            foreach (var valve in valves.ToArray())
            {
                var distance = cave.Distances[currentValve.Id, valve.Id];

                if (remainingTime >= distance + 1)
                {
                    valves.Remove(valve);
                    remainingTime -= distance + 1;
                    flowFromRest = Math.Max(flowFromRest, MaxFlow(cache, cave, valve, valves, remainingTime));
                    remainingTime += distance + 1;
                    valves.Add(valve);
                }
            }

            cache[key] = flowFromValve + flowFromRest;
        }

        return cache[key];
    }

    private static Cave ParseValves(string[] input)
    {
        var valves = input.Select(x => new Valve(x))
                          .OrderByDescending(v => v.FlowRate)
                          .ApplyEach((x, i) => x.Id = i)
                          .ToArray();

        return new Cave(FloydWarshallDistances(valves), valves);
    }

    private static int[,] FloydWarshallDistances(Valve[] valves)
    {
        const int ignoreVal = int.MaxValue / 10;
        var count = valves.Length;
        var distances = new int[count, count];

        for (int i = 0; i < count; i++)
            for (int j = 0; j < count; j++)
                distances[i, j] = valves[i].Tunnels.Contains(valves[j].Name) ? 1 : ignoreVal;

        for (int k = 0; k < count; k++)
            for (int i = 0; i < count; i++)
                for (int j = 0; j < count; j++)
                    distances[i, j] = Math.Min(distances[i, j], distances[i, k] + distances[k, j]);

        return distances;
    }
}

public record Cave(int[,] Distances, Valve[] Valves);

public class Valve
{
    public int Id { get; set; } = 0;
    public string Name { get; private set; }
    public int FlowRate { get; private set; }
    public string[] Tunnels { get; private set; }

    public Valve(string line)
    {
        var split = line.SplitOn(" ", "=", ";", ",");
        Name = split[1];
        FlowRate = int.Parse(split[5]);
        Tunnels = line.SplitOn("valves", "valve")[1].SplitOn(", ").Select(x => x.Trim()).ToArray();
    }
}
