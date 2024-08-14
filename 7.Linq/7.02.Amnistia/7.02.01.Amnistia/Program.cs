using System;
using System.Collections.Generic;
using System.Linq;

namespace _7._02.Amnistia
{
    internal class Program
    {
        static void Main(string[] args)
        {
            PrisonerCreator creator = new PrisonerCreator();
            PrisonersDatabase database = new PrisonersDatabase(creator);

            database.Work();
        }
    }

    class PrisonersDatabase
    {
        private List<string> _offenses;
        private List<Prisoner> _prisoners;

        public PrisonersDatabase(PrisonerCreator creator)
        {
            _offenses = creator.CreateOffenses();
            _prisoners = creator.CreatePrisoners();
        }

        public void Work()
        {
            const string CommandExit = "0";
            const string CommandTryAmnesty = "1";
            const string CommandShowAllPrisoners = "2";

            bool isExit = false;

            while (isExit == false)
            {
                Console.Clear();
                Console.WriteLine("БАЗА ДАННЫХ ПРЕСТУПНИКОВ");
                Console.WriteLine($"\nДоступные команды:" +
                    $"\n{CommandExit}. Выход из программы" +
                    $"\n{CommandTryAmnesty}. Амнистировать по статье" +
                    $"\n{CommandShowAllPrisoners}. Показать всех преступников");
                Console.WriteLine();
                Console.Write("Введите номер необходимой комманды: ");

                switch (Console.ReadLine())
                {
                    case CommandExit:
                        isExit = true;
                        break;

                    case CommandTryAmnesty:
                        TryAmnesty();
                        break;

                    case CommandShowAllPrisoners:
                        ShowAllPrisoners();
                        break;

                    default:
                        Console.Clear();
                        Console.WriteLine("Некорректный запрос!");
                        Console.ReadKey();
                        break;
                }
            }
        }

        private void TryAmnesty()
        {
            string offense = null;
            bool isChoiceMade = false;

            if (TryGetOffense(out offense))
            {
                if (_prisoners.Count(prisoner => prisoner.Offense == offense) > 0)
                {
                    while (isChoiceMade == false)
                    {
                        const int CommandCancel = 0;
                        const int CommandAmnesty = 1;

                        int userNumber = 0;

                        Console.Clear();
                        ShowPrisoners(_prisoners.Where(prisoner => prisoner.Offense == offense).ToList());
                        Console.WriteLine();
                        Console.WriteLine($"Доступные команды:" +
                            $"\n{CommandCancel}. Отмена" +
                            $"\n{CommandAmnesty}. Амнистировать");
                        Console.WriteLine();
                        Console.WriteLine($"Введите номер необходимой комманды:");

                        if (isChoiceMade = UserUtils.TryEnterNumberFromRange(CommandCancel, CommandAmnesty, out userNumber))
                        {
                            if (userNumber == CommandAmnesty)
                            {
                                AmnestyPrisoners(offense);
                            }
                        }
                        else
                        {
                            Console.Clear();
                            Console.WriteLine("Некорректный запрос!");
                            Console.ReadKey();
                        }
                    }
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine("Преступников по этой статье нет!");
                    Console.ReadKey();
                }
            }
        }

        private bool TryGetOffense(out string offense)
        {
            bool isOffenseFound = false;
            int userNumber = 0;

            offense = null;
            Console.Clear();
            ShowOffenses();
            Console.Write("\nВведите номер статьи: ");

            if (UserUtils.TryEnterNumberFromRange(0, _offenses.Count, out userNumber))
            {
                userNumber--;
                offense = _offenses[userNumber];
                isOffenseFound = true;
            }
            else
            {
                Console.WriteLine("Некорректный запрос!");
                Console.ReadKey();
            }

            return isOffenseFound;
        }

        private void ShowOffenses()
        {
            int sequenseNumber = 0;

            foreach (string offense in _offenses)
            {
                sequenseNumber++;
                Console.WriteLine($"{sequenseNumber}. {offense}");
            }
        }

