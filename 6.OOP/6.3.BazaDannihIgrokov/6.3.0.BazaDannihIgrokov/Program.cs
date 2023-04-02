using System;
using System.Collections.Generic;

namespace _6._3.BazaDannihIgrokov
{
    internal class Program
    {
        static void Main(string[] args)
        {
            const string CommandExit = "0";
            const string CommandAddPlayer = "1";
            const string CommandBanPlayer = "2";
            const string CommandUnbanPlayer = "3";
            const string CommandDeletePlayer = "4";

            BaseDataPlayers baseDataPlayers = new BaseDataPlayers();
            bool isWorker = true;

            while (isWorker == true)
            {
                string userInput;

                baseDataPlayers.ShowPlayers();
                Console.WriteLine();
                Console.Write($"Доступные команды:\n\n" +
                    $"{CommandExit}. Выйход из базы данных.\n" +
                    $"{CommandAddPlayer}. Добавить нового игрока.\n" +
                    $"{CommandBanPlayer}. Забанить игрока.\n" +
                    $"{CommandUnbanPlayer}. Разбанить игрока.\n" +
                    $"{CommandDeletePlayer}. Удалить игрока.\n\n" +
                    $"Ввод: ");

                switch (userInput = Console.ReadLine())
                {
                    case CommandExit:
                        isWorker = false;
                        break;

                    case CommandAddPlayer:
                        baseDataPlayers.AddPlayer();
                        break;

                    case CommandBanPlayer:
                        baseDataPlayers.BanPlayer();
                        break;

                    case CommandUnbanPlayer:
                        baseDataPlayers.UnbanPlayer();
                        break;

                    case CommandDeletePlayer:
                        baseDataPlayers.DeletePlayer();
                        break;

                    default:
                        Console.WriteLine("Некорректный запрос!");
                        Console.ReadKey();
                        break;
                }

                Console.Clear();
            }
        }
    }

    class BaseDataPlayers
    {
        private List<Player> _players = new List<Player>();
        private int _levelMax;
        private int _nicknameLenghtMax;
        private int _identifierLenght;

        public BaseDataPlayers()
        {
            _levelMax = 100;
            _nicknameLenghtMax = 15;
            _identifierLenght = 8;
        }

        public void ShowPlayers()
        {
            string cellIdentifierName = "Идентификатор";
            int cellIdentifierLenght = GetLenghtCell(cellIdentifierName, _identifierLenght);
            string cellNicknameName = "Никнейм";
            int cellNicknameLenght = GetLenghtCell(cellNicknameName, _nicknameLenghtMax);
            string cellLevelName = "Уровень";
            int cellLevelLenght = GetLenghtCell(cellLevelName, Convert.ToString(_levelMax).Length);
            string cellBanName = "Доступ";
            string cellBanValue = "бан";
            int cellBanLenght = GetLenghtCell(cellBanName, cellBanValue.Length);

            Console.WriteLine("База данных игроков:\n");
            OutputCell(cellIdentifierLenght, cellIdentifierName);
            OutputCell(cellNicknameLenght, cellNicknameName);
            OutputCell(cellLevelLenght, cellLevelName);
            OutputCell(cellBanLenght, cellBanName);
            Console.WriteLine();

            foreach (var player in _players)
            {
                OutputCell(cellIdentifierLenght, player.Identifier);
                OutputCell(cellNicknameLenght, player.Nickname);
                OutputCell(cellLevelLenght, Convert.ToString(player.Level));

                if (player.IsBanned == true)
                {
                    OutputCell(cellBanLenght, cellBanValue);
                }
                else
                {
                    OutputCell(cellBanLenght);
                }

                Console.WriteLine();
            }
        }

        public void AddPlayer()
        {
            string identifier = GenerateIdentifier();
            string nickname = GetNicknameFromUser();
            int level = GetLevelFromUser();
            Player player = new Player(identifier, nickname, level);

            _players.Add(player);
        }

        public void BanPlayer()
        {
            string userInput;

            Console.Write("Введите идентификатор игрока, чтобы выдать ему бан: ");
            userInput = Console.ReadLine();

            foreach (var player in _players)
            {
                if (player.Identifier == userInput)
                {
                    player.IsBanned = true;
                }
            }
        }

        public void UnbanPlayer()
        {
            string userInput;

            Console.Write("Введите идентификатор игрока, чтобы снять с него бан: ");
            userInput = Console.ReadLine();

            foreach (var player in _players)
            {
                if (player.Identifier == userInput)
                {
                    player.IsBanned = false;
                }
            }
        }

