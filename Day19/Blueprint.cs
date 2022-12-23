namespace Day19;

public class Blueprint
{
    public int Id { get; private set; }
    public int OreCost { get; private set; }
    public int ClayCost { get; private set; }
    public (int Ore, int Clay)  ObsidianCost { get; private set; }
    public (int Ore, int Obsidian) GeodeCost { get; private set; }
    public int MaxOreRequired { get; private set; }

    public Blueprint(string line)
    {
        var vals = line.ExtractInts().ToArray();
        Id = vals[0];
        OreCost = vals[1];
        ClayCost = vals[2];
        ObsidianCost = (vals[3], vals[4]);
        GeodeCost = (vals[5], vals[6]);
        MaxOreRequired = new int[] { OreCost, ClayCost, ObsidianCost.Ore, GeodeCost.Ore }.Max();
    }
}
