using System;
using System.Collections.Generic;

namespace _6._10.War
{
    internal class Program
    {
        static void Main(string[] args)
        {
            War war = new War();

            war.Work();
        }
    }

    class War
    {
        private List<Squad> _squads;

        public War()
        {
            _squads = CreateSquads();
        }

        public void Work()
        {
            int turn = 0;

            while (_squads.Count > 1)
            {
                Queue<Squad> queueMovementSquads = CreateQueueMovement();

                turn++;
                Console.Clear();
                Console.WriteLine($"---=== ХОД {turn} ===---");

                while (queueMovementSquads.Count > 0)
                {
                    Squad currentSquad = queueMovementSquads.Dequeue();

                    TryMakeMove(currentSquad);
                }

                RemoveDestroyedSquads();
                Console.WriteLine();
                ShowFighters();
                Console.ReadKey();
            }
        }

        private Queue<Squad> CreateQueueMovement()
        {
            Queue<Squad> queueMovementSquad = new Queue<Squad>();
            Squad transmittedSquad = null;

            for (int i = 0; i < _squads.Count; i++)
            {
                for (int j = 0; j < _squads.Count; j++)
                {
                    if (queueMovementSquad.Contains(_squads[j]) == false)
                    {
                        transmittedSquad = _squads[j];
                        break;
                    }
                }

                for (int j = 0; j < _squads.Count; j++)
                {
                    if (_squads[j].Initiative > transmittedSquad.Initiative
                        && queueMovementSquad.Contains(_squads[j]) == false)
                    {
                        transmittedSquad = _squads[j];
                    }
                }

                queueMovementSquad.Enqueue(transmittedSquad);
            }

            return queueMovementSquad;
        }

        private void TryMakeMove(Squad squad)
        {
            if (squad.IsDestroyed == false)
            {
                Console.WriteLine($"{squad.NameFaction} проводит атаку:");
                squad.Attack(FindEnemySquad(squad));
                Console.WriteLine();
            }
        }

        private Squad FindEnemySquad(Squad alliedSquad)
        {
            Squad enemySquad = null;

            foreach (Squad squad in _squads)
            {
                if (squad != alliedSquad && squad.IsDestroyed == false)
                {
                    enemySquad = squad;
                }
            }

            return enemySquad;
        }

        private void RemoveDestroyedSquads()
        {
            List<Squad> destroyedSquads = new List<Squad>();

            foreach (Squad squad in _squads)
            {
                if (squad.IsDestroyed == true)
                {
                    destroyedSquads.Add(squad);
                }
            }

            foreach (Squad destroyedSquad in destroyedSquads)
            {
                _squads.Remove(destroyedSquad);
            }
        }

        private void ShowFighters()
        {
            Console.WriteLine($"---=== БОЙЦЫ ===---");
            Console.WriteLine();

            foreach (Squad squad in _squads)
            {
                Console.WriteLine($"======      {squad.NameFaction}     ======");
                Console.WriteLine();
                squad.ShowFighters();
                Console.WriteLine();
            }
        }

        private List<Squad> CreateSquads()
        {
            List<Squad> squads = new List<Squad>()
            {
                new Squad("Дом Лейте"),
                new Squad("Дом Гартел")
            };

            return squads;
        }
    }

    class Squad
    {
        private List<Fighter> _fighters;

        public Squad(string nameFaction)
        {
            _fighters = CreateFighters();
            Initiative = CreateInitiative();
            NameFaction = nameFaction;
            IsDestroyed = false;
        }

        public string NameFaction { get; private set; }
        public int Initiative { get; private set; }
        public bool IsDestroyed { get; private set; }

        public void ShowFighters()
        {
            foreach (Fighter fighter in _fighters)
            {
                if (fighter.IsDead == false)
                {
                    fighter.ShowInfo();
                    Console.WriteLine();
                }
            }
        }

        public void Attack(Squad squad)
        {
            foreach (Fighter fighter in _fighters)
            {
                if (squad.IsDestroyed == false)
                {
                    squad.TakeDamage(fighter);
                }
            }
        }

