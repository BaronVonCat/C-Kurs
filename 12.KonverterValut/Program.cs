using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _12.KonverterValut
{
    internal class Program
    {
        static void Main(string[] args)
        {
            float rub;
            float rubToUsd = 0.016447f;
            float rubToCny = 0.11445f;
            float usd;
            float usdToRub = 60.8f;
            float usdToCny = 6.96f;
            float cny;
            float cnyToUsd = 0.14375f;
            float cnyToRub = 8.74f;
            string userInput;
            float currencyCount;
            string exit = "7";

            Console.Write("Введите количество рублей на вашем счету: ");
            rub = Convert.ToInt32(Console.ReadLine());
            Console.Clear();
            Console.Write("Введите количество долларов на вашем счету: ");
            usd = Convert.ToInt32(Console.ReadLine());
            Console.Clear();
            Console.Write("Введите колличество юаней на вашем счету: ");
            cny = Convert.ToInt32(Console.ReadLine());
            userInput = Console.ReadLine();

            while (userInput != exit)
            {
                const string CommandRubToUsd = "1";
                const string CommandRubToCny = "2";
                const string CommandUsdToRub = "3";
                const string CommandUsdToCny = "4";
                const string CommandCnyToUsd = "5";
                const string CommandCnyToRub = "6";

                Console.Clear();
                Console.WriteLine("Ваши счета:" +
                "\n Рубли - " + rub + "" +
                "\n Доллары - " + usd + "" +
                "\n Юани - " + cny + "" +
                "\n");
                Console.WriteLine("Какую валюту вы хотите обменять?" +
                    "\n" + CommandRubToUsd + ".Рубли в доллары" +
                    "\n" + CommandRubToCny + ".Рубли в юани" +
                    "\n" + CommandUsdToRub + ".Длллары в рубли" +
                    "\n" + CommandUsdToCny + ".Доллары в юани" +
                    "\n" + CommandCnyToUsd + ".Юани в доллары" +
                    "\n" + CommandCnyToRub + ".Юани в рубли" +
                    "\n" + exit + ".Выход");
                userInput = Console.ReadLine();

                switch (userInput)
                {
                    case CommandRubToUsd:
                        Console.WriteLine("Рубли в доллары");
                        Console.Write("Введите колличество валюты для обмена: ");
                        currencyCount = Convert.ToSingle(Console.ReadLine());

                        if (currencyCount <= rub)
                        {
                            usd += currencyCount * rubToUsd;
                            rub -= currencyCount;
                            Console.WriteLine("Транзакция произведена");
                        }
                        else
                        {
                            Console.WriteLine("Некорректный запрос.");
                        }

                        Console.ReadKey();
                        break;
                    case CommandRubToCny:
                        Console.WriteLine("Рубли в юани");
                        Console.Write("Введите колличество валюты для обмена: ");
                        currencyCount = Convert.ToSingle(Console.ReadLine());

                        if (currencyCount <= rub)
                        {
                            cny += currencyCount * rubToCny;
                            rub -= currencyCount;
                            Console.WriteLine("Транзакция произведена");
                        }
                        else
                        {
                            Console.WriteLine("Некорректный запрос.");
                        }

                        Console.ReadKey();
                        break;
                    case CommandUsdToRub:
                        Console.WriteLine("Доллары в рубли");
                        Console.Write("Введите колличество валюты для обмена: ");
                        currencyCount = Convert.ToSingle(Console.ReadLine());

                        if (currencyCount <= usd)
                        {
                            rub += currencyCount * usdToRub;
                            usd -= currencyCount;
                            Console.WriteLine("Транзакция произведена");
                        }
                        else
                        {
                            Console.WriteLine("Некорректный запрос.");
                        }

                        Console.ReadKey();
                        break;
                    case CommandUsdToCny:
                        Console.WriteLine("Доллары в юани");
                        Console.Write("Введите колличество валюты для обмена: ");
                        currencyCount = Convert.ToSingle(Console.ReadLine());

                        if (currencyCount <= usd)
                        {
                            cny += currencyCount * usdToCny;
                            usd -= currencyCount;
                            Console.WriteLine("Транзакция произведена");
                        }
                        else
                        {
                            Console.WriteLine("Некорректный запрос.");
                        }

                        Console.ReadKey();
                        break;
                    case CommandCnyToUsd:
                        Console.WriteLine("Юани в доллары");
                        Console.Write("Введите колличество валюты для обмена: ");
                        currencyCount = Convert.ToSingle(Console.ReadLine());

                        if (currencyCount <= cny)
                        {
                            usd += currencyCount * cnyToUsd;
                            cny -= currencyCount;
                            Console.WriteLine("Транзакция произведена");
                        }
                        else
                        {
                            Console.WriteLine("Некорректный запрос.");
                        }

                        Console.ReadKey();
                        break;
                    case CommandCnyToRub:
                        Console.WriteLine("Юани в рубли");
                        Console.Write("Введите колличество валюты для обмена: ");
                        currencyCount = Convert.ToSingle(Console.ReadLine());

                        if (currencyCount <= cny)
                        {
                            rub += currencyCount * cnyToRub;
                            cny -= currencyCount;
                            Console.WriteLine("Транзакция произведена");
                        }
                        else
                        {
                            Console.WriteLine("Некорректный запрос.");
                        }
                        break;
                }
            }
            Console.Clear();
            Console.WriteLine("Вы вышли из обменника валют.");
        }
    }
}
