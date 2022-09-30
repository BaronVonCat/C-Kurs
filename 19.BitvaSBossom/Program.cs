using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _19.BitvaSBossom
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int userHealth = 100;
            const string shacklesOfDeadSpirits = "1";
            int recoilOfTheSpell = 30;
            int spellEffect = 3;
            int movesInactivity = 0;
            const string SummonBanshee = "2";
            bool bansheeIsSummoned = false;
            const string howlingBanshee = "3";
            const string charmOfTheDead = "4";
            int swampTrollHealth = 1000;
            string userInput;

            Console.WriteLine("Вы, тёмный призыватель душ, на службе у местного властителя, " +
                "\nкоторый выдал вам поручение - уничтожить болотного тролля, " +
                "\nчто находится недалеко от его владений.");
            Console.ReadKey();
            Console.WriteLine("\nДобравшись до предположительного места обитания чудища," +
                " \nвы проводите разведку и находите пещеру," +
                " \nкоторая, вероятно, и является логовом бестии.");
            Console.ReadKey();
            Console.WriteLine("\nПодготовившись, вы решаетесь зайти внутрь.");
            Console.ReadKey();
            Console.WriteLine("\nТролль, видимо учуявший незванного гостя, уже поджидал вас," +
                " \nчто не оставило вам другого выбора, как вступить в бой.");
            Console.ReadKey();
            Console.Clear();
            
            while (swampTrollHealth > 0 && userHealth > 0)
            {
                bool userSpell;
                Random randomHowlingBansheeDamage = new Random();
                int howlingBansheeDamage;
                int howlingBansheeDamageMin = 100;
                int howlingBansheeDamageMax = 400;
                Random randomCharmOfTheDead = new Random();
                int charmOfTheDeadHealing;
                int charmOfTheDeadHealingMin = 10;
                int charmOfTheDeadHealingMax = 50;

                howlingBansheeDamage = randomHowlingBansheeDamage.Next(howlingBansheeDamageMin, howlingBansheeDamageMax);
                charmOfTheDeadHealing = randomCharmOfTheDead.Next(charmOfTheDeadHealingMin, charmOfTheDeadHealingMax);
                userSpell = false;

                while (userSpell != true)
                {
                    Console.WriteLine("Ваше здоровье - " + userHealth +
                        "\nЗдоровье болтного тролля - " + swampTrollHealth);
                    Console.WriteLine("\nВаши заклинания: " +
                        "\n " + shacklesOfDeadSpirits + ".Оковы мёртвых духов - " +
                        "обездвиживает противника на " + spellEffect + " хода," +
                        " за счёт " + recoilOfTheSpell + " здоровья пользователя." +
                        "\n " + SummonBanshee + ".Призыв банши - " +
                        "призывает астрального духа для атаки. Невозможно ввыполнить если вас атакуют." +
                        "\n " + howlingBanshee + ".Крик банши - " +
                        "наносит от " + howlingBansheeDamageMin + " до " +
                        howlingBansheeDamageMax + " урона противнику." +
                        "\n " + charmOfTheDead + ".Очарование мёртвых - " +
                        "восстанавливет от " + charmOfTheDeadHealingMin + " до " + charmOfTheDeadHealingMax + " здоровья, если есть банши.\n");
                    userInput = Console.ReadLine();
                    Console.Clear();

                    switch (userInput)
                    {
                        case shacklesOfDeadSpirits:
                            if (userHealth <= recoilOfTheSpell)
                            {
                                Console.WriteLine("Ваши жизненные силы на исходе! " +
                                    "Вы не в состоянии выполнить это заклинание, выберите другое.");
                            }
                            else
                            {
                                userHealth -= recoilOfTheSpell;
                                movesInactivity = spellEffect;
                                Console.WriteLine("Противник обездвижен!");
                                userSpell = true;
                            }
                            break;
                        case SummonBanshee:
                            if (bansheeIsSummoned == false)
                            {
                                if (movesInactivity > 0)
                                {
                                    bansheeIsSummoned = true;
                                    Console.WriteLine("Вы призвали духа - Банши.");
                                    userSpell = true;
                                }
                                else
                                {
                                    Console.WriteLine("Вы находитесь под атакой, " +
                                        "в этих условиях невозможно призвать духа." +
                                        "\nПопробуйте использовать другое заклинание.");
                                }
                            }
                            else
                            {
                                Console.WriteLine("Вы уже призвали банши, используйте другое заклинание.");
                            }
                            break;
                        case howlingBanshee:
                            if (bansheeIsSummoned == true)
                            {
                                Random random = new Random();

                                swampTrollHealth -= howlingBansheeDamage;
                                Console.WriteLine("Вопль банши заставляет тролля терпеть урон в размере - "
                                    + howlingBansheeDamage);
                                userSpell = true;
                            }
                            else
                            {
                                Console.WriteLine("Для этого заклинания требуется призвать банши.");
                            }
                            break;
                        case charmOfTheDead:

                            if (bansheeIsSummoned == true) 
                            {
                                userHealth += charmOfTheDeadHealing;
                                Console.WriteLine("Банши применила на вас очарование мёртвых, вы чувствуете себя лучше." +
                                    "\nВосстановленное здоровье - " + charmOfTheDeadHealing);
                                userSpell = true;
                            }
                            else
                            {
                                Console.WriteLine("Для этого заклинания требуется призвать банши.");
                            }
                            break;
                    }

                    Console.WriteLine();
                }

                if (movesInactivity > 0)
                {
                    movesInactivity--;

                    if (movesInactivity == 0)
                    {
                        Console.WriteLine("Оковы мёртвых духов пали! Тролля больше ничего не сдерживает.");
                    }
                    else
                    {
                        Console.WriteLine("Тролль пытается выбраться из оков. " +
                            "Ходов до разрушения заклинания - " + movesInactivity);
                    }
                }
                else
                {
                    Random random = new Random();
                    int swampTrollDamage;
                    int swampTrollDamageMin = 20;
                    int swampTrollDamageMax = 80;

                    swampTrollDamage = random.Next(swampTrollDamageMin, swampTrollDamageMax);
                    userHealth -= swampTrollDamage;
                    Console.WriteLine("Тролль наносит вам " + swampTrollDamage + " урона.");
                }

                if (swampTrollHealth <= 500)
                {
                    Random random = new Random();
                    int swampTrollHealthRegeneration;
                    int healthRegenerationMin = 50;
                    int healthRegenerationMax = 150;

                    swampTrollHealthRegeneration = random.Next(healthRegenerationMin, healthRegenerationMax);
                    swampTrollHealth += swampTrollHealthRegeneration;
                    Console.WriteLine("Регенерация тролля - " + swampTrollHealthRegeneration);
                }
                Console.WriteLine();
            }

            Console.WriteLine();

            if (userHealth <= 0 && swampTrollHealth <= 0)
            {
                Console.WriteLine("Вам удалось убить тролля, хоть и ценой своей жизни.");
            }
            else if (swampTrollHealth <= 0)
            {
                Console.WriteLine("Вы убили болотного тролля! Владыка будет доволен вами.");
            }
            else if (userHealth <= 0)
            {
                Console.WriteLine("Вы погибли жалкой смертью от рук болотного тролля.");
            }
        }
    }
}
