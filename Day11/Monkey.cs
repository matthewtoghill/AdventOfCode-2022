namespace Day11;

public class Monkey
{
    public Queue<long> Items { get; set; }
    public Func<long, long> Operation { get; set; }
    public int Test { get; set; }
    public int TrueIndex { get; set; }
    public int FalseIndex { get; set; }
    public long ItemsInspected { get; set; } = 0;

    public Monkey(long[] items, Func<long, long> operation, int test, int trueIndex, int falseIndex)
    {
        Items = new Queue<long>(items);
        Operation = operation;
        Test = test;
        TrueIndex = trueIndex;
        FalseIndex = falseIndex;
    }
}
