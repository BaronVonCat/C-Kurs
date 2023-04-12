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

            Database database = new Database();
            bool isWorker = true;

            while (isWorker == true)
            {
                string userInput;

                database.ShowPlayers();
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
                        database.AddPlayer();
                        break;

                    case CommandBanPlayer:
                        database.BanPlayer();
                        break;

                    case CommandUnbanPlayer:
                        database.UnbanPlayer();
                        break;

                    case CommandDeletePlayer:
                        database.DeletePlayer();
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

    class Database
    {
        private List<Player> _players = new List<Player>();

        public void ShowPlayers()
        {
            Console.WriteLine($"Идентификатор|Никнейм|Уровень|Доступ");
            Console.WriteLine();

            foreach ( var player in _players )
            {
                player.Show();
                Console.WriteLine();
            }
        }

        public void AddPlayer()
        {
            string identifier = GenerateIdentifier();
            string nickname = CreateNickname();
            int level = CreateLevel();
            Player player = new Player(identifier, nickname, level);

            _players.Add(player);
        }

        public void BanPlayer()
        {
            Player player;
            bool hasPlayerExist;
            string userInput;

            Console.Write("Чтобы выдать бан игроку, введите его идентификатор: ");
            userInput = Console.ReadLine();
            hasPlayerExist = TryGetPlayer(userInput, out player);

            if (hasPlayerExist == true)
            {
                Console.WriteLine($"Игрок, {player.Nickname}, забанен!");
                player.Ban();
            }
            else
            {
                Console.WriteLine("Игрок не найден!");
            }

            Console.ReadKey();
        }

        public void UnbanPlayer()
        {
            Player player;
            bool hasPlayerExistp;
            string userInput;

            Console.Write("Чтобы разбанить игрока, введите его идентификатор: ");
            userInput = Console.ReadLine();
            hasPlayerExistp = TryGetPlayer(userInput, out player);
            
            if (hasPlayerExistp == true)
            {
                Console.WriteLine($"Игрок, {player.Nickname}, разбанен!");
                player.Unban();
            }
            else
            {
                Console.WriteLine("Игрок не найден!");
            }

            Console.ReadKey();
        }

        public void DeletePlayer()
        {
            Player player;
            bool hasPlayerExistp;
            string userInput;

            Console.Write("Чтобы удалить игрока, введите его идентификатор: ");
            userInput = Console.ReadLine();
            hasPlayerExistp = TryGetPlayer(userInput, out player);

            if (hasPlayerExistp == true)
            {
                Console.WriteLine($"Игрок, {player.Nickname}, удалён!");
                _players.Remove(player);
            }
            else
            {
                Console.WriteLine("Игрок не найден!");
            }

            Console.ReadKey();
        }

        private bool TryGetPlayer(string identifier, out Player receivedPlayer)
        {
            bool isPlayerFound = false;

            receivedPlayer = null;

            foreach ( var player in _players)
            {
                if (player.Identifier == identifier)
                {
                    isPlayerFound = true;
                    receivedPlayer = player;
                }
            }

            return isPlayerFound;
        }

        private string GenerateIdentifier()
        {
            Random random = new Random();
            string newIdentifier = null;
            bool isIdentifierUnique = false;

            while (isIdentifierUnique == false)
            {
                int identifierLenght = 8;

                for (int i = 0; i < identifierLenght; i++)
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

        private string CreateNickname()
        {
            string nickname = null;
            bool isNicknameEnteredCorrectly = false;

            while (isNicknameEnteredCorrectly == false)
            {
                int nicknameLenghtMax = 15;
                char simbolWhiteSpace = ' ';
                int countWhiteSpace = 0;

                Console.Clear();
                Console.Write("Введите никнейм игрока: ");
                nickname = Console.ReadLine();

                if (nickname.Length <= nicknameLenghtMax)
                {
                    for (int i = 0; i < nickname.Length; i++)
                    {
                        if (nickname[i] == simbolWhiteSpace)
                        {
                            countWhiteSpace++;
                        }
                    }

                    if (countWhiteSpace != nickname.Length)
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
                        + nicknameLenghtMax + "символов!");
                    Console.ReadKey();
                }
            }

            return nickname;
        }

        private int CreateLevel()
        {
            int level = 0;
            bool isLevelEnteredCorrectly = false;

            while (isLevelEnteredCorrectly == false)
            {
                int levelMax = 100;
                bool isNumber;
                string userInput;

                Console.Clear();
                Console.Write("Введите уровень игрока: ");
                userInput = Console.ReadLine();
                isNumber = int.TryParse(userInput, out level);

                if (isNumber == true && level >= 0 && level <= levelMax)
                {
                    isLevelEnteredCorrectly = true;
                }
                else
                {
                    Console.WriteLine("Уровень не должен быть отрицательным числом" +
                        " или превышать максимальное значение,\n" +
                        "а так же содержать буквы и символы!\n" +
                        "Максимальный урвоень - " + levelMax);
                    Console.ReadKey();
                }
            }

            return level;
        }
    }

    class Player
    {
        public Player(string identifier, string nickname, int level)
        {
            Identifier = identifier;
            Nickname = nickname;
            Level = level;
            IsBanned = false;
        }

        public string Identifier { get; private set; }
        public string Nickname { get; private set; }
        public int Level { get; private set; }
        public bool IsBanned { get; private set; }

        public void Show()
        {
            string banValue = "бан";
            Console.Write($"{Identifier}|{Nickname}|{Level}|");

            if (IsBanned == true)
            {
                Console.Write(banValue);
            }
        }

        public void Ban()
        {
            IsBanned = true;
        }

        public void Unban()
        {
            IsBanned = false;
        }
    }
}
