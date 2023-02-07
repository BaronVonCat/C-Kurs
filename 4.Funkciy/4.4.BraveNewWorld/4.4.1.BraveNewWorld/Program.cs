using System;
using System.IO;

namespace _4._4.BraveNewWorld
{
    internal class Program
    {
        static void Main(string[] args)
        {
            const char DesignationPlayer = 'O';
            const char DesignationFreeCell = ' ';
            const char DesignationExit = '!';

            bool hasLevelPassed = false;
            int playerPositionX = 0, playerPositionY = 0;
            char[,] map = CreateMap(ref playerPositionX, ref playerPositionY, DesignationPlayer);
            
            Console.CursorVisible = false;
            DrawMap(map);
            Console.SetCursorPosition(playerPositionX, playerPositionY);

            while (hasLevelPassed == false)
            {
                int playerMoveX = 0, playerMoveY = 0;

                if (Console.KeyAvailable)
                {
                    ConsoleKeyInfo key = Console.ReadKey(true);

                    switch (key.Key)
                    {
                        case ConsoleKey.UpArrow:
                            playerMoveY--;
                            break;

                        case ConsoleKey.DownArrow:
                            playerMoveY++;
                            break;

                        case ConsoleKey.LeftArrow:
                            playerMoveX--;
                            break;

                        case ConsoleKey.RightArrow:
                            playerMoveX++;
                            break;
                    }

                    if (map[playerPositionY + playerMoveY, playerPositionX + playerMoveX] == DesignationFreeCell)
                    {
                        map[playerPositionY, playerPositionX] = DesignationFreeCell;
                        Console.SetCursorPosition(playerPositionX, playerPositionY);
                        Console.Write(DesignationFreeCell);
                        playerPositionX += playerMoveX;
                        playerPositionY += playerMoveY;
                        map[playerPositionY, playerPositionX] = DesignationPlayer;
                        Console.SetCursorPosition(playerPositionX, playerPositionY);
                        Console.Write(DesignationPlayer);
                    }
                    else if (map[playerPositionY + playerMoveY, playerPositionX + playerMoveX] == DesignationExit)
                    {
                        hasLevelPassed = true;
                        Console.Clear();
                        Console.WriteLine("Вы прошли!!!");
                        Console.ReadKey();
                    }
                }
            }
        }

        static char[,] CreateMap(ref int playerPositionX, ref int playerPositionY, char designationPlayer)
        {
            const string CommandField = "1";
            const string NameFileMapField = "field";
            const string CommandLabyrinth = "2";
            const string NameFileMapLabyrinth = "labyrinth";

            bool isMapSelected = false;
            char[,] map = new char[0, 0];

            while (isMapSelected == false)
            {
                string requestedMap;

                Console.WriteLine($"Введите номер карты: " +
                $"\n{CommandField}. Поле" +
                $"\n{CommandLabyrinth}. Лабиринт");
                requestedMap = Console.ReadLine();
                Console.Clear();

                switch (requestedMap)
                {
                    case CommandField:
                        map = DownloadMap(NameFileMapField);
                        isMapSelected = true;
                        break;

                    case CommandLabyrinth:
                        map = DownloadMap(NameFileMapLabyrinth);
                        isMapSelected = true;
                        break;

                    default:
                        Console.WriteLine("Некорректынй запрос!");
                        break;
                }
            }

            GetCoordinatesPlayer(ref playerPositionX, ref playerPositionY, map, designationPlayer);
            return map;
        }

        static char[,] DownloadMap(string nameFileMap)
        {
            string[] fileMap = File.ReadAllLines($"Maps/{nameFileMap}.txt");
            char[,] map = new char[fileMap.Length, fileMap[0].Length];

            for (int i = 0; i < map.GetLength(0); i++)
            {
                for (int j = 0; j < map.GetLength(1); j++)
                {
                    map[i, j] = fileMap[i][j];
                }
            }

            return map;
        }

        static void GetCoordinatesPlayer(ref int playerPositionX, ref int playerPositionY, char[,] map, char designationPlayer)
        {
            for (int i = 0; i < map.GetLength(0); i++)
            {
                for (int j = 0; j < map.GetLength(1); j++)
                {
                    if (map[i, j] == designationPlayer)
                    {
                        playerPositionX = j;
                        playerPositionY = i;
                    }
                }
            }
        }

        static void DrawMap(char[,] map)
        {
            for (int i = 0; i < map.GetLength(0); i++)
            {
                for (int j = 0; j < map.GetLength(1); j++)
                {
                    Console.Write(map[i, j]);
                }

                Console.WriteLine();
            }
        }
    }
}
