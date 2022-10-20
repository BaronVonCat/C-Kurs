using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Schema;

namespace _3._2.NaibolshiyElement
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Random random =new Random();
            int[,] numbers = new int[10, 10];
            int numberMax = 1000;
            int numberMin = 1;
            int largestNumber = 0;
            int largestNumberDesignations = 0;

            for (int i = 0; i < numbers.GetLength(0); i++ )
            {
                for (int j = 0; j < numbers.GetLength(1); j++)
                {
                    numbers[i, j] = random.Next(numberMin, numberMax);
                    Console.Write(numbers[i, j] + "  ");
                }

                Console.WriteLine();
            }

            foreach (int number in numbers)
            {
                if (number > largestNumber)
                {
                    largestNumber = number;
                }
            }
            
            Console.WriteLine("\nНаибольшее число - " + largestNumber);

            for (int i = 0; i < numbers.GetLength(0); i++)
            {
                for (int j = 0; j < numbers.GetLength(1); j++)
                {
                    if (numbers[i, j] == largestNumber)
                    {
                        numbers[i, j] = largestNumberDesignations;
                    }
                }
            }

            Console.WriteLine();

            for (int i = 0; i < numbers.GetLength(0); i++)
            {
                for (int j = 0; j < numbers.GetLength(1); j++)
                {
                    Console.Write(numbers[i, j] + "  ");
                }

                Console.WriteLine();
            }
        }
    }
}
