using System;
using System.Collections.Generic;
using System.Linq;

namespace _7._05.OpredelenieProsrochky
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int currentYear = 2024;
            CannedFoodCreator creator = new CannedFoodCreator();
            List<CannedFood> cannedFoods = creator.CreateCannedFoods(currentYear);

            ShowCannedFoods(cannedFoods);
            Console.WriteLine();
            ShowCannedFoods(GetExpiredCannedFoods(cannedFoods, currentYear));
            Console.ReadKey();
        }

        static List<CannedFood> GetExpiredCannedFoods(List<CannedFood> cannedFoods, int currentYear)
        {
            return cannedFoods.Where(cannedFood => cannedFood.YearProduction + cannedFood.ExpirationDate < currentYear).ToList();
        }

        static void ShowCannedFoods(List<CannedFood> cannedFoods)
        {
            foreach (CannedFood cannedFood in cannedFoods)
            {
                cannedFood.Show();
                Console.WriteLine();
            }
        }
    }

    class CannedFoodCreator
    {
        public List<CannedFood> CreateCannedFoods(int currentYear, int numberCannedFoods = 20)
        {
            List<CannedFood> cannedFoods = new List<CannedFood>();

            for (int i = 0; i < numberCannedFoods; i++)
            {
                cannedFoods.Add(CreateCannedFood(currentYear));
            }

            return cannedFoods;
        }

        private CannedFood CreateCannedFood(int currentYear)
        {
            int yearProductionMin = 2000;
            int yearProduction = UserUtils.GenereteRandom(yearProductionMin, currentYear + 1);

            List<CannedFood> cannedFoods = new List<CannedFood>
            {
                new CannedFood("Мясная консерва", 5, yearProduction),
                new CannedFood("Рыбная консерва", 7, yearProduction),
                new CannedFood("Фруктовая консерва", 3, yearProduction)
            };

            return cannedFoods[UserUtils.GenereteRandom(cannedFoods.Count)];
        }
    }

    class CannedFood
    {
        public CannedFood(string name, int expirationDate, int yearProduction)
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
    }
}
