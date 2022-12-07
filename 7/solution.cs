using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using System.Text.RegularExpressions;


public class FileStructure {
    public List<FileStructure> subFiles;

    public FileStructure parentDirectory;
    public string name;
    public bool isDirectory;

    private int size;

    public FileStructure(string nameInput) {
        name = nameInput;
        isDirectory = true;
        subFiles = new List<FileStructure>();
    }

    public FileStructure(string nameInput, FileStructure parent) {
        name = nameInput;
        isDirectory = true;
        subFiles = new List<FileStructure>();
        parentDirectory = parent;
    }

    public FileStructure(string nameInput, int sizeInput) {
        name = nameInput;
        size = sizeInput;
        isDirectory = false;
    }

    public void addSubFile(FileStructure subFile) {
        subFiles.Add(subFile);
    }

    public FileStructure findSubFile(string name) {
        foreach (var subFile in subFiles) {
            if (subFile.name == name)
            {
                return subFile;
            }
        }
        return null;
    }

    public int getSize()
    {
        if (isDirectory == false) {
            return size;
        }
        int sum = 0;
        foreach (var subFile in subFiles) {
            sum += subFile.getSize();
        }
        return sum;
    } 
}


public class DaySeven {
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
        var fileStructure = new FileStructure("/");
        var directories = new List<FileStructure>();
        for (var index = 2; index < lines.Count(); index += 1)
        {
            var line = lines[index];
            var splitLine = line.Split(" ");
            if (splitLine[1] == "ls")
            {
                // skip
            }
            else if (splitLine[1] == "cd")
            {
                if (splitLine[2] == "..")
                {
                    fileStructure = fileStructure.parentDirectory;
                }
                else
                {
                    fileStructure = fileStructure.findSubFile(splitLine[2]);
                }
            }
            else if (splitLine[0] == "dir")
            {
                var newFile = new FileStructure(splitLine[1], fileStructure);
                fileStructure.addSubFile(newFile);
                directories.Add(newFile);
            }
            else
            {
                var newFile = new FileStructure(splitLine[1], int.Parse(splitLine[0]));
                fileStructure.addSubFile(newFile);
            }
        }

        var resultSum = 0;
        foreach (var directory in directories) {
            var size = directory.getSize();
            if (size < 100000) {
                resultSum += size;
            }
        }


        return resultSum;
    }

    private static int partB(List<string> lines) 
    {
        var fileStructure = new FileStructure("/");
        var topLevel = fileStructure;
        var directories = new List<FileStructure>();
        for (var index = 2; index < lines.Count(); index += 1)
        {
            var line = lines[index];
            var splitLine = line.Split(" ");
            if (splitLine[1] == "ls")
            {
                // skip
            }
            else if (splitLine[1] == "cd")
            {
                if (splitLine[2] == "..")
                {
                    fileStructure = fileStructure.parentDirectory;
                }
                else
                {
                    fileStructure = fileStructure.findSubFile(splitLine[2]);
                }
            }
            else if (splitLine[0] == "dir")
            {
                var newFile = new FileStructure(splitLine[1], fileStructure);
                fileStructure.addSubFile(newFile);
                directories.Add(newFile);
            }
            else
            {
                var newFile = new FileStructure(splitLine[1], int.Parse(splitLine[0]));
                fileStructure.addSubFile(newFile);
            }
        }

        var unusedDiskspace = 70000000 - topLevel.getSize();
        var needToDelete = 30000000 - unusedDiskspace;
        var smallestOption = 70000000;

        foreach (var directory in directories) {
            var size = directory.getSize();
            if (size > needToDelete && size < smallestOption) {
                smallestOption = size;
            }
        }


        return smallestOption;
    }
}
