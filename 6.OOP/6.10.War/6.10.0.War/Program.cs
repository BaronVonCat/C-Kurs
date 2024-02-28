using System;
using System.Collections.Generic;

namespace _6._10.War
{
    internal class Program
    {
        static void Main(string[] args)
        {
            List<NobleHouse> nobleHouses = new List<NobleHouse>
            {
                new NobleHouse("Лейте"),
                new NobleHouse("Гартел")
            };
            War war = new War(nobleHouses);

            war.Work();
        }
    }

    class War
    {
        private List<NobleHouse> _nobleHouses;

        public War(List<NobleHouse> nobleHouses)
        {
            _nobleHouses = nobleHouses;
        }

        public void Work()
        {
            NobleHouse winningNobleHouse = null;

            ShowInfo();
            Console.ReadKey();
            BattleConduct();

            foreach (NobleHouse nobleHouse in _nobleHouses)
            {
                nobleHouse.CheckArmy();

                if (nobleHouse.IsArmyDestroyed == false) 
                {
                    winningNobleHouse = nobleHouse;
                }
            }

            Console.Clear();
            Console.WriteLine($"{winningNobleHouse.Name} одержал победу!");
            Console.ReadKey();
        }

        private void BattleConduct()
        {
            Battle battle;
            List<Squad> squads = new List<Squad>();
 
            foreach (NobleHouse nobleHouse in _nobleHouses)
            {
                squads.Add(nobleHouse.GetSquad());
            }

            battle = new Battle(squads);
            battle.Work();
        }

        private void ShowInfo()
        {
            Console.WriteLine($"Участники конфликта:");
            Console.WriteLine();

            foreach (NobleHouse nobleHouse in _nobleHouses)
            {
                nobleHouse.ShowInfo();
                Console.WriteLine();
            }
        }
    }

    class Battle
    {
        private List<Squad> _squads;
        private List<Fighter> _fighters;

        public Battle(List<Squad> squads)
        {
            _squads = squads;
            _fighters = GetAllFighters();
        }

        public void Work()
        {
            int turn = 0;

            while (_squads.Count > 1)
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
                RemoveDestroyedSquads();
                Console.WriteLine();
                ShowFighters();
                Console.ReadKey();
            }
        }

        

        private void TryMakeMove(Fighter fighter)
        {
            if (fighter.IsDead == false)
            {
                List<Fighter> enemies = FindEnemies(fighter.NameFaction);

                Console.WriteLine();

                while (fighter.ActionPoints > 0 && enemies.Count > 0)
                {
                    Fighter enemy = enemies[UserUtils.GenereteRandom(0, enemies.Count)];
                    fighter.TryAttack(enemy);
                    enemies = FindEnemies(fighter.NameFaction);
                }

                fighter.RestoreActionPoints();
            }
        }

        private List<Fighter> FindEnemies(string nameAlliedTeam)

        {
            List<Fighter> enemies = new List<Fighter>();

            foreach (Fighter fighter in _fighters)
            {
                if (fighter.NameFaction != nameAlliedTeam && fighter.IsDead == false)
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

        private void RemoveDestroyedSquads()
        {
            List<Squad> destroyedSquads = new List<Squad>();

            foreach (Squad squad in _squads)
            {
                squad.CheckIntegritySquad();

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
                Console.WriteLine($"======      {squad.Namefaction}     ======");
                Console.WriteLine();
                squad.ShowFighters();
                Console.WriteLine();
            }
        }

        private List<Fighter> GetAllFighters()
        {
            List<Fighter> fighters = new List<Fighter>();

            foreach (Squad squad in _squads)
            {
                fighters.AddRange(squad.GetFighters());
            }

            return fighters;
        }
    }

    class NobleHouse
    {
        private Squad _squad;

        public NobleHouse(string name)
        {
            Name =  $"Дом {name}";
            _squad = CreateSquad();
            IsArmyDestroyed = false;
        }

        public string Name { get; private set; }

        public bool IsArmyDestroyed { get; private set; }

        public void ShowInfo()
        {
            Console.WriteLine($"========    {Name}    ========");
            Console.WriteLine();
            _squad.ShowFighters();
        }

        public Squad GetSquad() { return _squad; }

        public void CheckArmy()
        {
            if (_squad.IsDestroyed == true)
            {
                IsArmyDestroyed = true;
            }
        }

        private Squad CreateSquad()
        {
            Squad squad;
            List<Fighter> fighters = new List<Fighter>();
            int maxFighters = 3;

            for (int i = 0; i < maxFighters; i++)
            {
                Fighter fighter = CreateFighter();

                fighters.Add(fighter);
            }

            squad = new Squad(fighters, Name);
            return squad;
        }

        private List<Fighter> CreateFigters()
        {
            List<Fighter> fighters = new List<Fighter>();
            int maxFighters = 3;

            for (int i = 0; i < maxFighters; i++)
            {
                Fighter fighter = CreateFighter();

                fighters.Add(fighter);
            }

            return fighters;
        }

        private Fighter CreateFighter()
        {
            List<Fighter> fighters = new List<Fighter>
            {
                new Assassin(Name),
                new Fencer(Name),
                new Flagelant(Name)
            };

            return fighters[UserUtils.GenereteRandom(0, fighters.Count)];
        }
    }

    class Squad
    {
        private List<Fighter> _fighters;

        public Squad(List<Fighter> fighters, string nameFaction)
        {
            _fighters = fighters;
            Namefaction = nameFaction;
        }

        public string Namefaction { get; private set; }

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

        public List<Fighter> GetFighters() { return _fighters; }

        public void CheckIntegritySquad()
        {
            List<Fighter> liveFighters = new List<Fighter>();

            foreach (Fighter fighter in _fighters)
            {
                if (fighter.IsDead == false)
                {
                    liveFighters.Add(fighter);
                }
            }

            if (liveFighters.Count == 0)
            {
                IsDestroyed = true;
            }
        }
    }

    class Fighter
    {
        protected Stats StatsBase;

        public Fighter(string nameFraction)
        {
            StatsBase = new Stats();
            Name = Database.GetRandomName();
            Type = null;
            NameFaction = nameFraction;
            Initiative = StatsBase.Initiative;
            Health = StatsBase.Health;
            Damage = StatsBase.Damage;
            ActionPoints = StatsBase.ActionPoints;
            IsDead = false;
        }

        public bool IsDead { get; protected set; }
        public string Name { get; protected set; }
        public string Type { get; protected set; }
        public string NameFaction { get; protected set; }
        public int Initiative { get; protected set; }
        public int Health { get; protected set; }
        public int Damage { get; protected set; }
        public int ActionPoints { get; protected set; }

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
        public Assassin(string nameFraction) : base(nameFraction)
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

        public Fencer(string nameFraction) : base(nameFraction)
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

        public Flagelant(string nameFraction) : base(nameFraction)
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

    class Database
    {
        public static string GetRandomName()
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