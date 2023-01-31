using System;

namespace _4._3.HochuChislo
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int userNumber;

            userNumber = GetNumber();
            Console.WriteLine($"Ваше число - {userNumber}");
        }

        static int GetNumber()
        {
            bool hasNumberBeenReceived = false;
            string userInput;
            int number = 0;

            while (hasNumberBeenReceived == false)
            {
                Console.Write("Хочу число: ");
                userInput = Console.ReadLine();
                Console.Clear();
                hasNumberBeenReceived = int.TryParse(userInput, out number);

                if (hasNumberBeenReceived == false)
                {
                    Console.WriteLine("Кожаный, мне нужно число, а ты что ввёл?! Давай по новой.");
                }
            }

            return number;
        }
    }
}
