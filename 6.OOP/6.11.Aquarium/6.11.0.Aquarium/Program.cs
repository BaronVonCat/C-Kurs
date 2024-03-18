using System;
using System.Collections.Generic;

namespace _6._11.Aquarium
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Aquarium aquarium = new Aquarium();
            bool isExit = false;

            while (isExit == false)
            {
                const string CommandExit = "0";
                const string CommandAddFish = "1";
                const string CommandRemoveFish = "2";
                const string CommandSkipTime = "3";

                string userInput = null;

                aquarium.ShowInfo();
                Console.WriteLine();
                Console.WriteLine($"Доступные команды:" +
                    $"\n{CommandExit} - Выйти из программы" +
                    $"\n{CommandAddFish} - Добавить рыбу" +
                    $"\n{CommandRemoveFish} - Убрать рыбу" +
                    $"\n{CommandSkipTime} - Пропустить время");
                Console.Write($"\nВведите номер комманды: ");
                userInput = Console.ReadLine();
                Console.Clear();

                switch (userInput)
                {
                    case CommandExit:
                        isExit = true;
                        break;

                    case CommandAddFish:
                        aquarium.TryAddFish(new Fish());
                        break;

                    case CommandRemoveFish:
                        aquarium.TryRemoveOneFish();
                        break;

                    case CommandSkipTime:
                        aquarium.SkipTime();
                        break;

                    default:
                        Console.Clear();
                        Console.WriteLine("Некорректный запрос!");
                        break;
                }

                aquarium.RemoveDeadFish();
            }
        }
    }

    class Aquarium
    {
        private List<Fish> _fish;

        public Aquarium()
        {
            _fish = new List<Fish>();
            Capacity = 5;
        }

        public int Capacity { get; private set; }

        public void ShowInfo()
        {
            if (_fish.Count > 0)
            {
                int sequenceNumber = 0;

                foreach (Fish fish in _fish)
                {
                    sequenceNumber++;
                    Console.Write($"{sequenceNumber}) ");
                    fish.ShowInfo();
                    Console.WriteLine();
                }
            }
            else
            {
                Console.WriteLine("Аквариум пуст.");
            }
        }

        public void SkipTime()
        {
            foreach (Fish fish in _fish)
            {
                fish.GrowAge();
            }
        }

        public void TryAddFish(Fish fish)
        {
            if (_fish.Count < Capacity)
            {
                _fish.Add(fish);
            }
            else
            {
                Console.WriteLine("В аквариуме нет мест!");
            }
        }

        public void TryRemoveOneFish()
        {
            string userInput = null;
            int userNumber = 0;

            ShowInfo();
            Console.WriteLine();
            Console.Write("Введите порядковый номер рыбы, чтобы убрать её из аквариума: ");
            userInput = Console.ReadLine();

            if (int.TryParse(userInput, out userNumber))
            {
                userNumber--;

                if (userNumber >= 0 && userNumber < _fish.Count)
                {
                    Fish fish = _fish[userNumber];
                    Console.Clear();
                    Console.WriteLine($"{fish.Name} убрана из аквариума.");
                    _fish.Remove(fish);
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine("Такой рыбы нет в аквариуме!");
                }
            }
            else
            {
                Console.Clear();
                Console.WriteLine("Некорректный запрос!");
            }
        }

        public void RemoveDeadFish()
        {
            for (int i = _fish.Count - 1; i >= 0; i--)
            {
                if (_fish[i].IsDead == true)
                {
                    _fish.RemoveAt(i);
                }
            }
        }
    }

    class Fish
    {
        private static int s_createdFishNames;

        static Fish()
        {
            s_createdFishNames = 0;
        }

        public Fish()
        {
            Name = CreateNameFish();
            AgeLimit = CreateAgeLimit();
            Age = UserUtils.GenereteRandom(0, AgeLimit);
        }

        public string Name { get; private set; }
        public int AgeLimit {  get; private set; }
        public int Age { get; private set; }
        public bool IsDead => Age > AgeLimit;

        public void ShowInfo()
        {
            Console.Write($"{Name} | возраст - {Age}/{AgeLimit}");
        }

        public void GrowAge() 
        {
            Age++;

            if (IsDead == true)
            {
                Console.WriteLine($"{Name} умрела от старости");
            }
        }

        private static string CreateNameFish()
        {
            string fishName = "Рыба";
            string newFishName;

            s_createdFishNames++;
            newFishName = $"{fishName} {s_createdFishNames}";
            return newFishName;
        }

        private int CreateAgeLimit()
        {
            int ageLimitMax = 15;
            int ageLimitMin = 5;

            return UserUtils.GenereteRandom(ageLimitMin, ageLimitMax);
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
    }
}