        private void AmnestyPrisoners(string offense)
        {
            _prisoners = _prisoners.Where(prisoner => prisoner.Offense != offense).ToList();
            Console.Clear();
            Console.WriteLine("Заключённые амнистированы!");
            Console.ReadKey();
        }

        private void ShowAllPrisoners()
        {
            Console.Clear();
            ShowPrisoners(_prisoners);
            Console.ReadKey();
        }

        private void ShowPrisoners(List<Prisoner> prisoners)
        {
            foreach (Prisoner prisoner in prisoners)
            {
                prisoner.ShowInfo();
                Console.WriteLine();
            }
        }
    }

    class Prisoner
    {
        public Prisoner(string name, string offense)
        {
            Name = name;
            Offense = offense;
        }

        public string Name { get; private set; }
        public string Offense { get; private set; }

        public void ShowInfo()
        {
            Console.Write($"{Name} | {Offense}");
        }
    }

    class PrisonerCreator
    {
        private NamesCreator _namesCreator;
        private List<string> _offenses;

        public PrisonerCreator()
        {
            _namesCreator = new NamesCreator();
            _offenses = CreateOffenses();
        }

        public List<Prisoner> CreatePrisoners(int numberPrisoner = 20)
        {
            List<Prisoner> prisoners = new List<Prisoner>();

            for (int i = 0; i < numberPrisoner; i++)
            {
                prisoners.Add(CreatePrisoner());
            }

            return prisoners;
        }

        public List<string> CreateOffenses()
        {
            List<string> offenses = new List<string>
            {
                "Антиправительственная агитация",
                "Терроризм",
                "Грабёж",
                "Убийство",
                "Мошенничество"
            };

            return offenses;
        }

        private Prisoner CreatePrisoner()
        {
            string name = _namesCreator.GenerateFullName();
            string offense = _offenses[UserUtils.GenereteRandom(_offenses.Count)];

            return new Prisoner(name, offense);
        }
    }

    class NamesCreator
    {
        private List<string> _names;
        private List<string> _surnames;
        private List<string> _patronymics;

        public NamesCreator()
        {
            _names = CreateNames();
            _surnames = CreateSurnames();
            _patronymics = CreatePatronymics();
        }

        public string GenerateFullName()
        {
            string name = _names[UserUtils.GenereteRandom(_names.Count)];
            string surname = _surnames[UserUtils.GenereteRandom(_surnames.Count)];
            string patronymics = _patronymics[UserUtils.GenereteRandom(_patronymics.Count)];

            return $"{surname} {name} {patronymics}";
        }

        private List<string> CreateNames()
        {
            List<string> names = new List<string>
            {
                "Дмитрий",
                "Иван",
                "Сергей",
                "Антон",
                "Роман",
                "Артём",
                "Юрий",
                "Олег",
                "Святослав",
                "Марк"
            };

            return names;
        }

        private List<string> CreateSurnames()
        {
            List<string> surnames = new List<string>
            {
                "Ушаков",
                "Кутузов",
                "Белоцерковский",
                "Барабанов",
                "Лаптев",
                "Горелов",
                "Ковалёв",
                "Козлов",
                "Давлятов",
                "Суворов"
            };

            return surnames;
        }

        private List<string> CreatePatronymics()
        {
            List<string> patronymics = new List<string>
            {
                "Сергеевич",
                "Олегович",
                "Валерьевич",
                "Святославович",
                "Антонович",
                "Дмитриевич",
                "Семёнович",
                "Юрьевич",
                "Маркович",
                "Алексеевич"
            };

            return patronymics;
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

        public static bool TryEnterNumberFromRange(int numberMin, int numberMax, out int userNumber)
        {
            bool isCorrectNumberEntered = false;

            userNumber = 0;

            if (int.TryParse(Console.ReadLine(), out userNumber))
            {
                if (userNumber >= numberMin && userNumber <= numberMax)
                {
                    isCorrectNumberEntered = true;
                }
            }

            return isCorrectNumberEntered;
        }
    }
}
