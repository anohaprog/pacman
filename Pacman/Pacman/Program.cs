using System;
using System.IO;

internal class Program
{
    private static void Main(string[] args)
    {
        char[,] map = ReadMap("map.txt");
        PrintMap(map);
    }

    private static char[,] ReadMap(string path)
    {
        string[] file = File.ReadAllLines(path);
        char[,] map = new char[GetMaxLength(file), file.Length];
        for (int i = 0; i < map.GetLength(0); i++)
        {   
            for (int j = 0; j < map.GetLength(1); j++)
            {
                map[i, j] = file[j][i];
            }
        }
        return map;
    }

    private static void PrintMap(char[,] map)
    {
        for (int j = 0; j < map.GetLength(1); j++)
        {
            for (int i = 0; i < map.GetLength(0); i++)
            {
                Console.Write(map[i, j]);
            }
            Console.WriteLine();
        }
    }

    private static int GetMaxLength(string[] lines)
    {
        int maxLength = lines[0].Length;
        foreach(var line in lines)
        {
            if (line.Length > maxLength)
                maxLength = line.Length;
        }

        return maxLength;
    }
}