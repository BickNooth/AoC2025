namespace AoC.Day4;

public class Parts
{
    public static void Part1()
    {
        var grid = GetInput("input.txt");

        var accessibleRolls = 0;
        foreach (var cell in
                 from y in Enumerable.Range(0, grid.GetLength(1))
                 from x in Enumerable.Range(0, grid.GetLength(0))
                 select new { x, y })
        {
            if (grid[cell.x, cell.y] == '@') 
            {
                accessibleRolls += CountAdjacentRolls(grid, cell.x, cell.y, '@') < 4 ? 1 : 0;
            }
        }

        Console.WriteLine($"AccessibleRolls: {accessibleRolls}");
        PrintGrid(grid);
    }


    public static void Part2()
    {
        var grid = GetInput("input.txt");

        var accessibleRolls = 0;
        var previousAccessibleRolls = 1;
        while (accessibleRolls != previousAccessibleRolls)
        {
            previousAccessibleRolls = accessibleRolls;
            foreach (var cell in
                     from y in Enumerable.Range(0, grid.GetLength(1))
                     from x in Enumerable.Range(0, grid.GetLength(0))
                     select new { x, y })
            {
                if (grid[cell.x, cell.y] == '@')
                {
                    if (CountAdjacentRolls(grid, cell.x, cell.y, '@') < 4)
                    {
                        grid[cell.x, cell.y] = '.';
                        accessibleRolls++;
                    }
                }

                //Console.WriteLine($"({cell.x},{cell.y})");
                //PrintGrid(grid);
                //Console.WriteLine();
            }
        }

        Console.WriteLine($"AccessibleRolls: {accessibleRolls}");
        PrintGrid(grid);
    }

    private static char[,] GetInput(string inputLocation)
    {
        var path = Path.Combine(AppContext.BaseDirectory, "Day4", inputLocation);
        var lines = File.ReadAllLines(path)
            .Where(x => !string.IsNullOrWhiteSpace(x))
            .ToArray();

        var gridHeight = lines.Length;       
        var gridWidth = lines[0].Length;   
        var grid = new char[gridWidth, gridHeight]; 

        foreach (var cell in 
                      from y in Enumerable.Range(0, gridHeight)
                      from x in Enumerable.Range(0, gridWidth)
                      select new { x, y, newCell = lines[y][x] })
        {
            grid[cell.x, cell.y] = cell.newCell;
        }

        return grid;
    }

    private static int CountAdjacentRolls(char[,] grid, int x, int y, char searchCharacter)
    {
        var gridWidth = grid.GetLength(0);
        var gridHeight = grid.GetLength(1);
        var adjacentRollCount = 0;

        for (var offsetY = -1; offsetY <= 1; offsetY++)
        {
            for (var offsetX = -1; offsetX <= 1; offsetX++)
            {
                if (offsetX == 0 && offsetY == 0) continue;

                var adjacentX = x + offsetX;
                var adjacentY = y + offsetY;

                if (adjacentX < 0 || adjacentY < 0 || adjacentX >= gridWidth || adjacentY >= gridHeight) continue;

                if (grid[adjacentX, adjacentY] == searchCharacter) adjacentRollCount++;
            }
        }

        return adjacentRollCount;
    }

    private static void PrintGrid(char[,] grid)
    {
        var gridWidth = grid.GetLength(0);
        var gridHeight = grid.GetLength(1);
        for (var y = 0; y < gridHeight; y++)
        {
            var rowChars = new char[gridWidth];
            for (var x = 0; x < gridWidth; x++)
            {
                rowChars[x] = grid[x, y];
            }
            Console.WriteLine(new string(rowChars));
        }
    }
}