        public void DeletePlayer()
        {
            string userInput;
            int indexPlayer = 0;
            bool hasPlayerFound = false;

            Console.Write("Введите идентификатор игрока, чтобы удалить данные о нём: ");
            userInput = Console.ReadLine();

            foreach (var player in _players)
            {
                if (player.Identifier == userInput)
                {
                    indexPlayer = _players.IndexOf(player);
                    hasPlayerFound = true;
                }
            }

            if (hasPlayerFound == true)
            {
                _players.RemoveAt(indexPlayer);
            }
        }

        private string GenerateIdentifier()
        {
            Random random = new Random();
            string newIdentifier = null;
            bool isIdentifierUnique = false;

            while (isIdentifierUnique == false)
            {
                for (int i = 0; i < _identifierLenght; i++)
                {
                    int digitOfIdentifierMin = 0;
                    int digitOfIdentifierMax = 10;
                    string digitOfIdentifier = Convert.ToString(random.Next(digitOfIdentifierMin, digitOfIdentifierMax));

                    newIdentifier += digitOfIdentifier;
                }

                if (_players.Count > 0)
                {
                    foreach (var player in _players)
                    {
                        if (player.Identifier == newIdentifier)
                        {
                            newIdentifier = null;
                        }
                        else
                        {
                            isIdentifierUnique = true;
                        }
                    }
                }
                else
                {
                    isIdentifierUnique = true;
                }
            }

            return newIdentifier;
        }

        private string GetNicknameFromUser()
        {
            string nickname = null;
            bool isNicknameEnteredCorrectly = false;

            while (isNicknameEnteredCorrectly == false)
            {
                char simbolWhiteSpace = ' ';
                int countSimbolWhiteSpaceFromNickname = 0;

                Console.Clear();
                Console.Write("Введите никнейм игрока: ");
                nickname = Console.ReadLine();

                if (nickname.Length <= _nicknameLenghtMax)
                {
                    for (int i = 0; i < nickname.Length; i++)
                    {
                        if (nickname[i] == simbolWhiteSpace)
                        {
                            countSimbolWhiteSpaceFromNickname++;
                        }
                    }

                    if (countSimbolWhiteSpaceFromNickname != nickname.Length)
                    {
                        isNicknameEnteredCorrectly = true;
                    }
                    else
                    {
                        Console.WriteLine("Никнейм не должен содержать одни лишь пробелы," +
                            " а так же быть пустым!");
                        Console.ReadKey();
                    }
                    
                }
                else
                {
                    Console.WriteLine("В никнеймае не должно быть более " 
                        + _nicknameLenghtMax + "символов!");
                    Console.ReadKey();
                }
            }

            return nickname;
        }

        private int GetLevelFromUser()
        {
            int level = 0;
            bool isLevelEnteredCorrectly = false;

            while (isLevelEnteredCorrectly == false)
            {
                bool isNumber;
                string userInput;

                Console.Clear();
                Console.Write("Введите уровень игрока: ");
                userInput = Console.ReadLine();
                isNumber = int.TryParse(userInput, out level);

                if (isNumber == true && level >= 0 && level <= _levelMax)
                {
                    isLevelEnteredCorrectly = true;
                }
                else
                {
                    Console.WriteLine("Уровень не должен быть отрицательным числом" +
                        " или превышать максимальное значение,\n" +
                        "а так же содержать буквы и символы!\n" +
                        "Максимальный урвоень - " + _levelMax);
                    Console.ReadKey();
                }
            }

            return level;
        }

        private int GetLenghtCell(string nameCell, int maximumLengthData)
        {
            int lenghtCell = 0;

            if (nameCell.Length >= maximumLengthData)
            {
                lenghtCell = nameCell.Length;
            }
            else
            {
                lenghtCell = maximumLengthData;
            }

            return lenghtCell;
        }

        private void OutputCell(int cellLenght, string value = " ")
        {
            char cellSeparator = '|';
            int startingPositionCell = Console.CursorLeft;

            Console.Write(value);
            Console.SetCursorPosition(startingPositionCell + cellLenght, Console.CursorTop);
            Console.Write(cellSeparator);
        }
    }

    class Player
    {
        public string Identifier;
        public string Nickname;
        public int Level;
        public bool IsBanned;

        public Player(string identifier, string nickname, int level)
        {
            Identifier = identifier;
            Nickname = nickname;
            Level = level;
            IsBanned = false;
        }
    }
}
