using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Numerics;


public class Monkey {
    public List<BigInteger> items;

    private string operatorString;
    private int operand;

    public int divisor;

    private int monkeyTrue;

    private int monkeyFalse;

    public BigInteger inspectCount;


    public Monkey(List<string> lines) {
        inspectCount = 0;

        var numRgx = new Regex(@"\d+");

        var matches = numRgx.Matches(lines[1]);
        items = new List<BigInteger>();
        foreach (var match in matches)
        {
            items.Add(int.Parse($"{match}"));
        }
        
        var sqRgx = new Regex(@"old \* old");
        if (sqRgx.IsMatch(lines[2])) {
            operatorString = "^";
        } else {
            matches = numRgx.Matches(lines[2]);
            operand = int.Parse($"{matches[0]}");
            var opRgx = new Regex(@"\*");
            operatorString = opRgx.IsMatch(lines[2]) ? "*" : "+";
        }

        matches = numRgx.Matches(lines[3]);
        divisor = int.Parse($"{matches[0]}");

        matches = numRgx.Matches(lines[4]);
        monkeyTrue = int.Parse($"{matches[0]}");

        matches = numRgx.Matches(lines[5]);
        monkeyFalse = int.Parse($"{matches[0]}");
    }

    public (int newMonkey, BigInteger newItem) throwItem(int bigDivisor, bool divideBy3)
    {
        inspectCount += 1;

        var item = items[0];
        items.RemoveAt(0);

        if (operatorString == "*") {
            item = item * operand;
        }
        else if (operatorString == "^") {
            item = item * item;
        }
        else {
            item = item + operand;
        }

        if (divideBy3) {
            item = item / 3;
        } else {
            item = item % bigDivisor;
        }

        if (item % divisor == 0) {
            return (newMonkey: monkeyTrue, newItem: item);
        } else {
            return (newMonkey: monkeyFalse, newItem: item);
        }
    }
}


public class DayEleven {
    public static void Main() 
    {
        List<string> lines = parseFile("input.txt");

        var partAResult = partA(lines);
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

    private static BigInteger solution(List<string> lines, int rounds, bool divideBy3)
    {
        var monkeys = new List<Monkey>();
        var divisor = 1;
        for (var i = 0; i < lines.Count; i += 7) {
            var monkey = new Monkey(lines.GetRange(i, 6));
            divisor = divisor * monkey.divisor;
            monkeys.Add(monkey);
        }

        for (var i = 0; i < rounds; i++)
        {
            foreach (var monkey in monkeys)
            {
                while (monkey.items.Count > 0){
                    var (newMonkey, newItem) = monkey.throwItem(divisor, divideBy3);
                    monkeys[newMonkey].items.Add(newItem);
                }
            }
        }


        var counts = new List<BigInteger>();
        foreach (var monkey in monkeys)
        {
            counts.Add(monkey.inspectCount);
        }

        counts.Sort();
        
        return counts[counts.Count - 1] * counts[counts.Count - 2];
    }

    private static BigInteger partA(List<string> lines) 
    {
        return solution(lines, 20, true);
    }

    private static BigInteger partB(List<string> lines) 
    {
        return solution(lines, 10000, false);
    }
}
