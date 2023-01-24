using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _4._3.HochuChislo
{
    internal class Program
    {
        static void Main(string[] args)
        {
            bool hasNumberBeenReceived = false;
            string userInput;
            int userNumber = 0;

            Console.Write("Хочу число: ");

            while (hasNumberBeenReceived == false)
            {
                userInput = Console.ReadLine();
                Console.Clear();
                hasNumberBeenReceived = int.TryParse(userInput, out userNumber);

                if (hasNumberBeenReceived == false)
                {
                    Console.WriteLine("Кожаный, мне нужно число, а ты что ввёл?! Давай по новой.");
                }
            }

            Console.Clear();
            Console.WriteLine($"Ваше число - {userNumber}");
        }
    }
}
