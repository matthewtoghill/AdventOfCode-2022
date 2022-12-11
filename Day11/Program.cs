using Day11;

var _input = Input.ReadAsParagraphs().ToArray();

Console.WriteLine($"Part 1: {Part1()}");
Console.WriteLine($"Part 2: {Part2()}");

long Part1()
{
    var monkeys = ParseMonkeys(_input);
    const int reliefValue = 3;

    for (int r = 0; r < 20; r++)
    {
        foreach (var monkey in monkeys)
        {
            while (monkey.Items.Count > 0)
            {
                var item = monkey.Items.Dequeue();
                monkey.ItemsInspected++;
                item = monkey.Operation(item);
                item /= reliefValue;
                monkeys[item % monkey.Test == 0 ? monkey.TrueIndex : monkey.FalseIndex].Items.Enqueue(item);
            }
        }
    }

    return monkeys.Select(x => x.ItemsInspected).OrderDescending().Take(2).Product();
}

long Part2()
{
    var monkeys = ParseMonkeys(_input);
    var reliefValue = monkeys.Select(x => x.Test).Product();

    for (int r = 0; r < 10_000; r++)
    {
        foreach (var monkey in monkeys)
        {
            while (monkey.Items.Count > 0)
            {
                var item = monkey.Items.Dequeue();
                monkey.ItemsInspected++;
                item = monkey.Operation(item);
                item %= reliefValue;
                monkeys[item % monkey.Test == 0 ? monkey.TrueIndex : monkey.FalseIndex].Items.Enqueue(item);
            }
        }
    }

    return monkeys.Select(x => x.ItemsInspected).OrderDescending().Take(2).Product();
}

List<Monkey> ParseMonkeys(string[] input)
{
    var monkeys = new List<Monkey>();
    foreach (var monkey in input)
    {
        var split = monkey.SplitOn("\n", "\r\n");
        var items = split[1][18..].SplitAs<long>(",").ToArray();
        Func<long, long> operation = split[2][19..].Split() switch
        {
            ["old", "*", "old"] => x => x * x,
            ["old", "+", "old"] => x => x + x,
            ["old", "+", var num] => x => x + int.Parse(num),
            ["old", "*", var num] => x => x * int.Parse(num),
            _ => throw new Exception()
        };
        var test = int.Parse(split[3].Split().Last());
        var trueIndex = int.Parse(split[4][^1..]);
        var falseIndex = int.Parse(split[5][^1..]);

        monkeys.Add(new Monkey(items, operation, test, trueIndex, falseIndex));
    }

    return monkeys;
}
