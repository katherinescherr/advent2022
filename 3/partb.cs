using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;



public class DayThreeB {

    public static string alpha = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";

    public static void Main() 
    {
        List<string> lines = parseFile("input.txt");

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

    private static int partB(List<string> lines) 
    {
        int scoreSum = 0;
        for (var i = 0; i < lines.Count; i += 3)
        {
            HashSet<char> set1 = new HashSet<char>(lines[i].ToCharArray());
            HashSet<char> set2 = new HashSet<char>(lines[i + 1].ToCharArray());
            HashSet<char> set3 = new HashSet<char>(lines[i + 2].ToCharArray());
            set1.IntersectWith(set2);
            set1.IntersectWith(set3);
            foreach (var x in set1)
            {
                scoreSum += alpha.IndexOf(x) + 1;
            }
        }
        return scoreSum;
    }
}
