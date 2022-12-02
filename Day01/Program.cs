var elves = Input.ReadAsParagraphs()
                 .Select(x => x.SplitAs<int>().Sum())
                 .OrderDescending()
                 .ToArray();

Console.WriteLine($"Part 1: {elves[0]}");
Console.WriteLine($"Part 2: {elves.Take(3).Sum()}");
