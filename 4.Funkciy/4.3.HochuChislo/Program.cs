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
            int userNumber;

            userNumber = ToRequestNumber();
        }

        static int ToRequestNumber()
        {
            bool hasCheckCompletedSuccessfully = false;
            int number = int.MinValue;

            while (hasCheckCompletedSuccessfully == false)
            {
                bool isNumber;
                string userInput;

                Console.WriteLine("Хочу число: ");
                userInput = Console.ReadLine();
                isNumber = int.TryParse(userInput, out number);

                if (isNumber == true)
                {
                    hasCheckCompletedSuccessfully = true;
                }
                else
                {
                    Console.WriteLine("Кожаный, мне нужно число, а ты что ввёл?! Давай по новой.");
                }
            }

            return number;
        }
    }
}
