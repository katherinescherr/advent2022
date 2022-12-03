using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;



public class DayThree {

    public static string alpha = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";

    public static void Main() 
    {
        List<string> lines = parseFile("input.txt");

        int partAResult = partA(lines);
        Console.WriteLine($"Part A: {partAResult}");
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
        int scoreSum = 0;
        foreach (var line in lines)
        {
            char[] items = line.ToCharArray();
            HashSet<char> set1 = new HashSet<char>(items.Take(items.Length / 2));
            HashSet<char> set2 = new HashSet<char>(items.TakeLast(items.Length / 2));
            set1.IntersectWith(set2);
            foreach (var x in set1)
            {
                scoreSum += alpha.IndexOf(x) + 1;
            }
            
        }
        return scoreSum;
    }
}
