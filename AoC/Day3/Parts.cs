using System.Diagnostics;

namespace AoC.Day3;

public class Parts
{
    public static void Part1()
    {
        var banks = GetInput("input.txt");
        var joltageSum = 0;
        foreach (var bank in banks)
        {
            var highestJoltageIndex = bank.Joltages.IndexOf(bank.Joltages.Max());
            if (highestJoltageIndex == bank.Joltages.Length - 1)
            {
                highestJoltageIndex = bank.Joltages[..highestJoltageIndex].IndexOf(bank.Joltages[..highestJoltageIndex].Max());
            }

            var nextHighestJoltageIndex =
                bank.Joltages[(highestJoltageIndex+1)..].IndexOf(bank.Joltages[(highestJoltageIndex+1)..].Max()) + highestJoltageIndex + 1;
            joltageSum += int.Parse($"{bank.Joltages[highestJoltageIndex]}{bank.Joltages[nextHighestJoltageIndex]}");
        }

        Console.WriteLine($"Joltage Sum: {joltageSum}");
    }


    public static void Part2()
    {
        var stopwatch = new Stopwatch();
        stopwatch.Start();

        var banks = GetInput("input.txt");
        var joltageSum = banks.Sum(bank => GetMax12DigitNumber(bank.Joltages));

        stopwatch.Stop();
        Console.WriteLine($"Joltage Sum: {joltageSum}. Completed in {stopwatch.Elapsed.TotalSeconds} seconds");
    }

    private static long GetMax12DigitNumber(int[] bankJoltages)
    {
        var bufferLimit = bankJoltages.Length - 12 + 1;
        var joltageBuilder = 0L;
        for (var i = 0; i < bankJoltages.Length; i++)
        {
            if ((int)Math.Ceiling(Math.Log10(joltageBuilder)) == 12) break;    
            if (bufferLimit == 0) joltageBuilder = CombineNumbers(joltageBuilder, bankJoltages[i]);
            
            var upperLimit = (i + bufferLimit) > bankJoltages.Length ? bankJoltages.Length : (i + bufferLimit);
            var joltagesToCompare = bankJoltages[i..upperLimit];
            
            var maxJoltage = joltagesToCompare.Max();
            var indexOfMaxJoltage = joltagesToCompare.IndexOf(maxJoltage);
            
            bufferLimit -= indexOfMaxJoltage;
            if (indexOfMaxJoltage > 0) i += indexOfMaxJoltage;

            joltageBuilder = CombineNumbers(joltageBuilder, maxJoltage);
        }

        return joltageBuilder;
    }

    private static long CombineNumbers(long a, long b) => 
        a * (long)Math.Pow(10, // raise 10 to the number of digits in b to shift left that number of places
                b == 0 ? 1 : (long)Math.Ceiling( // due to inaccuracies, 2 will be 1.99999 etc, round up
                                Math.Log10(b))) // number of digits in b
                            + b; // add b to the shifted a with space for b

    private class Bank
    {
        public int[] Joltages { get; init; }
    }

    private static Bank[] GetInput(string inputLocation)
    {
        var path = Path.Combine(AppContext.BaseDirectory, "Day3", inputLocation);
        var lines = File.ReadAllLines(path)
            .Where(x => !string.IsNullOrWhiteSpace(x));

        return lines
            .Select(line => new Bank
            {
                Joltages = line
                    .Select(x => int.Parse(x.ToString()))
                    .ToArray()
            })
            .ToArray();
    }
}