        public void TakeDamage(Fighter enemy)
        {
            Fighter fighter = _fighters[UserUtils.GenereteRandom(0, _fighters.Count)];

            enemy.Attack(fighter);

            if (fighter.IsDead == true)
            {
                _fighters.Remove(fighter);

                if (_fighters.Count == 0)
                {
                    BecomeDestroyed();
                }
            }
        }

        private void BecomeDestroyed()
        {
            Console.WriteLine($"    {NameFaction} - отряд полностью уничтожен!");
            IsDestroyed = true;
        }

        private List<Fighter> CreateFighters()
        {
            List<Fighter> fighters = new List<Fighter>();
            int maxFighters = 3;

            for (int i = 0; i < maxFighters; i++)
            {
                Fighter fighter = new Fighter();
                fighters.Add(fighter);
            }

            return fighters;
        }

        private int CreateInitiative()
        {
            int initiativeMax = 100;

            return UserUtils.GenereteRandom(0, initiativeMax);
        }

    }

    class Fighter
    {
        private Stats _statsBase;

        public Fighter()
        {
            _statsBase = new Stats();
            Name = Database.GetRandomName();
            Health = _statsBase.Health;
            Damage = _statsBase.Damage;
            IsDead = false;
        }

        public bool IsDead { get; private set; }
        public string Name { get; private set; }
        public int Health { get; private set; }
        public int Damage { get; private set; }

        public void ShowName()
        {
            Console.Write($"{Name}");
        }

        public void ShowStats()
        {
            Console.Write("Статус: ");

            if (IsDead == false)
            {
                Console.WriteLine("жив");
            }
            else
            {
                Console.WriteLine("мёртв");
            }

            Console.WriteLine($"Здоровье: {Health}/{_statsBase.Health}");
            Console.WriteLine($"Урон: {Damage}");
        }

        public void ShowInfo()
        {
            ShowName();
            Console.WriteLine();
            ShowStats();
        }

        public void Attack(Fighter enemy)
        {
            ShowName();
            Console.WriteLine($" обычная атака");
            enemy.TakeDamage(Damage);
        }

        public void TakeDamage(int damage)
        {
            Console.Write("    ");
            ShowName();
            Console.WriteLine($" получил урон - {damage}");

            if (Health > damage)
            {
                Health -= damage;
            }
            else
            {
                Die();
            }
        }

        public void Die()
        {
            Console.Write("    ");
            ShowName();
            Console.WriteLine($" погиб!");
            Health = 0;
            IsDead = true;
        }
    }

    class Stats
    {
        public Stats()
        {
            Health = GenerateHealth();
            Damage = GenerateDamage();
        }

        public int Health { get; private set; }
        public int Damage { get; private set; }

        private int GenerateHealth()
        {
            int maxHealth = 150;
            int minHealth = 50;

            return UserUtils.GenereteRandom(minHealth, maxHealth);
        }

        private int GenerateDamage()
        {
            int maxDamage = 20;
            int minDamage = 1;

            return UserUtils.GenereteRandom(minDamage, maxDamage);
        }
    }

    class Database
    {
        public static string GetRandomName()
        {
            List<string> names = new List<string>
            {
                "Корниенко",
                "Хохмайстер",
                "Ходжа",
                "Прифти",
                "Щеху",
                "Дервиши",
                "Бекташи",
                "Вагнер",
                "Хубер",
                "Миллер",
                "Майер",
                "Винклер",
                "Йегер",
                "Питерс",
                "Дюпон",
                "Клаас",
                "Ларсен",
                "Хансен",
                "Поульсен",
                "Мортенсен",
                "Маар",
                "Хайзенберг",
                "Андерссон",
                "Бегу",
                "Мехмед",
                "Мурад",
                "Хусейн",
                "Джейкоб",
                "Николас",
                "Кристиан",
                "Лукас",
                "Брахим",
                "Шкодер"
            };

            return names[UserUtils.GenereteRandom(0, names.Count)];
        }
    }

    class UserUtils
    {
        private static Random s_random = new Random();

        public static int GenereteRandom(int minValue, int maxValue)
        {
            int randomNumber = s_random.Next(minValue, maxValue);
            return randomNumber;
        }

        public static bool GenereteRandomBool()
        {
            int valueTrue = 1;

            return s_random.Next(valueTrue + 1) == 0;
        }
    }
}