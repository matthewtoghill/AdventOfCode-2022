namespace AdventOfCode.Tools;

public static class Input
{
    public static string ReadAll()
        => File.ReadAllText(@$"..\..\..\..\data\{System.Reflection.Assembly.GetEntryAssembly()!.GetName().Name}.txt");

    public static string ReadAll(int day)
        => File.ReadAllText(@$"..\..\..\..\data\day{day:00}.txt");

    public static string[] ReadAllLines()
        => File.ReadAllLines(@$"..\..\..\..\data\{System.Reflection.Assembly.GetEntryAssembly()!.GetName().Name}.txt");

    public static string[] ReadAllLines(int day)
        => File.ReadAllLines(@$"..\..\..\..\data\day{day:00}.txt");

    public static IEnumerable<T> ReadAllLinesAs<T>() where T : IParsable<T>
        => ReadAll().Split(new[] { "\n", "\r\n" }, StringSplitOptions.RemoveEmptyEntries)
                    .Select(x => T.Parse(x, null));

    public static IEnumerable<T> ReadAllLinesAs<T>(int day) where T : IParsable<T>
        => ReadAll(day).Split(new[] { "\n", "\r\n" }, StringSplitOptions.RemoveEmptyEntries)
                       .Select(x => T.Parse(x, null));

    public static IEnumerable<T> SplitAs<T>(this string text) where T : IParsable<T>
        => text.Split(new[] { "\n", "\r\n" }, StringSplitOptions.RemoveEmptyEntries)
               .Select(x => T.Parse(x, null));

    public static string[] SplitOn(this string text, StringSplitOptions splitOptions, params string[] separators)
        => text.Split(separators, splitOptions);

    public static string[] SplitOn(this string text, params string[] separators)
        => text.Split(separators, StringSplitOptions.None);

    public static string[] SplitOn(this string text, StringSplitOptions splitOptions, params char[] separators)
        => text.Split(separators, splitOptions);

    public static string[] SplitOn(this string text, params char[] separators)
        => text.Split(separators, StringSplitOptions.None);

    public static IEnumerable<string> ReadAsParagraphs()
        => ReadAll().Split(new[] { "\n\n", "\r\n\r\n" }, StringSplitOptions.RemoveEmptyEntries);

    public static IEnumerable<string> ReadAsParagraphs(int day)
        => ReadAll(day).Split(new[] { "\n\n", "\r\n\r\n" }, StringSplitOptions.RemoveEmptyEntries);
}
