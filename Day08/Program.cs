var _input = Input.ReadAllLines();
(int x, int y)[] _directions = new[] { (-1, 0), (0, -1), (1, 0), (0, 1) };

var (visibleFromOutside, scenicScore) = SurveyTrees(_input);

Console.WriteLine($"Part 1: {visibleFromOutside}");
Console.WriteLine($"Part 2: {scenicScore}");

(int visibleCount, int scenicScore) SurveyTrees(string[] input)
{
    var visibleCount = (input.Length - 1) * 4;
    var scores = new List<int>();

    for (int x = 1; x < input.Length - 1; x++)
    {
        for (int y = 1; y < input[x].Length - 1; y++)
        {
            var isVisible = false;
            var score = 1;

            foreach (var dir in _directions) // check each cardinal direction
            {
                var step = 1; // number of trees away from the current tree in a direction
                while (true)
                {
                    try
                    {
                        if (input[x + (dir.x * step)][y + (dir.y * step)] >= input[x][y])
                        {
                            score *= step;
                            break;
                        }
                        step++;
                    }
                    catch // handle exceeding the bounds of the grid
                    {
                        score *= step - 1;
                        isVisible = true;
                        break;
                    }
                }
            }

            scores.Add(score);
            if (isVisible) visibleCount++;
        }
    }

    return (visibleCount, scores.Max());
}
