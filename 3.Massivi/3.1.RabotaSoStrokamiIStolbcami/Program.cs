using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace _3._1.RabotaSoStrokamiIStolbcami
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Random random = new Random();
            int numberMax = 10;
            int numberMin = 0;
            int[,] numbers = new int[3, 3];
            int sumOfNumbers = 0;
            int productOfNumbers = 1;

            for (int i = 0; i < numbers.GetLength(0); i++)
            {
                for (int j = 0; j < numbers.GetLength(1); j++)
                {
                    numbers[i, j] = random.Next(numberMin, numberMax);
                    Console.Write(numbers[i, j] + " ");
                }

                Console.WriteLine();
            }

            for (int i = 0; i < numbers.GetLength(1); i++)
            {
                sumOfNumbers += numbers[1, i];
            }

            for (int i = 0; i < numbers.GetLength(0); i++)
            {
                productOfNumbers = productOfNumbers * numbers[i, 0];
            }

            Console.WriteLine("\nСумма второй строки - " + sumOfNumbers);
            Console.WriteLine("Произвдение чисел первого столбца - " + productOfNumbers);
        }
    }
}
