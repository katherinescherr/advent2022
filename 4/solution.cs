using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;


public class ElfRange {
    public int lowerBound;
    public int upperBound;

    public ElfRange(string input)
    {
        string[] split = input.Split("-");
        lowerBound = int.Parse(split[0]);
        upperBound = int.Parse(split[1]);
    }
}

public class DayFour {
    public static void Main() 
    {
        List<ElfRange[]> ranges = parseFile("input.txt");

        int partAResult = partA(ranges);
        Console.WriteLine($"Part A: {partAResult}");

        int partBResult = partB(ranges);
        Console.WriteLine($"Part B: {partBResult}");
    }


    private static List<ElfRange[]> parseFile(string filename)
    {
        StreamReader reader = File.OpenText(filename);
        string line;
        List<ElfRange[]> ranges = new List<ElfRange[]>();
        while ((line = reader.ReadLine()) != null) 
        {
            string[] split = line.Split(",");
            ElfRange[] range = {new ElfRange(split[0]), new ElfRange(split[1])};
            ranges.Add(range);
        }
        return ranges;
    }

    private static int partA(List<ElfRange[]> ranges) 
    {
        int scoreSum = 0;
        foreach (var rangePair in ranges)
        {
            if
            (
                (
                    rangePair[0].lowerBound >= rangePair[1].lowerBound && 
                    rangePair[0].upperBound <= rangePair[1].upperBound
                ) ||
                 (
                    rangePair[1].lowerBound >= rangePair[0].lowerBound && 
                    rangePair[1].upperBound <= rangePair[0].upperBound
                )
            )
            {
                scoreSum += 1;
            }
        }
        return scoreSum;
    }

    private static int partB(List<ElfRange[]> ranges) 
    {
        int scoreSum = 0;
        foreach (var rangePair in ranges)
        {
            if
            (
                (
                    rangePair[0].lowerBound <= rangePair[1].lowerBound && 
                    rangePair[0].upperBound >= rangePair[1].lowerBound
                ) ||
                 (
                    rangePair[1].lowerBound <= rangePair[0].lowerBound && 
                    rangePair[1].upperBound >= rangePair[0].lowerBound
                )
            )
            {
                scoreSum += 1;
            }
        }
        return scoreSum;
    }
}
