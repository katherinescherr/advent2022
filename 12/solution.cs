using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using System.Text.RegularExpressions;

public class Paths {

    const string alpha = "SabcdefghijklmnopqrstuvwxyzE.";

    private char currentValue;

    public int currentIndex;
    
    public char[,] path;

    public int pathLength;

    public bool complete;

    public (int x, int y) coords;

    public List<Paths> paths;

    public Paths(List<string> lines) {
        complete = false;
        paths = new List<Paths>();
        path = new char[lines.Count, lines.First().Length];
        coords = (x: 0, y: 0);

        for (var i = 0; i < lines.Count; i++)
        {
            var line = lines[i];
            for (var j = 0; j < line.Length; j++)
            {
                path[i,j] = line[j];
                if (line[j] == 'S') {
                    coords = (x: i, y: j);
                }
            }
        }

        
        pathLength = 0;
        currentValue = 'S';
        currentIndex = alpha.IndexOf(currentValue);
    }

    public Paths(List<string> lines, (int x, int y) startCoords) {
        complete = false;
        paths = new List<Paths>();
        path = new char[lines.Count, lines.First().Length];
        coords = (x: 0, y: 0);

        for (var i = 0; i < lines.Count; i++)
        {
            var line = lines[i];
            for (var j = 0; j < line.Length; j++)
            {
                path[i,j] = line[j];
            }
        }

        
        pathLength = 0;
        coords = startCoords;
        currentValue = path[startCoords.x, startCoords.y];
        currentIndex = alpha.IndexOf(currentValue);       
    }

    public Paths(Paths existingPath, (int x, int y) newCoords) {
        path = existingPath.path.Clone() as char[,];
        pathLength = existingPath.pathLength + 1;
        coords = newCoords;
        paths = existingPath.paths;

        currentValue = path[newCoords.x, newCoords.y];
        currentIndex = alpha.IndexOf(currentValue);

        if (currentValue == 'E') {
            complete = true;
            Console.WriteLine(pathLength);
        } else {
            complete = false;
            path[newCoords.x, newCoords.y] = '.';
        } 
    }

    public SortedList<int, Paths> findPaths() {
        var currentIndex = alpha.IndexOf(currentValue);
        var charDiff = 1;
        if (currentValue == 'y') {
            charDiff = 2;
        }
        if (currentValue == 'S') {
            charDiff = 2;
        }
        var newPaths = new SortedList<int, Paths>();
        
        // Console.WriteLine($"coords: {coords.x} x {coords.y}, value: {currentValue} {alpha[currentIndex + 1]}");
        // if (coords.x > 0) {
        //     Console.WriteLine($"compare 1: {path[coords.x - 1, coords.y]} {charOptions.Contains(path[coords.x - 1, coords.y])}");
        // }
        
        if (coords.x > 0 && alpha.IndexOf(path[coords.x - 1, coords.y]) - currentIndex <= charDiff) {
            newPaths.Add((alpha.IndexOf(path[coords.x - 1, coords.y]) - currentIndex) * 10 + 1, new Paths(this, (x: coords.x - 1, y: coords.y)));
        }

        // if (coords.y > 0) {
        //     Console.WriteLine($"compare 2: {path[coords.x, coords.y - 1]} {charOptions.Contains(path[coords.x, coords.y - 1])}");
        // }

        if (coords.y > 0 && alpha.IndexOf(path[coords.x, coords.y - 1]) - currentIndex <= charDiff) {
            newPaths.Add((alpha.IndexOf(path[coords.x, coords.y - 1]) - currentIndex) * 10 + 2, new Paths(this, (x: coords.x, y: coords.y - 1)));
        }

        // if (coords.x < path.GetLength(0) - 1) {
        //     Console.WriteLine($"compare 3: {path[coords.x + 1, coords.y]} {charOptions.Contains(path[coords.x + 1, coords.y])}");
        // }

        if (coords.x < path.GetLength(0) - 1 && alpha.IndexOf(path[coords.x + 1, coords.y]) - currentIndex <= charDiff) {
            newPaths.Add((alpha.IndexOf(path[coords.x + 1, coords.y]) - currentIndex) * 10 + 3, new Paths(this, (x: coords.x + 1, y: coords.y)));
        }

        // if (coords.y < path.GetLength(1) - 1) {
        //     Console.WriteLine($"compare 4: {path[coords.x, coords.y + 1]} {charOptions.Contains(path[coords.x, coords.y + 1])}");
        // }

        if (coords.y < path.GetLength(1) - 1 && alpha.IndexOf(path[coords.x, coords.y + 1]) - currentIndex <= charDiff) {
            newPaths.Add((alpha.IndexOf(path[coords.x, coords.y + 1]) - currentIndex) * 10 + 4, new Paths(this, (x: coords.x, y: coords.y + 1)));
        }

        return newPaths;
    }
}


