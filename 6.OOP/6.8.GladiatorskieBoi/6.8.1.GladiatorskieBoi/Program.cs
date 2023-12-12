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
        List<Fighter> _fighters;
        List<Fighter> _fightersCandidates;

        public Arena()
        {
            _fighters = new List<Fighter>();
            _fightersCandidates = CreateCandidates();
        }

        public void Work()
        {
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
                ShowFighters(_fighters);
                userInput = Console.ReadLine();
                isNumber = int.TryParse(userInput, out userNumber);

                if (isNumber == true)
                {
                    TryTransferCandidateToFighters(userNumber - 1);
                }
                else
                {
                    switch (userInput)
                    {
                        case CommandExit:
                            isWork = false;
                            break;

                        case CommandFight:
                            TryCreateFight();
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
                fighter.ShowName();
                Console.WriteLine();
                fighter.ShowStats();
                Console.WriteLine();
            }
        }

        private void TryTransferCandidateToFighters(int index)
        {
            if (_fighters.Count <= 1)
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
                    _fighters.Add(fighter);
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

        private void TryCreateFight()
        {
            if (_fighters.Count > 1)
            {
                Fight fight;
                int teamNumber = 0;

                foreach (Fighter fighter in _fighters)
                {
                    teamNumber++;
                    fighter.SetTeamNumber(teamNumber); 
                }

                fight = new Fight(_fighters);
                fight.Start();
                _fighters.Clear();
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
            const int ClassNumberAssassin = 0;
            const int ClassNumberFancer = 1;
            const int ClassNumberFlagelant = 2;

            Fighter fighter = null;

            switch (UserUtils.GenereteRandom(0, ClassNumberFlagelant + 1))
            {
                case ClassNumberAssassin:
                    fighter = CreateAssassin();
                    break;

                case ClassNumberFancer:
                    fighter = CreateFancer();
                    break;

                case ClassNumberFlagelant:
                    fighter = CreateFlagelant();
                    break;
            }

            return fighter;
        }

        private Fighter CreateBasisFighter()
        {
            Fighter fighter;
            Stats stats;
            string name = CreateName();
            int initiativeMax = 100;
            int initiativeMin = 1;
            int healthMax = 150;
            int healthMin = 50;
            int damageMax = 20;
            int damageMin = 1;
            int initiative = UserUtils.GenereteRandom(initiativeMin, initiativeMax);
            int health = UserUtils.GenereteRandom(healthMin, healthMax);
            int damage = UserUtils.GenereteRandom(damageMin, damageMax);
            int actionPoints = 2;

            stats = new Stats(initiative, health, damage, actionPoints);
            fighter = new Fighter(stats, name);
            return fighter;
        }

        private Assassin CreateAssassin()
        {
            Assassin assassin;
            Stats stats;
            Fighter fighter = CreateBasisFighter();
            string className = "Ассасин";
            string name = $"{fighter.Name} {className}";
            int dodgeMax = 50;
            int dodgeMin = 1;
            int dodge = UserUtils.GenereteRandom(dodgeMin, dodgeMax);

            stats = new Stats(fighter.Initiative, fighter.Health, fighter.Damage, fighter.ActionPoints);
            assassin = new Assassin(stats, name, dodge);
            return assassin;
        }

        private Fencer CreateFancer()
        {
            Fencer fancer;
            Stats stats;
            Fighter fighter = CreateBasisFighter();
            string className = "Фехтовальщик";
            string name = $"{fighter.Name} {className}";

            stats = new Stats(fighter.Initiative, fighter.Health, fighter.Damage, fighter.ActionPoints);
            fancer = new Fencer(stats, name);

            return fancer;
        }

        private Flagelant CreateFlagelant()
        {
            Flagelant flagelant;
            Stats stats;
            Fighter fighter = CreateBasisFighter();
            string className = "Флагелянт";
            string name = $"{fighter.Name} {className}";

            stats = new Stats(fighter.Initiative, fighter.Health, fighter.Damage, fighter.ActionPoints);
            flagelant = new Flagelant(stats, name);

            return flagelant;
        }

        private string CreateName()
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
            
            return names[UserUtils.GenereteRandom(0, names.Count)];
        }
    }

    class Fight
    {
        private List<Fighter> _fighters;

        public Fight(List<Fighter> fighters) 
        {
            _fighters = fighters;
        }

        public void Start()
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

            Console.WriteLine($"\n{_fighters[0].Name} одержал победу!!!");
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

        private void TryMakeMove(Fighter currentFighter)
        {
            if (currentFighter.IsDead == false)
            {
                Console.WriteLine();

                while (currentFighter.ActionPoints > 0)
                {
                    Fighter turgetFighter;
                    int damage = 0;

                    currentFighter.MakeAction(out damage);

                    if (damage > 0)
                    {
                        turgetFighter = FindTarget(currentFighter.TeamNumber);

                        if (turgetFighter != null)
                        {
                            turgetFighter.TakeDamage(damage);
                        }
                    }
                }

                currentFighter.RestoreActionPoints();
            }
        }

        private Fighter FindTarget(int friendlyTeamNumber)
        {
            List<Fighter> fightersEnemyTeams = new List<Fighter>();
            Fighter turgetFighter = null;

            foreach (Fighter fighter in _fighters)
            {
                if (fighter.TeamNumber != friendlyTeamNumber && fighter.IsDead == false)
                {
                    fightersEnemyTeams.Add(fighter);
                }
            }

            if (fightersEnemyTeams.Count > 0)
            {
                turgetFighter = fightersEnemyTeams[UserUtils.GenereteRandom(0, fightersEnemyTeams.Count)];
            }

            return turgetFighter;
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

        public Fighter(Stats stats, string name)
        {
            StatsBase = stats;
            Name = name;
            Initiative = stats.Initiative;
            Health = stats.Health;
            Damage = stats.Damage;
            ActionPoints = stats.ActionPoints;
            IsDead = false;
        }

        public bool IsDead { get; protected set; } 
        public string Name { get; protected set; }
        public int Initiative {  get; protected set; }
        public int Health { get; protected set; }
        public int Damage { get; protected set; }
        public int ActionPoints { get; protected set; }
        public int TeamNumber { get; protected set; }

        public virtual void ShowName()
        {
            Console.Write($"--- {Name} ---");
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

        public virtual void ShowInfo() 
        {
            ShowName();
            Console.WriteLine();
            ShowStats();
            Console.WriteLine("--Действующие эффекты--");
        }

        public void SetTeamNumber(int teamNumber)
        {
            TeamNumber = teamNumber;
        }

        public virtual void MakeAction(out int damage)
        {
            const int ActionNumberAttack = 0;
            const int ActionNumberSpecialSkill = 1;

            damage = 0;

            switch (UserUtils.GenereteRandom(0, ActionNumberSpecialSkill + 1))
            {
                case ActionNumberAttack:
                    damage = Attack();
                    break;

                case ActionNumberSpecialSkill:
                    damage = TryMakeSpecialSkill();
                    break;
            }
        }

        public virtual void TakeDamage(int damage)
        {
            Console.WriteLine($"    {Name} получил урон - {damage}");

            if (Health > damage)
            {
                Health -= damage;
            }
            else
            {
                Die();
            }
        }

        public virtual void Die()
        {
            Console.WriteLine($"{Name} погиб!");
            Health = 0;
            IsDead = true;
        }

        public void RestoreActionPoints()
        {
            ActionPoints = StatsBase.ActionPoints;
        }

        protected int Attack()
        {
            Console.WriteLine($"{Name} обычная атака");
            ActionPoints--;
            return Damage;
        }

        protected int TryMakeSpecialSkill()
        {
            int damage = 0;
            int actionPointsPrice = 2;

            if (ActionPoints >= actionPointsPrice)
            {
                ActionPoints -= actionPointsPrice;
                damage = MakeSpecialSkill();
            }

            return damage;
        }

        protected virtual int MakeSpecialSkill()
        {
            Console.WriteLine($"{Name} применяет специальный навык");
            return Damage;
        }
    }

    class Assassin : Fighter
    {
        private int Dodge;

        public Assassin(Stats stats, string name, int dodge) :
        base(stats, name) 
        {
            Dodge = dodge;
        }

        public override void ShowStats()
        {
            base.ShowStats();
            Console.WriteLine($"Уклонение: {Dodge}");
        }

        protected override int MakeSpecialSkill()
        {
            int damage = 0;

            if (UserUtils.GenereteRandomBool() == true)
            {
                int damageMultiplier = 3;

                Console.WriteLine($"{Name} наносит критический удар");
                damage = Damage * damageMultiplier;
            }
            else
            {
                Console.WriteLine($"{Name} задел ударом");
                damage = Damage;
            }

            return damage;
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
    }

    class Fencer : Fighter
    {
        private bool _isPerformsSkill;

        public Fencer(Stats stats, string name) :
        base(stats, name) 
        {
            _isPerformsSkill = false;
        }

        public override void MakeAction(out int damage)
        {
            if (_isPerformsSkill == true)
            {
                damage = SpecialAttack();
            }
            else
            {
                base.MakeAction(out damage);
            }
        }

        protected override int MakeSpecialSkill()
        {
            Console.WriteLine($"{Name} начинает серию ударов");
            _isPerformsSkill = true;
            ActionPoints++;

            return SpecialAttack();
        }

        private int SpecialAttack()
        {
            Console.WriteLine($"{Name} выполняет комбо атаку");
            _isPerformsSkill = UserUtils.GenereteRandomBool();

            if (_isPerformsSkill == false)
            {
                ActionPoints--;
                Console.WriteLine($"cерия прервана");
            }

            return Damage;
        }
    }

    class Flagelant : Fighter
    {
        private int _acceptedAttacks;

        public Flagelant(Stats stats, string name) : base(stats, name) 
        {
            _acceptedAttacks = 0;
        }

        public override void TakeDamage(int damage)
        {
            _acceptedAttacks++;
            base.TakeDamage(damage);
        }

        protected override int MakeSpecialSkill()
        {
            int healthPointRegen = 1;

            Console.WriteLine($"{Name} восстановил утраченное здоровье");
            Health += healthPointRegen * _acceptedAttacks;

            if (Health > StatsBase.Health)
            {
                Health = StatsBase.Health;
            }

            return 0;
        }
    }

    class Stats
    {
        public int Initiative;
        public int Health;
        public int Damage;
        public int ActionPoints;

        public Stats(int initiative, int health, int damage, int actionPoints)
        {
            Initiative = initiative;
            Health = health;
            Damage = damage;
            ActionPoints = actionPoints;
        }
    }

    class UserUtils
    {
        private static Random Random = new Random();

        public static int GenereteRandom(int minValue, int maxValue)
        {
            int randomNumber = Random.Next(minValue, maxValue);
            return randomNumber;
        }

        public static bool GenereteRandomBool()
        {
            bool result = false;
            int valueTrue = 1;
            int valueBool = Random.Next(0, valueTrue + 1);

            if (valueBool == valueTrue)
            {
                result = true;
            }

            return result;
        }
    }
}
