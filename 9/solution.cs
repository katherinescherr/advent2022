using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using System.Text.RegularExpressions;


public class RopeGrid {
    public char[,] grid;

    public (int x, int y) tailIndex;

    public (int x, int y) headIndex;

    public (int x, int y)[] ropeArray;

    public RopeGrid(int x, int y, int count) {
        grid = new char[y,x];
        for (var i = 0; i < x; i++)
        {
            for (var j = 0; j < y; j++)
            {
                grid[j,i] = '.';
            }
        }
        ropeArray = new (int x, int y)[count];
        for (var i = 0; i < count; i++)
        {
            ropeArray[i] = (x: x / 2, y: y / 2);
        }
        grid[x / 2, y / 2] = '#';
    }

    public void moveRope(string direction, int count)
    {
        for (var i = 0; i < count; i++)
        {
            switch (direction)
            {
                case "R":
                    ropeArray[0].x += 1;
                    break;
                case "L":
                    ropeArray[0].x -= 1;
                    break;
                case "U":
                    ropeArray[0].y += 1;
                    break;
                case "D":
                    ropeArray[0].y -= 1;
                    break;
            }
            for (var index = 1; index < ropeArray.Length; index++)
            {
                if (Math.Abs(ropeArray[index - 1].x - ropeArray[index].x) > 1 || Math.Abs(ropeArray[index - 1].y - ropeArray[index].y) > 1) {
                    if ((ropeArray[index - 1].x - ropeArray[index].x) > 0) {
                        ropeArray[index].x += 1;
                    } else if ((ropeArray[index].x - ropeArray[index - 1].x) > 0) {
                        ropeArray[index].x -= 1;
                    }
                    if ((ropeArray[index - 1].y - ropeArray[index].y) > 0) {
                        ropeArray[index].y += 1;
                    } else if ((ropeArray[index].y - ropeArray[index - 1].y) > 0) {
                        ropeArray[index].y -= 1;
                    }
                }
            }
            grid[ropeArray.Last().y, ropeArray.Last().x] = '#';
        }
    }

    public int tailCount() {
        var count = 0;
        for (var y = 0; y < grid.GetLength(0); y++)
        {
            for (var x = 0; x < grid.GetLength(1); x++)
            {
                if (grid[y,x] == '#')
                {
                    count += 1;
                }
            }
        }
        return count;
    }

    public void printGrid(bool printTailStops) {
        for (var y = grid.GetLength(0) - 1; y >= 0; y--)
        {
            for (var x = 0; x < grid.GetLength(1); x++)
            {
                if (printTailStops) {
                    Console.Write(grid[y,x]);
                } else {
                    var printChar = ".";

                    for (var i = ropeArray.Count() - 1; i >= 0; i--)
                    {
                        if (x == ropeArray[i].x && y == ropeArray[i].y)
                        {
                            printChar = (i == 0) ? "H" : $"{i}";
                        }
                    }
                    Console.Write(printChar);
                }
            }
            Console.WriteLine("");
        }
        Console.WriteLine("");
    }
}


public class DayNine {
    public static void Main() 
    {
        List<string> lines = parseFile("input.txt");

        int partAResult = partA(lines);
        Console.WriteLine($"Part A: {partAResult}");

        int partBResult = partB(lines);
        Console.WriteLine($"Part B: {partBResult}");
    }


    private static List<string> parseFile(string filename)
    {
        StreamReader reader = File.OpenText(filename);
        string line;
        List<string> lines = new List<string>();
        while ((line = reader.ReadLine()) != null) 
        {
            lines.Add(line);
        }
        return lines;
    }

    private static int partA(List<string> lines) 
    {
        var grid = new RopeGrid(1000, 1000, 2);
        foreach (var line in lines)
        {
            var splitLine = line.Split(" ");
            Console.WriteLine(line);
            grid.moveRope(splitLine[0], int.Parse(splitLine[1]));
        }
        return grid.tailCount();
    }

    private static int partB(List<string> lines) 
    {
        return 0;
    }
}
