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
                    Squad squad = queueMovementSquads.Dequeue();

                    if (TryMakeMove(squad))
                    {
                        queueMovementSquads.Enqueue(squad);
                    }
                }

                RemoveDestroyedSquads();
                RestoreAbilityMoveFightersInSquads();
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

        private bool TryMakeMove(Squad squad)
        {
            bool isMove = false;

            if (squad.IsDestroyed == false)
            {
                Squad enemySquad = null;
                Fighter  fighter = null;

                if (squad.TryGetRandomAttackingFighter(out fighter)
                    && TryFindEnemySquad(squad, out enemySquad))
                {
                    Fighter enemyFighter = null;

                    if (enemySquad.TryGetRandomFighter(out enemyFighter))
                    {
                        fighter.Attack(enemyFighter);
                        enemySquad.RemoveDeadFighters();
                        isMove = true;
                    }
                }
            }

            return isMove;
        }

        private bool TryFindEnemySquad(Squad alliedSquad, out Squad enemySquad)
        {
            List<Squad> enemySquads = new List<Squad>();
            bool hasEnemyFound = false;

            enemySquad = null;

            foreach (Squad squad in _squads)
            {
                if (squad != alliedSquad && squad.IsDestroyed == false)
                {
                    enemySquads.Add(squad);
                    hasEnemyFound = true;
                }
            }

            if (hasEnemyFound == true)
            {
                enemySquad = enemySquads[UserUtils.GenereteRandom(0, enemySquads.Count)];
            }
            

            return hasEnemyFound;
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

        private void RestoreAbilityMoveFightersInSquads()
        {
            foreach (Squad squad in _squads)
            {
                squad.RestoreAbilityMoveFighters();
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
            NameFaction = nameFaction;
            _fighters = CreateFighters();
            Initiative = CreateInitiative();
            RestoreAbilityMoveFighters();
        }

        public string NameFaction { get; private set; }
        public int Initiative { get; private set; }
        public bool IsDestroyed => _fighters.Count == 0;

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

        public bool TryGetRandomAttackingFighter(out Fighter attackingFighter)
        {
            List<Fighter> fighters = new List<Fighter>();
            bool hasFighterFound = false;

            attackingFighter = null;

            foreach (Fighter fighter in _fighters)
            {
                if (fighter.IsMove == true)
                {
                    fighters.Add(fighter);
                    hasFighterFound = true;
                }
            }

            if (hasFighterFound == true)
            {
                attackingFighter = fighters[UserUtils.GenereteRandom(0, fighters.Count)];
            }

            return hasFighterFound;
        }

        public bool TryGetRandomFighter(out Fighter fighter)
        {
            bool hasFighterFound = false;

            fighter = null;

            if (_fighters.Count > 0)
            {
                fighter = _fighters[UserUtils.GenereteRandom(0, _fighters.Count)];
                hasFighterFound = true;
            }

            return hasFighterFound;
        }

        public void RestoreAbilityMoveFighters()
        {
            foreach (Fighter fighter in _fighters)
            {
                fighter.RestoreAbilityMove();
            }
        }

        public void RemoveDeadFighters()
        {
            for (int i = _fighters.Count - 1; i > -1 ; i--)
            {
                if (_fighters[i].IsDead == true)
                {
                    _fighters.RemoveAt(i);
                }
            }

            if(_fighters.Count == 0)
            {
                BecomeDestroyed();
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
                Fighter fighter = new Fighter(NameFaction);
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
        private string _nameFaction;

        public Fighter(string nameFaction)
        {
            _statsBase = new Stats();
            _nameFaction = nameFaction;
            Name = Database.GetRandomName();
            Health = _statsBase.Health;
            Damage = _statsBase.Damage;
            IsDead = false;
            IsMove = true;
        }

        public bool IsDead { get; private set; }
        public bool IsMove {  get; private set; }
        public string Name { get; private set; }
        public int Health { get; private set; }
        public int Damage { get; private set; }

        public void ShowInfo()
        {
            Console.WriteLine(Name);
            ShowStats();
        }

        public void Attack(Fighter enemy)
        {
            Console.WriteLine($"{_nameFaction} {Name} обычная атака");
            enemy.TakeDamage(Damage);
            IsMove = false;
        }

        public void TakeDamage(int damage)
        {
            Console.WriteLine($"    {_nameFaction} {Name} получил урон - {damage}");

            if (Health > damage)
            {
                Health -= damage;
            }
            else
            {
                Die();
            }
        }

        public void RestoreAbilityMove()
        {
            IsMove = true;
        }

        private void Die()
        {
            Console.WriteLine($"    {Name} погиб!");
            Health = 0;
            IsDead = true;
        }

        private void ShowStats()
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
    }
}