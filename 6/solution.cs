using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using System.Text.RegularExpressions;


public class DaySix {
    public static void Main() 
    {
        List<string> lines = parseFile("input.txt");

        int partAResult = partA(lines[0]);
        Console.WriteLine($"Part A: {partAResult}");

        int partBResult = partB(lines[0]);
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

    private static int partA(string line) 
    {
        return getMarker(line, 4);
    }

    private static int partB(string line) 
    {
        return getMarker(line, 14);
    }

    private static int getMarker(string line, int charCount) {
        int marker;
        for (marker = charCount; marker <= line.Length; marker += 1) {
            HashSet<char> set = new HashSet<char>();
            for (var offset = 1; offset <= charCount; offset += 1) {
                set.Add(line[marker - offset]);
            }
            if (set.Count() == charCount) {
                break;
            }
        }
        return marker;
    }
}
