using System;
using System.Collections.Generic;
using System.Linq;

namespace _7._07.ObjedineniyeVoiysk
{
    internal class Program
    {
        static void Main(string[] args)
        {
            SoldierCreator creator = new SoldierCreator();
            List<Soldier> soldiersA = creator.CreateSoldiers();
            List<Soldier> soldiersB = creator.CreateSoldiers();
            string firstLetter = "Б";
            List<Soldier> transferSoldiers = soldiersA.Where(soldier => soldier.Name.StartsWith(firstLetter)).ToList();

            Console.WriteLine("До перевода:");
            Console.WriteLine("Отряд А");
            ShowSoldiers(soldiersA);
            Console.WriteLine();
            Console.WriteLine("Отряд Б");
            ShowSoldiers(soldiersB);
            Console.WriteLine();
            soldiersB = soldiersB.Union(transferSoldiers).ToList();
            soldiersA = soldiersA.Except(transferSoldiers).ToList();
            Console.WriteLine("После перевода:");
            Console.WriteLine("Отряд А");
            ShowSoldiers(soldiersA);
            Console.WriteLine();
            Console.WriteLine("Отряд Б");
            ShowSoldiers(soldiersB);
            Console.ReadKey();
        }

        static void ShowSoldiers(List<Soldier> soldiers)
        {
            foreach (Soldier soldier in soldiers)
            {
                soldier.ShowInfo();
                Console.WriteLine();
            }
        }
    }

    class Soldier
    {
        public Soldier(string name, string rank)
        {
            Name = name;
            Rank = rank;
        }

        public string Name { get; private set; }
        public string Rank { get; private set; }

        public void ShowInfo()
        {
            Console.Write($"{Name} | {Rank}");
        }
    }

    class SoldierCreator
    {
        private NamesCreator _namesCreator;

        public SoldierCreator()
        {
            _namesCreator = new NamesCreator();
        }

        public List<Soldier> CreateSoldiers(int numberSoldiers = 10)
        {
            List<Soldier> soldiers = new List<Soldier>();

            for (int i = 0; i < numberSoldiers; i++)
            {
                soldiers.Add(CreateSoldier());
            }

            return soldiers;
        }

        private Soldier CreateSoldier()
        {
            string name = _namesCreator.GenerateFullName();
            string rank = GenerateRank();

            return new Soldier(name, rank);
        }

        private string GenerateRank()
        {
            List<string> ranks = new List<string>
            {
                "Рядовой",
                "Ефврейтор",
                "Сержант",
                "Прапорщик",
                "Лейтенант",
                "Капитан"
            };

            return ranks[UserUtils.GenereteRandom(ranks.Count)];
        }
    }

    class NamesCreator
    {
        public string GenerateFullName()
        {
            return $"{GenerateSurnames()} {GenerateNames()} {GeneratePatronymics()}";
        }

        private string GenerateNames()
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

            return names[UserUtils.GenereteRandom(names.Count)];
        }

        private string GenerateSurnames()
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

            return surnames[UserUtils.GenereteRandom(surnames.Count)];
        }

        private string GeneratePatronymics()
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
    }
}
