using System;
using System.Collections.Generic;
using System.Linq;

namespace _7._01.PoiskPrestupnika
{
    internal class Program
    {
        static void Main(string[] args)
        {
            HumanCreator creator = new HumanCreator();
            HumanDatabase database = new HumanDatabase(creator);

            database.Work();
        }
    }

    class HumanDatabase
    {
        private List<Human> _humans;

        public HumanDatabase(HumanCreator creator)
        {
            _humans = creator.CreateHumans();
        }

        public void Work()
        {
            const string CommandExit = "0";
            const string CommandSearch = "1";
            const string CommandShowAllHumans = "2";

            bool isExit = false;

            while (isExit == false)
            {
                Console.Clear();
                Console.WriteLine("БАЗА ДАННЫХ ГРАЖДАН");
                Console.WriteLine($"\nДоступные команды:" +
                    $"\n{CommandExit}. Выход из программы" +
                    $"\n{CommandSearch}. Поиск гражданина" +
                    $"\n{CommandShowAllHumans}. Показать всех граждан");
                Console.WriteLine();
                Console.Write("Введите номер необходимой комманды: ");

                switch (Console.ReadLine())
                {
                    case CommandExit:
                        isExit = true;
                        break;

                    case CommandSearch:
                        SearchHuman();
                        break;

                    case CommandShowAllHumans:
                        ShowAllHumans();
                        break;

                    default:
                        Console.Clear();
                        Console.WriteLine("Некорректный запрос!");
                        Console.ReadKey();
                        break;
                }
            }
        }

        private void SearchHuman()
        {
            string nationality = null;
            int whole = 0;
            int height = 0;
            IEnumerable<Human> filteredHumans = null;

            Console.Clear();
            nationality = EnterNationalityAndOtherData(out whole, out height);
            filteredHumans = FilterHumansByParameters(nationality, whole, height);
            ShowFilteredHumans(filteredHumans);
            Console.ReadKey();
        }

        private string EnterNationalityAndOtherData(out int whole, out int height)
        {
            string nationality = null;

            Console.WriteLine("Укажите данные гражданина");
            Console.Write("Национальность: ");
            nationality = Console.ReadLine();
            Console.Write("Вес: ");
            int.TryParse(Console.ReadLine(), out whole);
            Console.Write("Рост: ");
            int.TryParse(Console.ReadLine(), out height);
            return nationality;
        }

        private IEnumerable<Human> FilterHumansByParameters(string nationality, int whole, int height)
        {
            IEnumerable<Human> filteredHumans = null;
            int increment = 5;

            filteredHumans = from Human human in _humans
                             where human.Whole <= whole + increment
                             && human.Whole >= whole - increment
                             && human.Height <= height + increment
                             && human.Height >= height - increment
                             && human.Nationality.ToLower() == nationality.ToLower()
                             && human.IsArrested == false
                             select human;
            return filteredHumans;
        }

        private void ShowFilteredHumans(IEnumerable<Human> filteredHumans)
        {
            if (filteredHumans.Count() > 0)
            {
                foreach (var human in filteredHumans)
                {
                    human.ShowInfo();
                    Console.WriteLine();
                }
            }
            else
            {
                Console.WriteLine("Таких граждан нет в базе данных");
            }
        }

        private void ShowAllHumans()
        {
            Console.Clear();

            foreach (Human human in _humans)
            {
                human.ShowInfo();
                Console.WriteLine();
            }

            Console.ReadKey();
        }
    }

    class Human
    {
        public Human(string name, string nationality, bool isArrested, int height, int whole)
        {
            Name = name;
            Nationality = nationality;
            Height = height;
            Whole = whole;
            IsArrested = isArrested;
        }

        public string Name { get; private set; }
        public string Nationality { get; private set; }
        public bool IsArrested { get; private set; }
        public int Height { get; private set; }
        public int Whole { get; private set; }

        public void ShowInfo()
        {
            Console.Write($"{Name} | {Nationality} | {Height} | {Whole} | ");

            if (IsArrested == true)
            {
                Console.Write("Под арестом");
            }
        }
    }

    class HumanCreator
    {
        private NamesCreator _namesCreator;

        public HumanCreator()
        {
            _namesCreator = new NamesCreator();
        }

        public List<Human> CreateHumans(int numberHuman = 1000)
        {
            List<Human> humans = new List<Human>();

            for (int i = 0; i < numberHuman; i++)
            {
                humans.Add(CreateHuman());
            }

            return humans;
        }

        private Human CreateHuman() 
        {
            string natonality;
            string name = _namesCreator.GenerateNationalFullName(out natonality);
            bool isArrested = UserUtils.GenereteRandomBool();
            int height = CreateHeight();
            int whole = CreateWhole();

            return new Human(name, natonality, isArrested, height, whole);
        }

        private int CreateHeight()
        {
            int heightMin = 150;
            int heightMax = 210;

            return UserUtils.GenereteRandom(heightMin, heightMax);
        }

        private int CreateWhole()
        {
            int wholeMin = 40;
            int wholeMax = 150;

            return UserUtils.GenereteRandom(wholeMin, wholeMax);
        }
    }
    
    class NamesCreator
        {
            private List<NamesCollection> _namesCollections;

