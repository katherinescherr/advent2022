using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using System.Text.RegularExpressions;


public class DayTen {
    public static void Main() 
    {
        List<string> lines = parseFile("input.txt");

        int partAResult = partA(lines);
        Console.WriteLine($"Part A: {partAResult}");

        var partBResult = partB(lines);
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
        var signalArray = new int[lines.Count * 2];
        var currentStrength = 1;
        var marker = 0;
        foreach(var line in lines)
        {
            signalArray[marker] += currentStrength;
            currentStrength = signalArray[marker];
            var splitLine = line.Split(" ");
            if (splitLine[0] == "addx") {
                signalArray[marker + 1] = currentStrength;
                signalArray[marker + 2] += int.Parse(splitLine[1]);
                marker += 1;
            }
            marker += 1;
        }
        return (
            (signalArray[19] * 20) + 
            (signalArray[59] * 60) + 
            (signalArray[99] * 100) + 
            (signalArray[139] * 140) + 
            (signalArray[179] * 180) +
            (signalArray[219] * 220)
        );
    }

    private static string partB(List<string> lines) 
    {
        var signalArray = new int[240];
        var currentStrength = 1;
        var marker = 0;
        foreach(var line in lines)
        {
            signalArray[marker] += currentStrength;
            currentStrength = signalArray[marker];
            var splitLine = line.Split(" ");
            if (splitLine[0] == "addx") {
                signalArray[marker + 1] = currentStrength;
                signalArray[marker + 2] += int.Parse(splitLine[1]);
                marker += 1;
            }
            marker += 1;
        }
        var result = "";
        for (var i = 0; i < 240; i++)
        {
            var value = i % 40;
            if (value == 0) {
                result += '\n';
            }
            if (Math.Abs(signalArray[i] - value) <= 1) {
                result +=  "#";
            } else {
                result += ".";
            }
        }
        return result;
    }

}
