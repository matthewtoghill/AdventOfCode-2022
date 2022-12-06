var input = Input.ReadAll().AsSpan();

Console.WriteLine($"Part 1: {GetIndexOfFirstUniqueSet(input, 4)}");
Console.WriteLine($"Part 2: {GetIndexOfFirstUniqueSet(input, 14)}");

static int GetIndexOfFirstUniqueSet(ReadOnlySpan<char> span, int setLength)
{
    for (int i = setLength - 1; i < span.Length; i++)
    {
        var set = new HashSet<char>(span.Slice(i - setLength + 1, setLength).ToArray());
        if (set.Count == setLength)
            return i + 1;
    }

    return 0;
}
