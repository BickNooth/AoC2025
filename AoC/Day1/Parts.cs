namespace AoC.Day1;

public class Parts
{
    private class Dial
    {
        public int MaxIndex { get; set; }
        public int CurrentIndex { get; set; }
        public int TimesHitZero { get; set; }
        public int TimesPassedZero { get; set; }

        public void SetCurrentIndex(bool left, int amount)
        {
            if (left)
            {
                for (var i = 0; i < amount; i++)
                {
                    CurrentIndex -= 1;
                    switch (CurrentIndex)
                    {
                        case 0:
                            TimesPassedZero++;
                            break;
                        case < 0:
                            CurrentIndex = MaxIndex;
                            break;
                    }
                }
            }
            else
            {
                for (var i = 0; i < amount; i++)
                {
                    CurrentIndex += 1;
                    if (CurrentIndex > MaxIndex)
                    {
                        CurrentIndex = 0;
                        TimesPassedZero++;
                    }
                }
            }
            if (CurrentIndex == 0)
                TimesHitZero += 1;
        }
    }
    public static void Part1()
    {
        const int startingPosition = 50;
        const int dialSize = 99;
        var instructions = GetInput("input.txt");
        var dial = new Dial { MaxIndex = dialSize, CurrentIndex = startingPosition };

        foreach (var instruction in instructions)
        {
            var direction = instruction[0];
            var amount = int.Parse(instruction[1..]);
            dial.SetCurrentIndex(direction == 'L', amount);
        }

        Console.WriteLine($"Hit zero {dial.TimesHitZero} times");
    }


    public static void Part2()
    {
        const int startingPosition = 50;
        const int dialSize = 99;
        var instructions = GetInput("input.txt");
        var dial = new Dial { MaxIndex = dialSize, CurrentIndex = startingPosition };

        foreach (var instruction in instructions)
        {
            var direction = instruction[0];
            var amount = int.Parse(instruction[1..]);
            dial.SetCurrentIndex(direction == 'L', amount);
            Console.WriteLine($"Instruction: {instruction} Current index: {dial.CurrentIndex}");
        }

        Console.WriteLine($"Hit zero {dial.TimesPassedZero} times");
    }

    private static string[] GetInput(string inputLocation)
    {
        var path = Path.Combine(AppContext.BaseDirectory, "Day1", inputLocation);
        return File.ReadAllLines(path)
            .Where(x => !string.IsNullOrWhiteSpace(x))
            .ToArray();
    }
}
