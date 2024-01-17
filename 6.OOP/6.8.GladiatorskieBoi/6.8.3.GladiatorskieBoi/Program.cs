using System;
using System.Collections.Generic;

namespace _6._8.GladiatorskieBoi
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Arena arena = new Arena();

            arena.Work();
        }
    }

    class Arena
    {
        private List<Fighter> _fightersCandidates;

        public Arena()
        {
            _fightersCandidates = CreateCandidates();
        }

        public void Work()
        {
            List<Fighter> fighters = new List<Fighter>();
            bool isWork = true;

            while (isWork == true)
            {
                const string CommandExit = "exit";
                const string CommandFight = "fight";
                
                string userInput;
                int userNumber;
                bool isNumber;

                Console.Clear();
                Console.WriteLine("Укажите номер бойца, чтобы вывести его на арену, " +
                    "\nили текст небоходимой команды.");
                Console.WriteLine("\nКомманды:" +
                    $"\n{CommandExit} - Выход" +
                    $"\n{CommandFight} - Провести бой");
                Console.WriteLine("\n--Кандидаты на бой--\n");
                ShowFighters(_fightersCandidates);
                Console.WriteLine("\n--Бойцы--\n");
                ShowFighters(fighters);
                userInput = Console.ReadLine();
                isNumber = int.TryParse(userInput, out userNumber);

                if (isNumber == true)
                {
                    TryTransferCandidateToFighters(fighters, userNumber - 1);
                }
                else
                {
                    switch (userInput)
                    {
                        case CommandExit:
                            isWork = false;
                            break;

                        case CommandFight:
                            TryCreateFight(fighters);
                            break;

                        default:
                            Console.Clear();
                            Console.WriteLine("Некорректный запрос!");
                            Console.ReadKey();
                            break;
                    }
                }
            }
        }

        private void ShowFighters(List<Fighter> fighters)
        {
            int sequenceNumber = 0;

            foreach (Fighter fighter in fighters)
            {
                sequenceNumber++;
                Console.Write($"{sequenceNumber}. ");
                fighter.ShowFullName();
                Console.WriteLine();
                fighter.ShowStats();
                Console.WriteLine();
            }
        }

        private void TryTransferCandidateToFighters(List<Fighter> fighters, int index)
        {
            if (fighters.Count <= 1)
            {
                bool isFighterFound = false;
                Fighter fighter = null;

                for (int i = 0; i < _fightersCandidates.Count; i++)
                {
                    if (i == index)
                    {
                        fighter = _fightersCandidates[i];
                        isFighterFound = true;
                    }
                }

                if (isFighterFound == true)
                {
                    fighters.Add(fighter);
                    _fightersCandidates.Remove(fighter);
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine("Такого бойца нет среди кандидатов!");
                    Console.ReadKey();
                }
            }
            else
            {
                Console.Clear();
                Console.WriteLine("Бойцы уже выбраны!");
                Console.ReadKey();
            }
        }

        private void TryCreateFight(List<Fighter> fighters)
        {
            if (fighters.Count > 1)
            {
                Battle fight;
                int teamNumber = 0;

                foreach (Fighter fighter in fighters)
                {
                    teamNumber++;
                    fighter.SetTeamNumber(teamNumber);
                }

                fight = new Battle(fighters);
                fight.Conduct();
                fighters.Clear();
                _fightersCandidates = CreateCandidates();
            }
            else
            {
                Console.Clear();
                Console.WriteLine("Недостаточно бойцов для боя!");
                Console.ReadKey();
            }
        }

        private List<Fighter> CreateCandidates()
        {
            List<Fighter> fighters = new List<Fighter>();
            int maxFighters = 5;

            for (int i = 0; i < maxFighters; i++)
            {
                Fighter fighter = CreateCandidate();

                fighters.Add(fighter);
            }

            return fighters;
        }

        private Fighter CreateCandidate()
        {
            List<Fighter> fighters = new List<Fighter>
            {
                new Assassin(),
                new Fencer(),
                new Flagelant()
            };

            return fighters[UserUtils.GenereteRandom(0, fighters.Count)];
        }
    }

    class Battle
    {
        private List<Fighter> _fighters;

        public Battle(List<Fighter> fighters)
        {
            _fighters = fighters;
        }

        public void Conduct()
        {
            int turn = 0;

            while (_fighters.Count > 1)
            {
                Queue<Fighter> queueMovementFighters = CreateQueueMovement();

                turn++;
                Console.Clear();
                Console.WriteLine($"---=== ХОД {turn} ===---");

                while (queueMovementFighters.Count > 0)
                {
                    Fighter currentFighter = queueMovementFighters.Dequeue();

                    TryMakeMove(currentFighter);
                }

                RemoveDeadFighters();
                Console.WriteLine();
                ShowFighters();
                Console.ReadKey();
            }

            Console.WriteLine();
            _fighters[0].ShowFullName();
            Console.WriteLine($" одержал победу!!!");
            Console.ReadKey();
        }

        private Queue<Fighter> CreateQueueMovement()
        {
            Queue<Fighter> queueMovementFighters = new Queue<Fighter>();
            Fighter transmittedFighter = null;

            for (int i = 0; i < _fighters.Count; i++)
            {
                for (int j = 0; j < _fighters.Count; j++)
                {
                    if (queueMovementFighters.Contains(_fighters[j]) == false)
                    {
                        transmittedFighter = _fighters[j];
                        break;
                    }
                }

                for (int j = 0; j < _fighters.Count; j++)
                {
                    if (_fighters[j].Initiative > transmittedFighter.Initiative
                        && queueMovementFighters.Contains(_fighters[j]) == false)
                    {
                        transmittedFighter = _fighters[j];
                    }
                }

                queueMovementFighters.Enqueue(transmittedFighter);
            }

            return queueMovementFighters;
        }

        private void TryMakeMove(Fighter fighter)
        {
            if (fighter.IsDead == false)
            {
                List<Fighter> enemies = FindEnemies(fighter.TeamNumber);

                Console.WriteLine();

                while (fighter.ActionPoints > 0 && enemies.Count > 0)
                {
                    Fighter enemy = enemies[UserUtils.GenereteRandom(0, enemies.Count)];
                    fighter.TryAttack(enemy);
                    enemies = FindEnemies(fighter.TeamNumber);
                }

                fighter.RestoreActionPoints();
            }
        }

        private List<Fighter> FindEnemies(int teamNumberAllied)
        {
            List<Fighter> enemies = new List<Fighter>();

            foreach (Fighter fighter in _fighters)
            {
                if (fighter.TeamNumber != teamNumberAllied && fighter.IsDead == false)
                {
                    enemies.Add(fighter);
                }
            }

            return enemies;
        }

        private void RemoveDeadFighters()
        {
            List<Fighter> deadFighters = new List<Fighter>();

            foreach (Fighter fighter in _fighters)
            {
                if (fighter.IsDead == true)
                {
                    deadFighters.Add(fighter);
                }
            }

            foreach (Fighter deadFighter in deadFighters)
            {
                _fighters.Remove(deadFighter);
            }
        }

        private void ShowFighters()
        {
            Console.WriteLine($"---=== БОЙЦЫ ===---");

            foreach (Fighter fighter in _fighters)
            {
                Console.WriteLine();
                fighter.ShowInfo();
            }
        }
    }

    class Fighter
    {
        protected Stats StatsBase;

        public Fighter()
        {
            StatsBase = new Stats();
            Name = DataBase.GetRandomName();
            Type = null;
            Initiative = StatsBase.Initiative;
            Health = StatsBase.Health;
            Damage = StatsBase.Damage;
            ActionPoints = StatsBase.ActionPoints;
            IsDead = false;
        }

        public bool IsDead { get; protected set; }
        public string Name { get; protected set; }
        public string Type { get; protected set; }
        public int Initiative { get; protected set; }
        public int Health { get; protected set; }
        public int Damage { get; protected set; }
        public int ActionPoints { get; protected set; }
        public int TeamNumber { get; protected set; }

        public void ShowFullName()
        {
            if (Type != null)
            {
                Console.Write($"{Name} {Type}");
            }
            else
            {
                Console.Write($"{Name}");
            }
        }

        public virtual void ShowStats()
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

            Console.WriteLine($"Здоровье: {Health}/{StatsBase.Health}");
            Console.WriteLine($"Инициатива: {Initiative}");
            Console.WriteLine($"Урон: {Damage}");
        }

        public void ShowInfo()
        {
            ShowFullName();
            Console.WriteLine();
            ShowStats();
        }

        public void SetTeamNumber(int teamNumber)
        {
            TeamNumber = teamNumber;
        }

        public virtual void TryAttack(Fighter enemy)
        {
            const int ActionNumberAttack = 0;
            const int ActionNumberSpecialAttack = 1;

            switch (UserUtils.GenereteRandom(0, ActionNumberSpecialAttack + 1))
            {
                case ActionNumberAttack:
                    Attack(enemy);
                    break;

                case ActionNumberSpecialAttack:
                    TryMakeSpecialAttack(enemy);
                    break;
            }
        }

        public virtual void TakeDamage(int damage)
        {
            Console.Write("    ");
            ShowFullName();
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
            ShowFullName();
            Console.WriteLine($" погиб!");
            Health = 0;
            IsDead = true;
        }

        public void RestoreActionPoints()
        {
            ActionPoints = StatsBase.ActionPoints;
        }

        public void Attack(Fighter enemy)
        {
            ShowFullName();
            Console.WriteLine($" обычная атака");
            ActionPoints--;
            enemy.TakeDamage(Damage);
        }

        protected void TryMakeSpecialAttack(Fighter enemy)
        {
            int actionPointsPrice = 2;

            if (ActionPoints >= actionPointsPrice)
            {
                ActionPoints -= actionPointsPrice;
                MakeSpecialAttack(enemy);
            }
        }

        protected virtual void MakeSpecialAttack(Fighter enemy)
        {
            ShowFullName();
            Console.WriteLine($" применяет специальный навык");
            enemy.TakeDamage(Damage);
        }
    }

    class Assassin : Fighter
    {
        public Assassin() : base()
        {
            Type = "Ассасин";
            Dodge = GenerateDodge();
        }

        public int Dodge { get; private set; }

        public override void ShowStats()
        {
            base.ShowStats();
            Console.WriteLine($"Уклонение: {Dodge}");
        }

        protected override void MakeSpecialAttack(Fighter enemy)
        {
            if (UserUtils.GenereteRandomBool() == true)
            {
                int damageMultiplier = 3;
                int damage = Damage * damageMultiplier;
                ShowFullName();
                Console.WriteLine($" наносит критический удар");
                enemy.TakeDamage(damage);
            }
            else
            {
                ShowFullName();
                Console.WriteLine($" наносит обычный удар");
                enemy.TakeDamage(Damage);
            }
        }

        public override void TakeDamage(int damage)
        {
            if (TryDodge() == false)
            {
                base.TakeDamage(damage);
            }
        }

        private bool TryDodge()
        {
            bool hasManageDodge = false;
            int dodgeMax = 100;
            int dodgeMin = 0;

            if (Dodge >= UserUtils.GenereteRandom(dodgeMin, dodgeMax))
            {
                Console.WriteLine($"    {Name} уклонился");
                hasManageDodge = true;
            }

            return hasManageDodge;
        }

        private int GenerateDodge()
        {
            int maxDodge = 50;
            int minDodge = 1;

            return UserUtils.GenereteRandom(minDodge, maxDodge);
        }
    }

    class Fencer : Fighter
    {
        private bool _isPerformsSkill;

        public Fencer() : base()
        {
            Type = "Фехтовальщик";
            _isPerformsSkill = false;
        }

        public override void TryAttack(Fighter enemy)
        {
            if (_isPerformsSkill == true)
            {
                MakeComboAttack(enemy);
            }
            else
            {
                base.TryAttack(enemy);
            }
        }

        protected override void MakeSpecialAttack(Fighter enemy)
        {
            ShowFullName();
            Console.WriteLine($" начинает серию ударов");
            _isPerformsSkill = true;
            ActionPoints++;
            MakeComboAttack(enemy);
        }

        private void MakeComboAttack(Fighter enemy)
        {
            ShowFullName();
            Console.WriteLine($" выполняет комбо атаку");
            _isPerformsSkill = UserUtils.GenereteRandomBool();

            if (_isPerformsSkill == false)
            {
                ActionPoints--;
                Console.WriteLine($"cерия прервана");
            }

            enemy.TakeDamage(Damage);
        }
    }

    class Flagelant : Fighter
    {
        private int _acceptedAttacks;

        public Flagelant() : base()
        {
            Type = "Флагеллянт";
            _acceptedAttacks = 0;
        }

        public override void TakeDamage(int damage)
        {
            _acceptedAttacks++;
            base.TakeDamage(damage);
        }

        protected override void MakeSpecialAttack(Fighter enemy)
        {
            int healthPointRegen = 1;
            int damage = _acceptedAttacks + Damage;

            ShowFullName();
            Console.WriteLine($" праведная атака");
            Health += healthPointRegen * _acceptedAttacks;
            enemy.TakeDamage(damage);

            if (Health > StatsBase.Health)
            {
                Health = StatsBase.Health;
            }
        }
    }

    class Stats
    {
        public Stats()
        {
            Initiative = GenerateInitiative();
            Health = GenerateHealth();
            Damage = GenerateDamage();
            ActionPoints = 2;
        }

        public int Initiative { get; private set; }
        public int Health { get; private set; }
        public int Damage { get; private set; }
        public int ActionPoints { get; private set; }

        private int GenerateInitiative()
        {
            int maxInitiative = 100;
            int minInitiative = 1;

            return UserUtils.GenereteRandom(minInitiative, maxInitiative);
        }

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

    class DataBase
    {
        private static List<string> s_names;

        static DataBase()
        {
            s_names = CreateNames();
        }

        public static string GetRandomName()
        {
            return s_names[UserUtils.GenereteRandom(0, s_names.Count)];
        }

        private static List<string> CreateNames()
        {
            List<string> names = new List<string>
            {
                "Йегер",
                "Майер",
                "Ходжа",
                "Брахим",
                "Мортенсен",
                "Хубер",
                "Маар"
            };

            return names;
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

            return s_random.Next(valueTrue + 1) == 1;
        }
    }
}
