namespace Day19;

public class Program
{
    private static readonly Blueprint[] _blueprints = Input.ReadAllLines().Select(ParseBlueprint).ToArray();
    private static void Main()
    {
        Console.WriteLine($"Part 1: {Part1(_blueprints)}");
        Console.WriteLine($"Part 2: {Part2(_blueprints.Take(3).ToArray())}");
    }

    private static int Part1(Blueprint[] blueprints)
    {
        var results = new List<(int id, int geodes)>();
        foreach (var blueprint in blueprints)
        {
            var maxGeodes = GenerateMaxGeodes(blueprint, 24);
            results.Add((blueprint.Id, maxGeodes));
        }
        return results.Select(x => x.id * x.geodes).Sum();
    }

    private static int Part2(Blueprint[] blueprints)
    {
        return blueprints.Select(x => GenerateMaxGeodes(x, 32)).Product();
    }

    private static int GenerateMaxGeodes(Blueprint blueprint, int totalMinutes)
    {
        var states = new HashSet<RobotState>();
        var initialState = new RobotState(0, 0, 0, 0, 1, 0, 0, 0, totalMinutes);
        var frontier = new Queue<RobotState>();
        frontier.Enqueue(initialState);
        var maxGeodes = 0;

        while(frontier.Count > 0)
        {
            var (ore, clay, obsidian, geodes, oreBots, clayBots, obsidianBots, geodeBots, minsLeft) = frontier.Dequeue();

            maxGeodes = int.Max(maxGeodes, geodes);

            if (minsLeft < 1)
                continue;

            ore = int.Min(ore, (minsLeft * blueprint.MaxOreRequired) - (oreBots * (minsLeft - 1)));
            clay = int.Min(clay, (minsLeft * blueprint.ObsidianCost.Clay) - (clayBots * (minsLeft - 1)));
            obsidian = int.Min(obsidian, (minsLeft * blueprint.GeodeCost.Obsidian) - (obsidianBots * (minsLeft - 1)));

            var state = new RobotState(ore, clay, obsidian, geodes, oreBots, clayBots, obsidianBots, geodeBots, minsLeft);

            if (states.Contains(state)) continue;

            states.Add(state);

            // don't build anything
            var baseState = new RobotState(ore + oreBots,
                                           clay + clayBots,
                                           obsidian + obsidianBots,
                                           geodes + geodeBots,
                                           oreBots,
                                           clayBots,
                                           obsidianBots,
                                           geodeBots,
                                           minsLeft - 1);

            frontier.Enqueue(baseState);

            // try to build an ore robot if additional resources are still needed
            if (ore >= blueprint.OreCost && oreBots < blueprint.MaxOreRequired)
                frontier.Enqueue(baseState with
                {
                    Ore = ore + oreBots - blueprint.OreCost,
                    OreBots = oreBots + 1
                });

            // try to build a clay robot if additional resources are still needed
            if (ore >= blueprint.ClayCost && clayBots < blueprint.ObsidianCost.Clay)
                frontier.Enqueue(baseState with
                {
                    Ore = ore + oreBots - blueprint.ClayCost,
                    ClayBots = clayBots + 1
                });

            // try to build an obsidian robot if additional resources are still needed
            if (ore >= blueprint.ObsidianCost.Ore && clay >= blueprint.ObsidianCost.Clay && obsidianBots < blueprint.GeodeCost.Obsidian)
                frontier.Enqueue(baseState with
                {
                    Ore = ore + oreBots - blueprint.OreCost,
                    Clay = clay + clayBots - blueprint.ObsidianCost.Clay,
                    ObsidianBots = obsidianBots + 1
                });

            // try to build a geode robot
            if (ore >= blueprint.GeodeCost.Ore && obsidian >= blueprint.GeodeCost.Obsidian)
                frontier.Enqueue(baseState with
                {
                    Ore = ore + oreBots - blueprint.OreCost,
                    Obsidian = obsidian + obsidianBots - blueprint.GeodeCost.Obsidian,
                    GeodeBots = geodeBots + 1
                });
        }

        return maxGeodes;
    }

    private static Blueprint ParseBlueprint(string line) => new (line);
}

public record RobotState(int Ore, int Clay, int Obsidian, int Geodes, int OreBots, int ClayBots, int ObsidianBots, int GeodeBots, int MinsLeft);
