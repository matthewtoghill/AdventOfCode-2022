var input = Input.ReadAllLines()
                 .SelectMany(x => x.SplitOn('-', ',').Select(int.Parse))
                 .Chunk(4);

Console.WriteLine($"Part1: {input.Count(x => IsContainedWithin(x[0], x[1], x[2], x[3]))}");
Console.WriteLine($"Part2: {input.Count(x => HasOverlap(x[0], x[1], x[2], x[3]))}");

static bool IsContainedWithin(int startA, int endA, int startB, int endB)
    => (startA >= startB && endA <= endB) || (startB >= startA && endB <= endA);

static bool HasOverlap(int startA, int endA, int startB, int endB)
    => endA >= startB && endB >= startA;
