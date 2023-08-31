using System;
using System.IO;
using System.Threading;

internal class Program
{
    private static void Main(string[] args)
    {
        Console.CursorVisible = false;

        char[,] map = ReadMap("map.txt");
        ConsoleKeyInfo pressKey = new ConsoleKeyInfo('w', ConsoleKey.W, false, false, false);

        // Поток для беспрерывного передвижения
        Task.Run(() =>
        {
            while (true)
            {
                pressKey = Console.ReadKey();
            };
        });

        int pacmanX = 1;
        int pacmanY = 1;
        int score = 0;

        while (true)
        {
            Console.Clear();
            // Обработка шага
            KeyInput(pressKey, ref pacmanX, ref pacmanY, map, ref score);
            // Отрисовка карты
            Console.ForegroundColor = ConsoleColor.Cyan;
            PrintMap(map);
            // Отрисовка pacman 
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.SetCursorPosition(pacmanX, pacmanY);
            Console.Write("@");
            // Отрисовка очков
            Console.ForegroundColor = ConsoleColor.Green;
            Console.SetCursorPosition(29, 0);
            Console.Write($"Score: {score}");

            Thread.Sleep(1000);
        };
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

    private static void KeyInput(ConsoleKeyInfo pressKey, ref int pacmanX, ref int pacmanY, char[,] map, ref int score)
    {
        int[] direction = GetDirection(pressKey);

        int nextPosX = pacmanX + direction[0];
        int nextPosY = pacmanY + direction[1];

        char nextCell = map[nextPosX, nextPosY];

        if (nextCell == ' ' || nextCell == '.')
        {
            pacmanX = nextPosX;
            pacmanY = nextPosY;

            if (nextCell == '.')
            {
                score++;
                map[nextPosX, nextPosY] = ' ';
            }
        }
    }

    private static int[] GetDirection(ConsoleKeyInfo pressKey)
    {
        int[] direction = { 0, 0 };

        if (pressKey.Key == ConsoleKey.UpArrow)
            direction[1] = -1;
        else if (pressKey.Key == ConsoleKey.DownArrow)
            direction[1] = 1;
        else if (pressKey.Key == ConsoleKey.RightArrow)
            direction[0] = 1;
        else if (pressKey.Key == ConsoleKey.LeftArrow)
            direction[0] = -1;

        return direction;
    }

    private static int GetMaxLength(string[] lines)
    {
        int maxLength = lines[0].Length;
        foreach (var line in lines)
        {
            if (line.Length > maxLength)
                maxLength = line.Length;
        }

        return maxLength;
    }
}