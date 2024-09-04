using System;
using System.Collections.Generic;
using System.Linq;

namespace _7._04.TopServera
{
    internal class Program
    {
        static void Main(string[] args)
        {
            PlayerCreator creator = new PlayerCreator();
            PlayerDatabase database = new PlayerDatabase(creator);

            database.Work();
        }
    }

    class PlayerDatabase
    {
        private List<Player> _players;

        public PlayerDatabase(PlayerCreator creator)
        {
            _players = creator.CreatePlayers();
        }

        public void Work()
        {
            const string CommandExit = "0";
            const string CommandShowTopPlayers = "1";

            bool isExit = false;

            while (isExit == false)
            {
                int topSize = 3;
                int cursorPositionX = 0;
                int cursorPositionY = 0;

                Console.Clear();
                Console.WriteLine("БАЗА ДАННЫХ ИГРОКОВ");
                Console.WriteLine($"\nДоступные команды:" +
                    $"\n{CommandExit}. Выход из программы" +
                    $"\n{CommandShowTopPlayers}. Показать Топ - {topSize} игроков сервера");
                Console.WriteLine();
                Console.Write("Введите номер необходимой комманды: ");
                cursorPositionX = Console.CursorLeft;
                cursorPositionY = Console.CursorTop;
                Console.WriteLine("\n");
                ShowPlayers(_players);
                Console.SetCursorPosition(0, 0);
                Console.SetCursorPosition(cursorPositionX, cursorPositionY);

                switch (Console.ReadLine())
                {
                    case CommandExit:
                        isExit = true;
                        break;

                    case CommandShowTopPlayers:
                        ShowTopPlayers(topSize);
                        break;

                    default:
                        Console.Clear();
                        Console.WriteLine("Некорректный запрос!");
                        Console.ReadKey();
                        break;
                }
            }
        }

        private void ShowTopPlayers(int topSize)
        {
            const string CommandShowTopByLevel = "1";
            const string CommandShowTopByPower = "2";

            List<Player> players = new List<Player>();

            Console.Clear();
            Console.WriteLine($"{CommandShowTopByLevel}. Показать по уровню" +
                $"\n{CommandShowTopByPower}. Показать по силе");
            Console.WriteLine();
            Console.Write("Ввод: ");

            switch (Console.ReadLine())
            {
                case CommandShowTopByLevel:
                    players = _players.OrderByDescending(player => player.Level).ToList();
                    break;

                case CommandShowTopByPower:
                    players = _players.OrderByDescending(player => player.Power).ToList();
                    break;
            }

            if (players.Count >= topSize)
            {
                Console.Clear();
                ShowPlayers(players.Take(topSize).ToList());
                Console.ReadKey();
            }
            else
            {
                Console.Clear();
                Console.WriteLine("Недостаточно игроков!");
                Console.ReadKey();
            }
        }

        private void ShowPlayers(List<Player> players)
        {
            foreach (Player player in players)
            {
                player.ShowInfo();
                Console.WriteLine();
            }
        }
    }

    class Player
    {
        public Player(string nickname, int level, int power)
        {
            Nickname = nickname;
            Level = level;
            Power = power;
        }

        public string Nickname { get; private set; }
        public int Level { get; private set; }
        public int Power { get; private set; }

        public void ShowInfo()
        {
            Console.Write($"{Nickname} | {Level} lvl | {Power} pwr");
        }
    }

    class PlayerCreator
    {
        private NicknameCreator _nicknameCreator;

        public PlayerCreator()
        {
            _nicknameCreator = new NicknameCreator();
        }

        public List<Player> CreatePlayers(int numberPlayers = 100)
        {
            List<Player> players = new List<Player>();

            for (int i = 0; i < numberPlayers; i++)
            {
                Player player = CreatePlayer(players);

                players.Add(player);
            }

            return players;
        }

        private Player CreatePlayer(List<Player> players)
        {
            string nickname = CreateUniqueNickname(players);
            int level = CreateLevel();
            int power = CreatePower();

            return new Player(nickname, level, power);
        }

