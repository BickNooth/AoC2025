namespace AoC.Day2;

public class Parts
{
    public static void Part1()
    {
        var productRanges = GetInput("input.txt");
        var invalidProductIds = new List<long>();
        foreach (var range in productRanges)
        {
            for (var i = range.FirstId; i <= range.LastId; i++)
            {
                if (!IsValidId(i))
                {
                    invalidProductIds.Add(i);    
                }
            }
        }

        Console.WriteLine(invalidProductIds.Sum());
    }


    public static void Part2()
    {
        var productRanges = GetInput("input.txt");
        var invalidProductIds = new List<long>();
        foreach (var range in productRanges)
        {
            for (var id = range.FirstId; id <= range.LastId; id++)
            {
                if (!IsValidIdPart2(id))
                {
                    invalidProductIds.Add(id);
                }
            }
        }

        Console.WriteLine(invalidProductIds.Sum());
    }

    private class ProductRange
    {
        public long FirstId { get; init; }
        public long LastId { get; init; }
    }

    private static ProductRange[] GetInput(string inputLocation)
    {
        var path = Path.Combine(AppContext.BaseDirectory, "Day2", inputLocation);
        return File.ReadAllLines(path)
            .Where(x => !string.IsNullOrWhiteSpace(x))
            .SelectMany(x => x.Split(','))
            .Select(x =>
            {
                var parts = x.Split('-');
                return new ProductRange
                {
                    FirstId = long.Parse(parts[0]),
                    LastId = long.Parse(parts[1])
                };
            })
            .ToArray();
    }

    private static bool IsValidId(long id)
    {
        var productString = id.ToString();

        if ((productString.Length % 2) == 1) return true;

        var midpointIndex = productString.Length / 2;
        return !productString[..midpointIndex]
            .Equals(productString[midpointIndex..], StringComparison.InvariantCulture);
    }

    private static bool IsValidIdPart2(long id)
    {
        var productString = id.ToString();
        var length = productString.Length;
        var midpointIndex = productString.Length / 2;

        for (var patternLength = 1; patternLength <= midpointIndex; patternLength++)
        {
            if (length % patternLength != 0 || length / patternLength < 2) continue; 

            var repeatingPatterns = true;
            for (var offset = patternLength; offset < length && repeatingPatterns; offset += patternLength)
            {
                if (!productString[offset..(offset + patternLength)]
                    .Equals(productString[..patternLength], StringComparison.InvariantCulture))
                {
                    repeatingPatterns = false;
                }
            }

            if (repeatingPatterns) return false; 
        }

        return true; 
    }
}
