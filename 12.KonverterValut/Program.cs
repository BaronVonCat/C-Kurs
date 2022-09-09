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

            while (true)
            {
                Console.Clear();
                Console.WriteLine("Ваши счета:" +
                "\n Рубли - " + rub + "" +
                "\n Доллары - " + usd + "" +
                "\n Юани - " + cny + "" +
                "\n");
                Console.WriteLine("Какую валюту вы хотите обменять?" +
                    "\n 1.Рубли в доллары" +
                    "\n 2.Рубли в юани" +
                    "\n 3.Длллары в рубли" +
                    "\n 4.Доллары в юани" +
                    "\n 5.Юани в доллары" +
                    "\n 6.Юани в рубли" +
                    "\n 7.Выход");
                userInput = Console.ReadLine();

                if (userInput == exit)
                {
                    break;
                }

                switch (userInput)
                {
                    case "1":
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
                    case "2":
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
                    case "3":
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
                    case "4":
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
                    case "5":
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
                    case "6":
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

                        Console.ReadKey();
                        break;
                    case "7":
                        Console.WriteLine();
                        break;
                }
            }
            Console.Clear();
            Console.WriteLine("Вы вышли из обменника валют.");
        }
    }
}
