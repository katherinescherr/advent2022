using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using System.Text.RegularExpressions;


public class DayEigtht {
    public static void Main() 
    {
        var lines = parseFile("input.txt");
        var grid = getGrid(lines);

        int partAResult = partA(grid);
        Console.WriteLine($"Part A: {partAResult}");

        int partBResult = partB(grid);
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

    private static int[,] getGrid(List<string> lines)
    {
        var grid = new int[lines.Count, lines.First().Length];

        for (var i = 0; i < lines.Count; i++)
        {
            var line = lines[i];
            for (var j = 0; j < line.Length; j++) {
                grid[i, j] = int.Parse($"{line[j]}");
            }
        }

        return grid;
    }

    private static int partA(int[,] grid) 
    {
        var countVisible = (grid.GetLength(0) + grid.GetLength(1) - 2) * 2;


        for (var i = 1; i < grid.GetLength(0) - 1; i++)
        {
            for (var j = 1; j < grid.GetLength(1) - 1; j++)
            {
                if (
                    isVisible(grid, i, -1, j, 0) ||
                    isVisible(grid, i, 1, j, 0) ||
                    isVisible(grid, i, 0, j, -1) ||
                    isVisible(grid, i, 0, j, 1)
                )
                {
                    countVisible++;
                }
            }
        }
        return countVisible;
    }
    
    private static bool isVisible(int[,] grid, int iInit, int iDiff, int jInit, int jDiff)
    {
        var i = iInit + iDiff;
        var j = jInit + jDiff;
        var isVisible = true;
        while(i >= 0 && i < grid.GetLength(0) && j >= 0 && j < grid.GetLength(1))
        {
            if (grid[iInit, jInit] <=  grid[i,j]) {
                isVisible = false;
                break;
            }
            i += iDiff;
            j += jDiff;
        }
        return isVisible;
    }


    private static int partB(int[,] grid) 
    {
        var maxScore = 0;

        for (var i = 1; i < grid.GetLength(0) - 1; i++)
        {
            for (var j = 1; j < grid.GetLength(1) - 1; j++)
            {
                var score = (
                    getScore(grid, i, -1, j, 0) *
                    getScore(grid, i, 1, j, 0) *
                    getScore(grid, i, 0, j, -1) *
                    getScore(grid, i, 0, j, 1)
                );
                if (score > maxScore)
                {
                    maxScore = score;
                }
            }
        }
        return maxScore;
    }


    private static int getScore(int[,] grid, int iInit, int iDiff, int jInit, int jDiff)
    {
        var i = iInit + iDiff;
        var j = jInit + jDiff;
        var score = 0;
        while(i >= 0 && i < grid.GetLength(0) && j >= 0 && j < grid.GetLength(1))
        {
            score += 1;
            if (grid[iInit, jInit] <=  grid[i,j]) {
                break;
            }
            i += iDiff;
            j += jDiff;
        }
        return score;        
    }
}
