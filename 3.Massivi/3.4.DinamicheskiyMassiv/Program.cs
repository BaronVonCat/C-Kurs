using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _3._4.DinamicheskiyMassiv
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int[] userNumbers = new int[0];
            string userInput;
            string commandExit = "exit";
            string commandSum = "sum";
            bool isExitProgram = false;

            while (isExitProgram != true)
            {
                Console.Write("Ранее набранные числа: ");
                foreach (int userNumber in userNumbers)
                {
                    Console.Write(userNumber + " ");
                }

                Console.Write("\nВведите число: ");
                userInput = Console.ReadLine();

                if (userInput == commandExit)
                {
                    isExitProgram = true;
                }
                else if (userInput == commandSum)
                {
                    int sum = 0;

                    foreach (int userNumber in userNumbers)
                    {
                        sum += userNumber; 
                    }

                    Console.WriteLine("Сумма всех ранее перечисленных чисел: " + sum);
                    Console.ReadKey();
                }
                else
                {
                    int[] tempUserNumbers = new int[userNumbers.Length + 1];

                    for (int i = 0; i < userNumbers.Length; i++)
                    {
                        tempUserNumbers[i] = userNumbers[i];
                    }

                    tempUserNumbers[tempUserNumbers.Length - 1] = Convert.ToInt32(userInput);
                    userNumbers = tempUserNumbers;
                }

                Console.Clear();
            }
        }
    }
}
