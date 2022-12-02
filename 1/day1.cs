using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;


public class DayOne {
    public static void Main() 
    {
        List<string> lines = parseFile("day1input.txt");

        int partAResult = partA(lines);
        Console.WriteLine(partAResult);

        int partBResult = partB(lines);
        Console.WriteLine(partBResult);
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
        int currentElf = 0;
        int maxElf = 0;
        foreach (var line in lines)
        {
            if (line == "") {
                maxElf = Math.Max(currentElf, maxElf);
                currentElf = 0;
            } else {
                currentElf += int.Parse(line);
            }

        }
        maxElf = Math.Max(currentElf, maxElf);
        return maxElf;
    }

    private static int partB(List<string> lines) 
    {
        int currentElf = 0;
        List<int> allElves = new List<int>();
        foreach (var line in lines)
        {
            if (line == "") {
                allElves.Add(currentElf);
                currentElf = 0;
            } else {
                currentElf += int.Parse(line);
            }

        }
        allElves.Sort();
        allElves.Reverse();
        int top3 = allElves.GetRange(0, 3).Sum();
        return top3;
    }
}
