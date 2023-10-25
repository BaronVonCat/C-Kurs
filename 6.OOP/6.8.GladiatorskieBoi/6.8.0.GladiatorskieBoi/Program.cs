using System;
using System.Collections.Generic;
using System.Security.Policy;
using System.Xml.Linq;

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
            bool isWork = false;

            while (isWork == false)
            {
                const string CommandExit = "exit";
                const string CommandFight = "fight";
                const string CommandChangeCandidate = "change";
                const string CommandResetCandidates = "reset";
                const string CommandReplenishCandidates = "replenish";

                string userInput;
                int userNumber;
                bool isNumber;

                Console.Clear();
                Console.WriteLine("Укажите номер бойца, чтобы вывести его на арену, " +
                    "\nили текст небоходимой команды.");
                Console.WriteLine("\nКомманды:" +
                    $"\n{CommandExit} - Выход" +
                    $"\n{CommandFight} - Провести бой" +
                    $"\n{CommandChangeCandidate} - Поменять кандидата" +
                    $"\n{CommandResetCandidates} - Поменять всех кандидатов" +
                    $"\n{CommandReplenishCandidates} - Пополнить кандидатов");
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
                            isWork = true;
                            break;

                        case CommandFight:
                            TryCreateFight();
                            break;

                        case CommandChangeCandidate:
                            TryChangeCandidate();
                            break;

                        case CommandResetCandidates:
                            ResetCandidates();
                            break;

                        case CommandReplenishCandidates:
                            ReplenishCandidates();
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
                _fightersCandidates.AddRange(_fighters);
                _fighters.Clear();
            }
            else
            {
                Console.Clear();
                Console.WriteLine("Недостаточно бойцов для боя!");
                Console.ReadKey();
            }
        }

        private void TryChangeCandidate()
        {
            bool isNumber;
            string userInput;
            int userNumber;

            Console.Clear();
            ShowFighters(_fightersCandidates);
            Console.Write("Введите номер кандита чтобы поменять его: ");
            userInput = Console.ReadLine();
            isNumber = int.TryParse(userInput, out userNumber);

            if (isNumber == true)
            {
                if (userNumber <= _fightersCandidates.Count)
                {
                    Fighter fighter = CreateCandidate();

                    _fightersCandidates[userNumber - 1] = fighter;
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine("Такого бойца не существует!");
                    Console.ReadKey();
                }
            }
            else
            {
                Console.Clear();
                Console.WriteLine("Некорректный запрос!");
                Console.ReadKey();
            }
        }

        private void ResetCandidates()
        {
            _fightersCandidates.Clear();
            ReplenishCandidates();
        }

        private void ReplenishCandidates()
        {
            int maxFighters = 5;
            int numberCurrentFighters = _fightersCandidates.Count + _fighters.Count;

            for (int i = numberCurrentFighters; i < maxFighters; i++)
            {
                Fighter fighter = CreateCandidate();

                _fightersCandidates.Add(fighter);
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
            const int ClassNumberNecromancer = 0;
            const int ClassNumberAssassin = 1;
            const int ClassNumberKnight = 2;

            Fighter fighter = null;

            switch (UserUtils.GenereteRandom(0, ClassNumberKnight + 1))
            {
                case ClassNumberNecromancer:
                    fighter = CreateNecromancer();
                    break;

                case ClassNumberAssassin:
                    fighter = CreateAssassin();
                    break;

                case ClassNumberKnight:
                    fighter = CreateKnight();
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

        private Necromancer CreateNecromancer()
        {
            Necromancer necromancer;
            Stats stats;
            Fighter fighter = CreateBasisFighter();
            string className = "Некромант";
            string name = $"{fighter.Name} {className}";
            int manaMax = 150;
            int manaMin = 50;
            int mana = UserUtils.GenereteRandom(manaMin, manaMax);

            stats = new Stats(fighter.Initiative, fighter.Health, fighter.Damage, fighter.ActionPoints);
            necromancer = new Necromancer(stats, name, mana);
            return necromancer;
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

        private Knight CreateKnight()
        {
            Knight knight;
            Stats stats;
            Fighter fighter = CreateBasisFighter();
            string className = "Рыцарь";
            string name = $"{fighter.Name} {className}";
            int armorMax = 50;
            int armorMin = 1;
            int armor = UserUtils.GenereteRandom(armorMin, armorMax);

            stats = new Stats(fighter.Initiative, fighter.Health, fighter.Damage, fighter.ActionPoints);
            knight = new Knight(stats, name, armor);

            return knight;
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
                Console.WriteLine($"=== Ход {turn} ===");
                ResetStats();
                ActivateImpacts();

                while (queueMovementFighters.Count > 0)
                {
                    Fighter currentFighter = queueMovementFighters.Dequeue();

                    TryMakeMove(currentFighter);
                }

                ShowFighters();
                RemoveExpiredImpacts();
                RemoveDeadFigters();
                Console.ReadKey();
            }
        }

        private void TryMakeMove(Fighter currentFighter)
        {
            if (currentFighter.IsDead == false)
            {
                Console.WriteLine($"\n===={currentFighter.Name} ходит");

                while (currentFighter.ActionPoints > 0)
                {
                    //Fighter turgetFighter = FindTarget(currentFighter.TeamNumber);
                    Attack attack = null;

                    currentFighter.MakeAction(ref attack);
                    TryGiveAttackToFighter(turgetFighter, attack);
                }

                Console.WriteLine($"{currentFighter.Name} завершает ход");
            }
        }

        private void TryGiveAttackToFighter(Fighter fighter, Attack attack)
        {
            if (attack is SpecialAttack)
            {
                SpecialAttack specialAttack = (SpecialAttack)attack;
                Effect impact = specialAttack.GetImpact();

                if (impact is Skeleton)
                {
                    Skeleton summonedUndead = (Skeleton)impact;

                    _fighters.AddRange(summonedUndead.GetUndead());
                }
            }
            else if (attack != null)
            {
                fighter.TakeAttack(attack);
            }
        }

        private void ResetStats()
        {
            foreach (Fighter fighter in _fighters)
            {
                fighter.ResetCurrentStats();
            }
        }

        private void ActivateImpacts()
        {
            foreach (Fighter fighter in _fighters)
            {
                fighter.ActivateImpacts();
            }
        }

        private void AddNewFightersToFight(List<Fighter> newFighters)
        {
            foreach (Fighter fighter in newFighters)
            {
                _fighters.Add(fighter);
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

        private void RemoveDeadFigters()
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

        private void RemoveExpiredImpacts()
        {
            foreach (Fighter fighter in _fighters)
            {
                fighter.DeleteExpiredImpacts();
            }
        }

        private void ShowFighters()
        {;

            foreach (Fighter fighter in _fighters)
            {
                fighter.ShowInfo();
                Console.WriteLine();
            }
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

                for (int s = 0; s < _fighters.Count; s++)
                {
                    if (_fighters[s].Initiative > transmittedFighter.Initiative
                        && queueMovementFighters.Contains(_fighters[s]) == false)
                    {
                        transmittedFighter = _fighters[s];
                    }
                }

                queueMovementFighters.Enqueue(transmittedFighter);
            }

            return queueMovementFighters;
        }
    }

    class Fighter
    {
        protected List<Effect> Effects;
        protected Stats StatsBase;

        public Fighter(Stats stats, string name)
        {
            Effects = new List<Effect>();
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

        public virtual void ShowImpacts()
        {
            int sequenseNumber = 0;

            foreach (Effect impact in Effects)
            {
                sequenseNumber++;
                Console.Write($"{sequenseNumber}. ");
                impact.ShowInfo();
            }
        }

        public virtual void ShowInfo() 
        {
            ShowName();
            Console.WriteLine();
            ShowStats();
            Console.WriteLine("--Действующие эффекты--");
            ShowImpacts();
        }

        public virtual void TakeAttack(Attack attack)
        {
            g
        }

        public virtual void MakeAction(out Impact impact)
        {
            impact = CreateAttack();
        }

        public virtual void Die()
        {
            Console.WriteLine($"{Name} погиб!");
            IsDead = true;
        }

        public void SetTeamNumber(int teamNumber)
        {
            TeamNumber = teamNumber;
        }

        public void ResetCurrentStats()
        {
            Initiative = StatsBase.Initiative;
            Damage = StatsBase.Damage;
            ActionPoints = StatsBase.ActionPoints;
        }

        public void ActivateImpacts()
        {
            if (Effects.Count > 0)
            {
                Stats stats = new Stats(StatsBase.Initiative, Health, Damage, StatsBase.ActionPoints);

                foreach (Effect impact in Effects)
                {
                    impact.SkipTimeAction();
                    impact.Activate(stats);

                }

                Initiative = stats.Initiative;
                Health = stats.Health;
                Damage = stats.Damage;
                ActionPoints = stats.ActionPoints;

                if (Health <= 0)
                {
                    IsDead = true;
                }
            }
        }

        public void DeleteExpiredImpacts()
        {
            List<Effect> RemoveImpacts;
            bool hasImpactsExpired = TryFindExpiredImpacts(out RemoveImpacts);

            if (hasImpactsExpired == true)
            {
                foreach (Effect impact in RemoveImpacts)
                {
                    if (impact is Skeleton)
                    {
                        Skeleton summonedUndead = (Skeleton)impact;

                        summonedUndead.DeactivateUndead();
                    }
                    else
                    {
                        Effects.Remove(impact);
                    }
                }
            }
        }

        public void TryTakeImpact(Effect impact)
        {
            if (impact != null)
            {
                Effects.Add(impact);
            }
        }

        protected virtual void TakeDamage(int damage)
        {
            Console.WriteLine($"-{Name} получил урон - {damage}");

            if (Health > damage)
            {
                Health -= damage;
            }
            else
            {
                IsDead = true;
                Console.WriteLine($"-{Name} погиб");
            }
        }

        protected Attack CreateAttack()
        {
            Attack attack;
            string name = "обычная атака";
            int damage = Damage;

            ActionPoints--;
            attack = new Attack(name, damage);

            return attack;
        }

        protected bool TryFindExpiredImpacts(out List<Effect> impacts)
        {
            bool isImpactsFound = false;

            impacts = new List<Effect>();

            foreach (Effect impact in Effects)
            {
                if (impact.TimeAction == 0)
                {
                    impacts.Add(impact);
                    isImpactsFound = true;
                }
            }

            return isImpactsFound;
        }

        protected bool TryFindDefenseImpact(out Effect impactDefense)
        {
            bool isImpactFound = false;

            impactDefense = null;

            foreach (Effect impact in Effects)
            {
                if (impact is BoneDome)
                {
                    BoneDome boneDome = (BoneDome)impact;

                    if (boneDome.HasDestroyed == false)
                    {
                        isImpactFound = true;
                        impactDefense = impact;
                        break;
                    }
                }
            }

            return isImpactFound;
        }
    }

    class Necromancer : Fighter
    {
        private List<Connection> _connections;

        public Necromancer(Stats stats, string name, int mana) :
        base(stats ,name)
        {
            _connections = new List<Connection>();
            ManaBase = mana;
            ManaCurrent = mana;
        }

        public int ManaBase { get; private set; }
        public int ManaCurrent { get; private set; }

        public override void ShowStats()
        {
            base.ShowStats();
            Console.WriteLine($"Мана: {ManaCurrent}/{ManaBase}");
        }

        public override void MakeAction(out Impact impact)
        {
            const int ActionNumberAttack = 0;
            const int ActionNumberSummonUndead = 1;
            const int ActionNumberSummonBoneDome = 2;


            impact = null;

            switch (UserUtils.GenereteRandom(0, ActionNumberSummonBoneDome + 1))
            {
                case ActionNumberAttack:
                    impact = CreateAttack();
                    break;

                case ActionNumberSummonUndead:
                    impact = SummonUndead();
                    break;

                case ActionNumberSummonBoneDome:
                    impact = SummonBoneDome();
                    break;

            }
        }

        public override void Die()
        {
            foreach (Connection connection in _connections)
            {
                connection.Deactivate();
            }

            base.Die();
        }

        private SummonedUndead SummonUndead()
        {
            SummonedUndead summonedUndead = null;
            int actionPointsPrice = 2;
            int manaPrice = 50;

            if (ActionPoints >= actionPointsPrice && ManaCurrent >= manaPrice)
            {
                ConnectionUndead connectionUndead;

                ActionPoints -= actionPointsPrice;
                ManaCurrent -= manaPrice;
                summonedUndead = new SummonedUndead(CreateUndead());
                connectionUndead = new ConnectionUndead(summonedUndead.GetUndead());
                _connections.Add(connectionUndead);
            }

            return summonedUndead;
        }

        private List<Fighter> CreateUndead()
        {
            List<Fighter> fighters = new List<Fighter>();
            int numberFighters = 2;

            for (int i = 0; i < numberFighters; i++)
            {
                fighters.Add(CreateSkeleton());
            }

            return fighters;
        }

        private Skeleton CreateSkeleton()
        {
            Skeleton skeleton;
            Stats stats;
            int initiativeMax = 50;
            int initiative = UserUtils.GenereteRandom(0, initiativeMax);
            int healthMax = 50;
            int health = UserUtils.GenereteRandom(0, healthMax);
            int damageMax = 5;
            int damage = UserUtils.GenereteRandom(1, damageMax);
            int actionPoints = 1;

            stats = new Stats(initiative, health, damage, actionPoints);
            skeleton = new Skeleton(stats);
            return skeleton;
        }

        private SummonedBoneDome SummonBoneDome()
        {
            SummonedBoneDome summonedBoneDome = null;
            int actionPointsPrice = 2;
            int manaPrice = 25;

            if (ActionPoints >= actionPointsPrice && ManaCurrent >= manaPrice)
            {
                ConnectionBoneDome connectionBoneDome = null;

                ActionPoints -= actionPointsPrice;
                ManaCurrent -= manaPrice;
                summonedBoneDome = new SummonedBoneDome();
                connectionBoneDome = new ConnectionBoneDome(summonedBoneDome.GetBoneDome());
                _connections.Add(connectionBoneDome);
            }

            return summonedBoneDome;
        }

        private void TryDeleteConnection()
        {
            List<Connection> connections;
            bool isBrokenConnectionsFound = FindBrokenConnections(out connections);

            if (isBrokenConnectionsFound == true)
            {

            }
        }

        private bool FindBrokenConnections(out List<Connection> connections)
        {
            bool isSearch = false;

            foreach (Connection connection )
            {

            }

            return isSearch;
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

        public override void TakeAttack(Attack attack)
        {
            if (TryDodge() == false)
            {
                base.TakeAttack(attack);
            }
        }

        private bool TryDodge()
        {
            bool hasManageDodge = false;
            int dodgeMax = 100;
            int dodgeMin = 0;

            if (Dodge >= UserUtils.GenereteRandom(dodgeMin, dodgeMax))
            {
                Console.WriteLine($"{Name} уклонился");
                hasManageDodge = true;
            }

            return hasManageDodge;
        }

        protected override SpecialAttack CreateSpecialAttack()
        {
            SpecialAttack specialAttack;
            Effect impact = CreateImpactToTarget();
            string name = "Перерезать артерию";
            int damage = Damage;

            specialAttack = new SpecialAttack(name, damage, impact);
            return specialAttack;
        }

        protected override Effect CreateImpactToTarget()
        {
            Effect impact = new Bleed();

            return impact;
        }

        protected override Effect CreateImpact()
        {
            Effect impact = new IncreasedReflexes();
            return impact;
        }
    }

    class Knight : Fighter
    {
        public Knight(Stats stats, string name, int armor ) :
        base(stats, name) 
        {
            ArmorBase = armor;
            ArmorCurrent = armor;
        }
        
        public int ArmorBase { get; private set; }
        public int ArmorCurrent { get; private set; }

        public override void ShowStats()
        {
            base.ShowStats();
            Console.WriteLine($"Броня: {ArmorCurrent}");
        }

        protected override void TakeDamage(int damage)
        {
            if (ArmorCurrent > 0)
            {
                if (ArmorCurrent > damage)
                {
                    Console.WriteLine($"-урон по броне - {damage}");
                    ArmorCurrent -= damage;
                }
                else
                {
                    int residualDamage = damage - ArmorCurrent;

                    ArmorCurrent = 0;
                    Console.WriteLine($"-броня разрушена!");
                    base.TakeDamage(residualDamage);
                }
            }
            else
            {
                base.TakeDamage(damage);
            }
        }

        protected override SpecialAttack CreateSpecialAttack()
        {
            SpecialAttack specialAttack;
            Effect impact = CreateImpactToTarget();
            string name = "Удар в слабое место";
            int damage = Damage;

            specialAttack = new SpecialAttack(name, damage, impact);
            return specialAttack;
        }

        protected override Effect CreateImpactToTarget()
        {
            Effect impact = new Weakness();

            return impact;
        }

        protected override Effect CreateImpact()
        {
            Effect impact = new Rage();
            return impact;
        }
    }

    class Skeleton : Fighter
    {
        public Skeleton(Stats stats) :
        base(stats, "Скелет") { }

        public override void Die()
        {
            Console.WriteLine($"{Name} рассыпался!");
            IsDead = true;
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

        public Stats GetCopy() 
        { 
            return new Stats(Initiative, Health, Damage, ActionPoints); 
        }
    }

    class Impact 
    {
        public Impact(string name)
        {
            Name = name;
        }

        public string Name { get; private set; }
    }

    class Attack : Impact
    {
        public Attack(string name , int damage) : base (name)
        {
            Damage = damage;
        }

        public int Damage { get; private set; }
    }

    class SpecialAttack : Attack
    {
        private Effect _effect;

        public SpecialAttack(string name, int damage, Effect effect) : base(name, damage)
        {
            _effect = effect;
        }

        public Effect GetImpact()
        {
            return _effect;
        }
    }

    class Buff : Impact
    {
        Effect _effect;

        public Buff(Effect effect, string name) : base(name)
        {
            _effect = effect;
        }
    }

    class SummonedUndead : Impact
    {
        private List<Fighter> _fighters;

        public SummonedUndead(List<Fighter> fighters) : base("Призванная нежить")
        {
            _fighters = fighters;
        }

        public List<Fighter> GetUndead()
        {
            return _fighters;
        }
    }

    class SummonedBoneDome : Impact
    {
        private BoneDome _boneDome; 

        public SummonedBoneDome() : base(null)
        {
            _boneDome = new BoneDome();
        }

        public BoneDome GetBoneDome()
        {
            return _boneDome;
        }
    }

    class Effect
    {
        public Effect(string name)
        {
            Name = name;
        }

        public string Name { get; protected set; }

        public virtual void ShowInfo()
        {
            Console.WriteLine(Name);
        }
    }

    class BoneDome : Effect
    {
        public BoneDome() : base("Костянной купол")
        {
            Health = 50;
            HasDestroyed = false;
        }

        public int Health { get; private set; }
        public bool HasDestroyed { get; private set; }

        public override void ShowInfo()
        {
            base.ShowInfo();
            Console.WriteLine();
            Console.WriteLine($"Здоровье: {Health}");
        }

        public void TakeDamage(int damage)
        {
            Health -= damage;
            Console.WriteLine($"{Name} принял урон - {damage}");

            if (Health <= 0)
            {
                Destroy();
            }
        }

        public void Destroy()
        {
            Console.WriteLine($"{Name} разрушен!");
            HasDestroyed = true;
        }
    }

    class TemporaryEffect : Effect
    {
        public TemporaryEffect(int timeAction, string name) : base(name)
        {
            TimeAction = timeAction;
        }

        public int TimeAction { get; private set; }

        public virtual void Activate(Stats stats) { }

        public override void ShowInfo()
        {
            base.ShowInfo();
            Console.Write($"Время действия: {TimeAction}");
        }

        public void SkipTimeAction()
        {
            TimeAction--;
        }
    }

    class Bleed : TemporaryEffect
    {
        public Bleed() : base(3, "Кровотечение")
        {
            Damage = 5;
            InitiativePenalty = 10;
        }

        public int Damage { get; private set; }
        public int InitiativePenalty { get; private set; }

        public override void Activate(Stats stats)
        {
            stats.Health -= Damage;
            stats.Initiative -= InitiativePenalty;
        }

        public override void ShowInfo()
        {
            base.ShowInfo();
            Console.WriteLine();
            Console.WriteLine($"Урон здоровью за ход: {Damage}");
            Console.WriteLine($"Штраф к инициативе: {InitiativePenalty}");
        }
    }

    class IncreasedReflexes : TemporaryEffect
    {
        public IncreasedReflexes() : base(3, "Повышенные рефлексы")
        {
            InitiativeBonus = 20;
        }

        public int InitiativeBonus { get; private set; }

        public override void Activate(Stats stats)
        {
            stats.Initiative += InitiativeBonus;
        }

        public override void ShowInfo()
        {
            base.ShowInfo();
            Console.WriteLine();
            Console.WriteLine($"Бонус к инициативе: {InitiativeBonus}");
        }
    }

    class Weakness : TemporaryEffect
    {
        public Weakness() : base(3, "Слабость")  
        {
            DamagePenalty = 10;
            InitiativePenalty = 20;
        }

        public int DamagePenalty { get; private set; }
        public int InitiativePenalty { get; private set; }

        public override void Activate(Stats stats)
        {
            stats.Damage -= DamagePenalty;
            stats.Initiative -= InitiativePenalty;
        }

        public override void ShowInfo()
        {
            base.ShowInfo();
            Console.WriteLine();
            Console.WriteLine($"Штраф к урону: {DamagePenalty}");
            Console.WriteLine($"Штраф к Инициативе: {InitiativePenalty}");
        }
    }

    class Rage : TemporaryEffect
    {
        public Rage() : base(3, "Ярость")
        {
            DamageBonus = 10;
            InitiativeBonus = 20;
        }

        public int DamageBonus { get; private set; }
        public int InitiativeBonus { get; private set; }

        public override void Activate(Stats stats)
        {
            stats.Damage += DamageBonus;
            stats.Initiative += InitiativeBonus;
        }

        public override void ShowInfo()
        {
            base.ShowInfo();
            Console.WriteLine();
            Console.WriteLine($"Бонус к урону: {DamageBonus}");
            Console.WriteLine($"Бонус к Инициативе: {InitiativeBonus}");
        }
    }

    class Connection
    {
        public Connection()
        {
            IsConnected = true;
        }

        public bool IsConnected { get; protected set; }

        public virtual void Deactivate()
        {
            IsConnected = false;
        }
    }

    class ConnectionUndead : Connection
    {
        private List<Fighter> _fighters;

        public ConnectionUndead(List<Fighter> fighters)
        {
            _fighters = fighters;
        }

        public override void Deactivate()
        {
            foreach (Fighter fighter in _fighters)
            {
                fighter.Die();
            }

            base.Deactivate();
        }
    }

    class ConnectionBoneDome : Connection
    {
        private BoneDome _boneDome;

        public ConnectionBoneDome(BoneDome boneDome)
        {
            _boneDome = boneDome;
        }

        public override void Deactivate()
        {
            _boneDome.Destroy();
            base.Deactivate();
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
    }
}
