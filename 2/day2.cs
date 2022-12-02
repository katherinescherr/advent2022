using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;



public class DayTwo {

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
        Dictionary<string, int> score = new Dictionary<string, int>
        {
            {"A X", 1 + 3},
            {"A Y", 2 + 6},
            {"A Z", 3 + 0},
            {"B X", 1 + 0},
            {"B Y", 2 + 3},
            {"B Z", 3 + 6},
            {"C X", 1 + 6},
            {"C Y", 2 + 0},
            {"C Z", 3 + 3}
        };
        int scoreSum = 0;
        foreach (var line in lines)
        {
            scoreSum += score[line];
        }
        return scoreSum;
    }

    private static int partB(List<string> lines) 
    {
        Dictionary<string, int> score = new Dictionary<string, int>
        {
            {"A X", 3 + 0},
            {"A Y", 1 + 3},
            {"A Z", 2 + 6},
            {"B X", 1 + 0},
            {"B Y", 2 + 3},
            {"B Z", 3 + 6},
            {"C X", 2 + 0},
            {"C Y", 3 + 3},
            {"C Z", 1 + 6}
        };
        int scoreSum = 0;
        foreach (var line in lines)
        {
            scoreSum += score[line];
        }
        return scoreSum;
    }
}
