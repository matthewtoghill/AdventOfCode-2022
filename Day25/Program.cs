using System.Text;

var input = Input.ReadAllLines();

Console.WriteLine($"Part 1: {LongToSnafu(input.Sum(SnafuToLong))}");

static long SnafuToLong(string snafu) => snafu.Aggregate(0L, (result, c) => (result * 5) + SnafuDigitToDecimal(c));

static string LongToSnafu(long number)
{
    var sb = new StringBuilder();
    while (number > 0)
    {
        var digit = number % 5;
        if (digit.IsAnyOf(3, 4)) digit -= 5;
        number = (number - digit) / 5;
        sb.Insert(0, DecimalToSnafuDigit(digit));
    }
    return sb.ToString();
}

static long SnafuDigitToDecimal(char snafuDigit) => snafuDigit switch
{
    '2' => 2,
    '1' => 1,
    '0' => 0,
    '-' => -1,
    '=' => -2
};

static char DecimalToSnafuDigit(long d) => d switch
{
    2 => '2',
    1 => '1',
    0 => '0',
    -1 => '-',
    -2 => '='
};