        private string CreateUniqueNickname(List<Player> players)
        {
            bool hasNicknameCreated = false;
            string nickname = _nicknameCreator.CreateNickname();

            while (hasNicknameCreated == false)
            {
                if (players.All(player => player.Nickname != nickname))
                {
                    hasNicknameCreated = true;
                }
                else
                {
                    nickname = _nicknameCreator.ChangeNickname(nickname);
                }
            }

            return nickname;
        }

        private int CreateLevel()
        {
            int levelMin = 1;
            int levelMax = 101;

            return UserUtils.GenereteRandom(levelMin, levelMax);
        }

        private int CreatePower()
        {
            int powerMin = 1;
            int powerMax = 10000;

            return UserUtils.GenereteRandom(powerMin, powerMax);
        }
    }

    class NicknameCreator
    {
        public string CreateNickname()
        {
            string nickname = null;

            if (UserUtils.GenereteRandomBool())
            {
                nickname = GenerateAdjective() + CreateFoundation();
            }
            else
            {
                nickname = CreateFoundation();
            }

            return nickname;
        }

        public string ChangeNickname(string nickname)
        {
            int numberMax = 9;
            int number = UserUtils.GenereteRandom(numberMax + 1);
            string newNickname = nickname + number;

            return newNickname;
        }

        private string CreateFoundation()
        {
            string foundation = null;
            int numberNounsMax = 3;
            int numberNouns = UserUtils.GenereteRandom(1, numberNounsMax + 1);

            for (int i = 0; i < numberNouns; i++)
            {
                foundation += GenerateNoun();
            }

            return foundation;
        }

        private string GenerateNoun()
        {
            List<string> surnames = new List<string>
            {
                "Лауреат",
                "Ксенофоб",
                "Диван",
                "Информатик",
                "Маслина",
                "Машиностроитель",
                "Шансонье",
                "Портняжка",
                "Пахарь",
                "Сенбернар",
                "Арка",
                "Дуралей",
                "Закурка",
                "Канва",
                "Брасс",
                "Газоубежище",
                "Земляк",
                "Расстриг",
                "Бредень",
                "Герой",
                "Рыбовоз",
                "Социал-демократ",
                "Выкрик",
                "Жабо",
                "Хала",
                "Смоковница",
                "Автоген",
                "Тротуар",
                "Аист",
                "Козлище"
            };

            return surnames[UserUtils.GenereteRandom(surnames.Count)];
        }

        private string GenerateAdjective()
        {
            List<string> patronymics = new List<string>
            {
                "Противоположный",
                "Жертвенник",
                "Квашеный",
                "Изнуренный",
                "Сальвадорский",
                "Благодатный",
                "Беспредельный",
                "Серьезный",
                "Затхлый",
                "Глубоководный",
                "Неразличимый",
                "Коммуникабельный",
                "Удовлетворительный",
                "Побочный",
                "Вислоухий",
                "Эрудированный",
                "Пробивной",
                "Четырехразовый",
                "Нечаянный",
                "Однотипный",
                "Поштучный",
                "Ненасытный",
                "Шикарный",
                "Червонный",
                "Раковый",
                "Завистливый",
                "Эстонский",
                "Инистый",
                "культурный",
                "Засухоустойчивый",
            };

            return patronymics[UserUtils.GenereteRandom(patronymics.Count)];
        }
    }

    class UserUtils
    {
        private static Random s_random;

        static UserUtils()
        {
            s_random = new Random();
        }

        public static int GenereteRandom(int minValue, int maxValue)
        {
            int randomNumber = s_random.Next(minValue, maxValue);
            return randomNumber;
        }

        public static int GenereteRandom(int maxValue)
        {
            return s_random.Next(maxValue);
        }

        public static bool GenereteRandomBool()
        {
            List<bool> bools = new List<bool>
            {
                false,
                true
            };

            return bools[GenereteRandom(bools.Count)];
        }
    }
}
