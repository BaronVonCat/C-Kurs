using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _7._05.OpredelenieProsrochky
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int currentYear = 2024;
            PreservesCreator creator = new PreservesCreator();
            List<Preserves> preserves = creator.CreatePreserves(currentYear);

            ShowPreserves(preserves);
            Console.WriteLine();
            ShowPreserves(GetExpiredPreserves(preserves, currentYear));
            Console.ReadKey();
        }

        static List<Preserves> GetExpiredPreserves(List<Preserves> listPreserves, int currentYear)
        {
            List<Preserves> expiredPreserves = new List<Preserves>();

            expiredPreserves = listPreserves.Where(preserves => preserves.YearProduction + preserves.ExpirationDate >= currentYear).ToList();
            return expiredPreserves;
        }

        static void ShowPreserves(List<Preserves> preserves)
        {
            foreach (Preserves preserve in preserves)
            {
                preserve.Show();
                Console.WriteLine();
            }
        }
    }

    class PreservesCreator
    {
        public List<Preserves> CreatePreserves(int currentYear, int numberPreserves = 20)
        {
            List<Preserves> preserves = new List<Preserves>();

            for (int i = 0; i < numberPreserves; i++)
            {
                preserves.Add(CreateOnePreserves(currentYear));
            }

            return preserves;
        }

        private Preserves CreateOnePreserves(int currentYear)
        {
            int yearProductionMin = 2000;
            int yearProduction = UserUtils.GenereteRandom(yearProductionMin, currentYear + 1);

            List<Preserves> preserves = new List<Preserves>
            {
                new PreservesFish(yearProduction),
                new PreservesMeat(yearProduction),
                new PreservesFruit(yearProduction)
            };

            return preserves[UserUtils.GenereteRandom(preserves.Count)];
        }
    }

    class Preserves
    {
        public Preserves(string name, int expirationDate, int yearProduction)
        {
            Name = name;
            ExpirationDate = expirationDate;
            YearProduction = yearProduction;
        }

        public string Name { get; private set; }
        public int ExpirationDate { get; private set; }
        public int YearProduction { get; private set; }

        public void Show()
        {
            Console.Write($"{Name} | {YearProduction} | {ExpirationDate}");
        }
    }

    class PreservesFish : Preserves
    {
        public PreservesFish(int yearProduction) : base("Рыбная консерва", 5, yearProduction) { }
    }

    class PreservesMeat : Preserves
    {
        public PreservesMeat(int yearProduction) : base("Мясная консерва", 6, yearProduction) { }
    }

    class PreservesFruit : Preserves
    {
        public PreservesFruit(int yearProduction) : base("Фруктовая консерва", 4, yearProduction) { }
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
