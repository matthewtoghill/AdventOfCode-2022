using System.Numerics;

namespace AdventOfCode.Tools;

public static class EnumerableExtensions
{
    public static void ForEach<T>(this IEnumerable<T> items, Action<T> action)
    {
        foreach (T item in items)
            action(item);
    }

    public static void ForEach<T>(this IEnumerable<T> items, Action<T, int> action)
    {
        var index = 0;
        foreach (T item in items)
            action(item, index++);
    }

    public static IEnumerable<T> ApplyEach<T>(this IEnumerable<T> items, Action<T> action)
    {
        foreach (T item in items)
        {
            action(item);
            yield return item;
        }
    }

    public static IEnumerable<T> ApplyEach<T>(this IEnumerable<T> items, Action<T, int> action)
    {
        var index = 0;
        foreach (T item in items)
        {
            action(item, index++);
            yield return item;
        }
    }

    public static IEnumerable<T> Without<T>(this IEnumerable<T> items, T value)
        => items.Where(x => !Equals(x, value));

    public static IEnumerable<KeyValuePair<T, int>> GetFrequencies<T>(this IEnumerable<T> items) where T : notnull
        => items.GroupBy(x => x).ToDictionary(x => x.Key, x => x.Count());

    public static T Product<T>(this IEnumerable<T> items) where T : INumber<T>
        => items.Aggregate((total, x) => total * x);

    public static IEnumerable<T> Flatten<T>(this IEnumerable<IEnumerable<T>> items)
        => items.SelectMany(x => x);

    public static bool IsAnyOf<T>(this T item, params T[] items)
        => items.Contains(item);
}
