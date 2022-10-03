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
            Random random = new Random();
            int userHealth = 100;
            int recoilOfTheSpell = 30;
            int spellEffect = 3;
            int movesInactivity = 0;
            bool isTheBansheeSummoned = false;
            int swampTrollHealth = 1000;
            int healthThresholdForRegeneration = 500;
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
                bool hasSpellUsed;
                int howlingBansheeDamage;
                int howlingBansheeDamageMin = 100;
                int howlingBansheeDamageMax = 400;
                int charmOfTheDeadHealing;
                int charmOfTheDeadHealingMin = 10;
                int charmOfTheDeadHealingMax = 50;

                howlingBansheeDamage = random.Next(howlingBansheeDamageMin, howlingBansheeDamageMax);
                charmOfTheDeadHealing = random.Next(charmOfTheDeadHealingMin, charmOfTheDeadHealingMax);
                hasSpellUsed = false;

                while (hasSpellUsed == false)
                {
                    const string ShacklesOfDeadSpirits = "1";
                    const string SummonBanshee = "2";
                    const string HowlingBanshee = "3";
                    const string CharmOfTheDead = "4";

                    Console.WriteLine("Ваше здоровье - " + userHealth +
                        "\nЗдоровье болтного тролля - " + swampTrollHealth);
                    Console.WriteLine("\nВаши заклинания: " +
                        "\n " + ShacklesOfDeadSpirits + ".Оковы мёртвых духов - " +
                        "обездвиживает противника на " + spellEffect + " хода," +
                        " за счёт " + recoilOfTheSpell + " здоровья пользователя." +
                        "\n " + SummonBanshee + ".Призыв банши - " +
                        "призывает астрального духа для атаки. Невозможно ввыполнить если вас атакуют." +
                        "\n " + HowlingBanshee + ".Крик банши - " +
                        "наносит от " + howlingBansheeDamageMin + " до " +
                        howlingBansheeDamageMax + " урона противнику." +
                        "\n " + CharmOfTheDead + ".Очарование мёртвых - " +
                        "восстанавливет от " + charmOfTheDeadHealingMin + " до " + charmOfTheDeadHealingMax + " здоровья, если есть банши.\n");
                    userInput = Console.ReadLine();
                    Console.Clear();

                    switch (userInput)
                    {
                        case ShacklesOfDeadSpirits:
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
                                hasSpellUsed = true;
                            }
                            break;
                        case SummonBanshee:
                            if (isTheBansheeSummoned == false)
                            {
                                if (movesInactivity > 0)
                                {
                                    isTheBansheeSummoned = true;
                                    Console.WriteLine("Вы призвали духа - Банши.");
                                    hasSpellUsed = true;
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
                        case HowlingBanshee:
                            if (isTheBansheeSummoned == true)
                            {
                                swampTrollHealth -= howlingBansheeDamage;
                                Console.WriteLine("Вопль банши заставляет тролля терпеть урон в размере - "
                                    + howlingBansheeDamage);
                                hasSpellUsed = true;
                            }
                            else
                            {
                                Console.WriteLine("Для этого заклинания требуется призвать банши.");
                            }
                            break;
                        case CharmOfTheDead:

                            if (isTheBansheeSummoned == true) 
                            {
                                userHealth += charmOfTheDeadHealing;
                                Console.WriteLine("Банши применила на вас очарование мёртвых, вы чувствуете себя лучше." +
                                    "\nВосстановленное здоровье - " + charmOfTheDeadHealing);
                                hasSpellUsed = true;
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
                    int swampTrollDamage;
                    int swampTrollDamageMin = 20;
                    int swampTrollDamageMax = 80;

                    swampTrollDamage = random.Next(swampTrollDamageMin, swampTrollDamageMax);
                    userHealth -= swampTrollDamage;
                    Console.WriteLine("Тролль наносит вам " + swampTrollDamage + " урона.");
                }

                if (swampTrollHealth <= healthThresholdForRegeneration)
                {
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
            else if (userHealth <= 0)
            {
                Console.WriteLine("Вы погибли жалкой смертью от рук болотного тролля.");
            }
            else
            {
                Console.WriteLine("Вы убили болотного тролля! Владыка будет доволен вами.");
            }
        }
    }
}