public class DayTwelve {
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
        var paths = new SortedList<int, Paths>();
        var initPath = new Paths(lines);
        paths.Add(0, initPath);
        var pathGrid = new int[initPath.path.GetLength(0),initPath.path.GetLength(1)];

        for (var ii = 0; ii < pathGrid.GetLength(0); ii++) {
            for (var j = 0; j < pathGrid.GetLength(1); j++) {
                pathGrid[ii,j] = 1000000000;
            }
        }
        
        var minPath = 1000000000;
        var i = 0;

        while(paths.Count > 0)
        {
            var path = paths.First().Value;
            paths.RemoveAt(0);

            Console.WriteLine(paths.Count);

            var newPaths = path.findPaths();
            
            foreach (var newPath in newPaths)
            {
                i++;
                if (newPath.Value.complete) {
                    if (newPath.Value.pathLength < minPath) {
                        minPath = newPath.Value.pathLength;
                    }
                } else if (newPath.Value.pathLength < minPath && pathGrid[newPath.Value.coords.x, newPath.Value.coords.y] > newPath.Value.pathLength) {
                    pathGrid[newPath.Value.coords.x, newPath.Value.coords.y] = newPath.Value.pathLength;
                    paths.Add(newPath.Value.currentIndex * 100000 + newPath.Value.pathLength * 10000 + i, newPath.Value);
                } 
            }
        }

        return minPath;
    }

    private static int partB(List<string> lines) 
    {
        var initPath = new Paths(lines);
        var superMin = 1000000000;
        for (var ii = 0; ii < initPath.path.GetLength(0); ii++) {
            for (var j = 0; j < initPath.path.GetLength(1); j++) {
                if (initPath.path[ii,j] == 'a') {
                    var newMin = getMinPath(lines, (x: ii, y: j), superMin);
                    if (newMin < superMin) {
                        superMin = newMin;
                        Console.WriteLine(superMin);
                    } else {
                        Console.WriteLine($".{ii},{j}");
                    }
                }
            }
        }

        return superMin;
    }

    private static int getMinPath(List<string> lines, (int x, int y) coords, int superMin) {
        var paths = new SortedList<int, Paths>();
        var initPath = new Paths(lines, coords);
        paths.Add(0, initPath);
        var pathGrid = new int[initPath.path.GetLength(0),initPath.path.GetLength(1)];

        for (var ii = 0; ii < pathGrid.GetLength(0); ii++) {
            for (var j = 0; j < pathGrid.GetLength(1); j++) {
                pathGrid[ii,j] = 1000000000;
            }
        }
        
        var minPath = 1000000000;
        var i = 0;

        while(paths.Count > 0)
        {
            var path = paths.First().Value;
            paths.RemoveAt(0);

            var newPaths = path.findPaths();
            
            foreach (var newPath in newPaths)
            {
                i++;
                if (newPath.Value.complete) {
                    if (newPath.Value.pathLength < minPath) {
                        minPath = newPath.Value.pathLength;
                    }
                } else if (newPath.Value.pathLength < superMin && newPath.Value.pathLength < minPath && pathGrid[newPath.Value.coords.x, newPath.Value.coords.y] > newPath.Value.pathLength) {
                    pathGrid[newPath.Value.coords.x, newPath.Value.coords.y] = newPath.Value.pathLength;
                    paths.Add(newPath.Value.currentIndex * 100000 + newPath.Value.pathLength * 10000 + i, newPath.Value);
                } 
            }
        }

        return minPath;
    }
}
