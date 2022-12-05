var _input = Input.ReadAsParagraphs().ToArray();
var _instructions = GetInstructions();

Console.WriteLine($"Part 1: {Part1()}");
Console.WriteLine($"Part 2: {Part2()}");

string Part1()
{
    var stacks = GetCrateStacks(_input[0].SplitOn('\n'));

    foreach (var (quantity, from, to) in _instructions)
        for (int i = 0; i < quantity; i++)
            stacks[to].Push(stacks[from].Pop());

    return new string(stacks.Select(x => x.Peek()).ToArray());
}

string Part2()
{
    var stacks = GetCrateStacks(_input[0].SplitOn('\n'));

    foreach (var (quantity, from, to) in _instructions)
    {
        var tempStack = new Stack<char>();
        for (int i = 0; i < quantity; i++)
            tempStack.Push(stacks[from].Pop());

        var count = tempStack.Count;
        for (int i = 0; i < count; i++)
            stacks[to].Push(tempStack.Pop());
    }

    return new string(stacks.Select(x => x.Peek()).ToArray());
}

List<Stack<char>> GetCrateStacks(string[] cratesInput)
{
    var stackTotal = cratesInput.Last().SplitAs<int>(" ").Max();
    var stacks = Enumerable.Range(0, stackTotal).Select(_ => new Stack<char>()).ToList();

    for (int i = cratesInput.Length - 2; i >= 0; i--)
    {
        var crates = cratesInput[i];
        var stackIndex = 0;
        for (int j = 1; j < crates.Length; j += 4)
        {
            if (char.IsLetter(crates[j]))
                stacks[stackIndex].Push(crates[j]);

            stackIndex++;
        }
    }

    return stacks;
}

List<(int quantity, int from, int to)> GetInstructions()
{
    var instructionsInput = _input[1].SplitOn('\n');
    List<(int quantity, int from, int to)> instructions = new();

    foreach (var line in instructionsInput)
    {
        var split = line.Split(" ");
        instructions.Add(new(int.Parse(split[1]), int.Parse(split[3]) - 1, int.Parse(split[5]) - 1));
    }

    return instructions;
}
