using System;
using System.Collections.Generic;
using System.Linq;

namespace _7._06.OtchetOVooruzjeniy
{
    internal class Program
    {
        static void Main(string[] args)
        {
            SoldierCreator creator = new SoldierCreator();
            List<Soldier> soldiers = creator.CreateSoldiers();

            var newSoldiers = soldiers.Select(soldier => new
            {
                Name = soldier.Name,
                Rank = soldier.Rank
            });

            foreach (Soldier soldier in soldiers)
            {
                soldier.ShowInfo();
                Console.WriteLine();
            }

            Console.WriteLine();

            foreach (var soldier in newSoldiers)
            {
                Console.WriteLine($"{soldier.Name} | {soldier.Rank}");
            }

            Console.ReadKey();
        }
    }

    class Soldier
    {
        public Soldier(string name, string rank, string weapon, int serviceLifeInMonths)
        {
            Name = name;
            Rank = rank;
            Weapon = weapon;
            ServiceLifeInMonths = serviceLifeInMonths;
        }

        public string Name { get; private set; }
        public string Rank { get; private set; }
        public string Weapon {  get; private set; }
        public int ServiceLifeInMonths { get; private set; }

        public void ShowInfo()
        {
            Console.Write($"{Name} | {Rank} | {Weapon} | {ServiceLifeInMonths}");
        }
    }

    class SoldierCreator
    {
        private NamesCreator _namesCreator;

        public SoldierCreator()
        {
            _namesCreator = new NamesCreator();
        }

        public List<Soldier> CreateSoldiers(int numberSoldiers = 20)
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
            string weapon = GenerasteWeapon();
            int serviceLifeInMonths = GenerateServiceLifeInMonths();

            return new Soldier(name, rank, weapon, serviceLifeInMonths);
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

        private string GenerasteWeapon()
        {
            List<string> weapons = new List<string>
            {
                "Снайперская винтовка",
                "Гранатамёт",
                "Винтовка",
                "Автомат",
                "Пистолет",
            };

            return weapons[UserUtils.GenereteRandom(weapons.Count)];
        }

        private int GenerateServiceLifeInMonths()
        {
            int serviceLifeInMonthsMax = 240;

            return UserUtils.GenereteRandom(serviceLifeInMonthsMax);
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

