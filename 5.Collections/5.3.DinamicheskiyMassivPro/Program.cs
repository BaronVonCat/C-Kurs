using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace _5._3.DinamicheskiyMassivPro
{
    internal class Program
    {
        static void Main(string[] args)
        {
            const string CommandExit = "exit";
            const string CommandSum = "sum";

            List<int> userNumbers = new List<int>();
            bool isExitProgram = false;

            while (isExitProgram == false)
            {
                string userInput;

                OutputNumbersInConsole(userNumbers);
                Console.WriteLine($"\nДоступные команды:" +
                    $"\n{CommandSum} - вывести сумму ранее введённых чисел." +
                    $"\n{CommandExit} - выйти из программы.");
                Console.Write("\nВведите число или команду: ");
                userInput = Console.ReadLine();

                switch (userInput)
                {
                    case CommandExit:
                        isExitProgram = true;
                        break;

                    case CommandSum:
                        ExecuteCommandSum(userNumbers);
                        break;

                    default:
                        userNumbers = CheckAndAddNumber(userNumbers, userInput);
                        break;
                }

                Console.Clear();
            }
        }

        static void OutputNumbersInConsole(List<int> numbers)
        {
            Console.Write("Ранее набранные числа: ");

            foreach (int number in numbers)
            {
                Console.Write(number + " ");
            }

            Console.WriteLine();
        }

        static void ExecuteCommandSum(List<int> numbers)
        {
            int sum = 0;

            foreach (int number in numbers)
            {
                sum += number;
            }

            Console.Write($"Сумма всех ранее введённых чисел: {sum}");
            Console.ReadKey();
        }

        static List<int> CheckAndAddNumber(List<int> numbers, string userInput)
        {
            bool isNumber = false;
            int userNumber = 0;

            isNumber = int.TryParse(userInput, out userNumber);

            if (isNumber == true)
            {
                numbers.Add(userNumber);
            }
            else
            {
                Console.WriteLine("Некорректный запрос!");
                Console.ReadKey();
            }

            return numbers;
        }
    }
}
