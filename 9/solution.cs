using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using System.Text.RegularExpressions;


public class RopeGrid {
    public char[,] grid;

    public (int x, int y) tailIndex;

    public (int x, int y) headIndex;

    public RopeGrid(int x, int y) {
        grid = new char[y,x];
        for (var i = 0; i < x; i++)
        {
            for (var j = 0; j < y; j++)
            {
                grid[j,i] = '.';
            }
        }
        tailIndex = (x: x / 2, y: y / 2);
        headIndex = (x: x / 2, y: y / 2);
        grid[tailIndex.y,tailIndex.x] = '#';
    }

    public void moveRope(string direction, int count)
    {
        for (var i = 0; i < count; i++)
        {
            switch (direction)
            {
                case "R":
                    headIndex.x += 1;
                    break;
                case "L":
                    headIndex.x -= 1;
                    break;
                case "U":
                    headIndex.y += 1;
                    break;
                case "D":
                    headIndex.y -= 1;
                    break;
            }
            if (Math.Abs(headIndex.x - tailIndex.x) > 1 || Math.Abs(headIndex.y - tailIndex.y) > 1) {
                if ((headIndex.x - tailIndex.x) > 0) {
                    tailIndex.x += 1;
                } else if ((tailIndex.x - headIndex.x) > 0) {
                    tailIndex.x -= 1;
                }
                if ((headIndex.y - tailIndex.y) > 0) {
                    tailIndex.y += 1;
                } else if ((tailIndex.y - headIndex.y) > 0) {
                    tailIndex.y -= 1;
                }
                grid[tailIndex.y, tailIndex.x] = '#';
            }
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
                if (x == headIndex.x && y == headIndex.y && !printTailStops)
                {
                    Console.Write("H");
                }
                else if (x == tailIndex.x && y == tailIndex.y && !printTailStops)
                {
                    Console.Write("T");
                }
                else if (!printTailStops)
                {
                    Console.Write(".");
                }
                else {
                    Console.Write(grid[y,x]);
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
        var grid = new RopeGrid(1000, 1000);
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
