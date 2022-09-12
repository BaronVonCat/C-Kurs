using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _13.KoncolnoeMenu
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string userName;
            string userPassword;
            string userInput;
            string userInputMenu = " ";
            const string exit = "5";

            Console.WriteLine("Добро пожаловать в меню приветствия. " +
                "Укажите ваше имя пользователя и пароль.");
            Console.WriteLine("\nИмя пользователя : " +
                "\nПароль: ");
            Console.SetCursorPosition(19, 2);
            userName = userInput = Console.ReadLine();
            Console.SetCursorPosition (8, 3);
            userPassword = userInput = Console.ReadLine();
            Console.Clear();
            Console.WriteLine("Параметры успешно установлены.");

            while (userInputMenu != exit)
            {
                const string CommandChangeUserName = "1";
                const string CommandChangePassword = "2";
                const string CommandSetProgramColor = "3";
                const string CommandOpenCurrencyExchange = "4";

                Console.Clear();
                Console.WriteLine("Имя польователя: " + userName);
                Console.WriteLine("Выберите пункт в меню.\n");
                Console.WriteLine(CommandChangeUserName + " - Изменить имя пользователя." +
                    "\n" + CommandChangePassword + " - Изменить пароль." +
                    "\n" + CommandSetProgramColor + " - Установить цвет программы." +
                    "\n" + CommandOpenCurrencyExchange + " - Открыть обменник валют." +
                    "\n" + exit + " - Выход из программы.");
                userInputMenu = Console.ReadLine();

                switch (userInputMenu)
                {
                    case CommandChangeUserName:
                        Console.Clear();
                        Console.Write("Введите новоё имя пользователя: ");
                        userName = userInput = Console.ReadLine();
                        Console.Clear();
                        Console.WriteLine("Новое имя пользователя установлено");
                        Console.ReadKey();
                        break;
                    case CommandChangePassword:
                        Console.Clear();
                        Console.Write("Введите старый пароль: ");
                        userInput = Console.ReadLine();

                        if (userInput == userPassword)
                        {
                            Console.Write("Введите новый пароль: ");
                            userPassword = userInput = Console.ReadLine();
                            Console.Clear();
                            Console.WriteLine("Установлен новый пароль");
                            Console.ReadKey();
                        }
                        else
                        {
                            Console.WriteLine("Пароль неверен!");
                        }
                        break;
                    case CommandSetProgramColor:
                        const string CommandBackgroundColorRed = "1";
                        const string CommandBackgroundColorGreen = "2";
                        const string CommandBackgroundColorBlue = "3";

                        Console.Clear();
                        Console.WriteLine("Выберите цвет фона программы:\n");
                        Console.WriteLine(CommandBackgroundColorRed + " - Красный.\n"
                            + CommandBackgroundColorGreen + " - Зелёный.\n"
                            + CommandBackgroundColorBlue + " - Синий.");
                        userInput = Console.ReadLine();

                        if (userInput == CommandBackgroundColorRed ||
                            userInput == CommandBackgroundColorGreen ||
                            userInput == CommandBackgroundColorBlue)
                        {
                            switch (userInput)
                            {
                                case CommandBackgroundColorRed:
                                    Console.BackgroundColor = ConsoleColor.Red;
                                    Console.Clear();
                                    Console.WriteLine("Цвет программы изменён на красный");
                                    Console.ReadKey();
                                    break;
                                case CommandBackgroundColorGreen:
                                    Console.BackgroundColor = ConsoleColor.Green;
                                    Console.Clear();
                                    Console.WriteLine("Цвет программы изменён на зелёный");
                                    Console.ReadKey();
                                    break;
                                case CommandBackgroundColorBlue:
                                    Console.BackgroundColor = ConsoleColor.Blue;
                                    Console.Clear();
                                    Console.WriteLine("Цвет программы изменён на синий");
                                    Console.ReadKey();
                                    break;
                            }
                        }
                        else
                        {
                            Console.WriteLine("Некорректный запрос!");
                        }
                        break;
                    case CommandOpenCurrencyExchange:
                        Console.Clear();
                        Console.WriteLine("Чтобы использовать обменник валют" +
                            " введите пароль.\n");
                        Console.WriteLine("Пароль: ");
                        Console.SetCursorPosition(8, 2);
                        userInput = Console.ReadLine();

                        if (userInput == userPassword)
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
                            float currencyCount;
                            string exitCurrencyExchanger = "7";

                            Console.Clear();
                            Console.Write("Введите количество рублей на вашем счету: ");
                            rub = Convert.ToInt32(Console.ReadLine());
                            Console.Clear();
                            Console.Write("Введите количество долларов на вашем счету: ");
                            usd = Convert.ToInt32(Console.ReadLine());
                            Console.Clear();
                            Console.Write("Введите колличество юаней на вашем счету: ");
                            cny = Convert.ToInt32(Console.ReadLine());
                            userInput = Console.ReadLine();

                            while (userInput != exitCurrencyExchanger)
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
                                    "\n" + exitCurrencyExchanger + ".Выход");
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
                        else
                        {
                            Console.WriteLine("Пароль неверен!");
                        }
                        break;
                }
            }
            Console.Clear();
            Console.WriteLine("Вы вышли из программы.");
        }
    }
}