            public NamesCreator()
            {
                _namesCollections = CreateNamesCollections();
            }

            public string GenerateNationalFullName(out string nationality)
            {
                NamesCollection namesCollection = _namesCollections[UserUtils.GenereteRandom(_namesCollections.Count())];
                string name = namesCollection.GetRandomFullName();

                nationality = namesCollection.Nationality;
                return name;
            }

            private List<NamesCollection> CreateNamesCollections()
            {
                List<NamesCollection> namesCollections = new List<NamesCollection>
                {
                    new NamesCollectionChinese(),
                    new NamesCollectionFrench(),
                    new NamesCollectionItalian(),
                    new NamesCollectionGerman(),
                    new NamesCollectionRussian(),
                };

                return namesCollections;
            }
        }

    class NamesCollection
        {
            protected List<string> Names;
            protected List<string> Surnames;

            public NamesCollection(string nationality)
            {
                Names = CreateNames();
                Surnames = CreateSurnames();
                Nationality = nationality;
            }

            public string Nationality { get; private set; }

            public string GetRandomFullName()
            {
                string fullName = null;

                if (Names.Count > 0 && Surnames.Count > 0)
                {
                    string name = Names[UserUtils.GenereteRandom(Names.Count)];
                    string surname = Surnames[UserUtils.GenereteRandom(Surnames.Count)];

                    fullName = $"{surname} {name}";
                }

                return fullName;
            }

            protected virtual List<string> CreateNames() 
            {
                return new List<string>();
            }

            protected virtual List<string> CreateSurnames()  
            {
                return new List<string>();
            }
        }

    class NamesCollectionGerman : NamesCollection
        {
            public NamesCollectionGerman() : base("Немец")
            {
                Names = CreateNames();
                Surnames = CreateSurnames();
            }

            protected override List<string> CreateNames()
            {
                List<string> names = new List<string>
                {
                    "Эдуард",
                    "Кай",
                    "Хинрих",
                    "Бёрндт",
                    "Герт",
                    "Лотар"
                };

                return names;
            }

            protected override List<string> CreateSurnames()
            {
                List<string> surnames = new List<string> 
                {
                    "Курцманн",
                    "Хольцер",
                    "Маус",
                    "Фельд",
                    "Бергер",
                    "Хазе"
                };

                return surnames;
            }
        }

    class NamesCollectionFrench : NamesCollection
        {
            public NamesCollectionFrench() : base("Француз")
            {
                Names = CreateNames();
                Surnames = CreateSurnames();
            }

            protected override List<string> CreateNames()
            {
                List<string> names = new List<string>
                {
                    "Ролан",
                    "Пьер",
                    "Виктор",
                    "Роже",
                    "Камиль",
                    "Ив"
                };

                return names;
            }

            protected override List<string> CreateSurnames()
            {
                List<string> surnames = new List<string>
                {
                    "Шаньон",
                    "Барбо",
                    "Поль",
                    "Монгрен",
                    "Дюран",
                    "Риашар"
                };

                return surnames;
            }
        }

    class NamesCollectionRussian : NamesCollection
        {
            public NamesCollectionRussian() : base("Русский")
            {
                Names = CreateNames();
                Surnames = CreateSurnames();
            }

            protected override List<string> CreateNames()
            {
                List<string> names = new List<string>
                {
                    "Дмитрий",
                    "Антон",
                    "Владислав",
                    "Роман",
                    "Сергей",
                    "Иван"
                };

                return names;
            }

            protected override List<string> CreateSurnames()
            {
                List<string> surnames = new List<string>
                {
                    "Твердохлебов",
                    "Белоцерковский",
                    "Корытов",
                    "Семёнов",
                    "Козлов",
                    "Барабанов"
                };

                return surnames;
            }
        }

    class NamesCollectionChinese : NamesCollection
        {
            public NamesCollectionChinese() : base("Китаец")
            {
                Names = CreateNames();
                Surnames = CreateSurnames();
            }

            protected override List<string> CreateNames()
            {
                List<string> names = new List<string>
                {
                    "Фу",
                    "Су",
                    "Чэн",
                    "Тань",
                    "Цзан",
                    "Сань"
                };

                return names;
            }

            protected override List<string> CreateSurnames()
            {
                List<string> surnames = new List<string>
                {
                    "Пяньшао",
                    "Баньхай",
                    "Диши",
                    "Шуйне",
                    "Чжэнь",
                    "Цзэн"
                };

                return surnames;
            }
        }

    class NamesCollectionItalian : NamesCollection
        {
            public NamesCollectionItalian() : base("Итальянец")
            {
                Names = CreateNames();
                Surnames = CreateSurnames();
            }

            protected override List<string> CreateNames()
            {
                List<string> names = new List<string>
                {
                    "Ладислао",
                    "Фабрицио",
                    "Роберто",
                    "Джузеппе",
                    "Берте",
                    "Лоренцо"
                };

                return names;
            }

            protected override List<string> CreateSurnames()
            {
                List<string> surnames = new List<string>
                {
                    "Арено",
                    "Маттиоли",
                    "Лилло",
                    "Нери",
                    "Спинелли",
                    "Рива"
                };

                return surnames;
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
