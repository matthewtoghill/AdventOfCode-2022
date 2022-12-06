var input = Input.ReadAll().AsSpan();

Console.WriteLine($"Part 1: {GetIndexAfterFirstUniqueSet(input, 4)}");
Console.WriteLine($"Part 2: {GetIndexAfterFirstUniqueSet(input, 14)}");

static int GetIndexAfterFirstUniqueSet(ReadOnlySpan<char> span, int setLength)
{
    for (int i = setLength; i < span.Length; i++)
    {
        var set = new HashSet<char>(span.Slice(i - setLength, setLength).ToArray());
        if (set.Count == setLength)
            return i;
    }

    return 0;
}
