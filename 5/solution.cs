using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using System.Text.RegularExpressions;


public class DayFive {
    public static void Main() 
    {
        List<string> lines = parseFile("input.txt");

        string partAResult = partA(lines);
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

    private static string partA(List<string> lines) 
    {
        // get the list
        int index = 0;
        string line = lines[0];
        int pileNum = (line.Length + 1) / 4;
        List<char>[] pileList = new List<char>[pileNum];
        // init
        for (var i = 0; i < pileList.Count(); i += 1)
        {
            pileList[i] = new List<char>();
        }
        Regex rgx = new Regex(@"\d+");
        while (!rgx.IsMatch(line = lines[index]))
        {
            line = lines[index];
            for (var i = 0; i < pileNum; i += 1) {
                char value = line[((i + 1) * 4) - 3];
                if (value != ' ') {
                    pileList[i].Insert(0, value);
                }
            }
            index += 1;
        }
        index += 2;

        Console.WriteLine(lines[index]);

        while (index < lines.Count()) {
            line = lines[index];
            MatchCollection matches = rgx.Matches(line);
            Console.WriteLine(matches[0]);
            int count = int.Parse($"{matches[0]}");
            int fromIndex = int.Parse($"{matches[1]}") - 1;
            int toIndex = int.Parse($"{matches[2]}") - 1;
            int elementIndex = pileList[fromIndex].Count() - count;

            for (var i = 0; i < count; i += 1)
            {
                char element = pileList[fromIndex].ElementAt(elementIndex);
                pileList[fromIndex].RemoveAt(elementIndex);
                pileList[toIndex].Add(element);
            }

            index += 1;
        }


        string result = "";

        foreach (var pile in pileList)
        {
            result += pile.Last();
        }

        return result;
    }

    private static int partB(List<string> lines) 
    {
        int scoreSum = 0;
        return scoreSum;
    }
}
