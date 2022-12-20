var _input = Input.ReadAllLinesAs<long>().ToArray();

Console.WriteLine($"Part 1: {MixNumbers(_input, 1, 1)}");
Console.WriteLine($"Part 2: {MixNumbers(_input, 10, 811589153)}");

static long MixNumbers(long[] numbers, int iterations, long decryptionKey)
{
    var nums = numbers.Select(x => x * decryptionKey).ToList();
    var indexes = Enumerable.Range(0, nums.Count).ToList();

    for (int n = 0; n < iterations; n++)
    {
        for (int i = 0; i < indexes.Count; i++)
        {
            var oldIndex = indexes.IndexOf(i);
            indexes.RemoveAt(oldIndex);
            var newIndex = (int)Mod(oldIndex + nums[i], indexes.Count);
            indexes.Insert(newIndex, i);
        }
    }

    var indexOfZero = indexes.IndexOf(nums.IndexOf(0));
    return Enumerable.Range(1, 3).Select(x => nums[indexes[(int)Mod(indexOfZero + (1000 * x), nums.Count)]]).Sum();
}

static long Mod(long k, long n) => ((k %= n) < 0) ? k + n : k;
