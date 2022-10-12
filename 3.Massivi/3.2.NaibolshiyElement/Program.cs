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
            int numbersMax = 1000;
            int numbersMin = 1;
            int largestNumber = 0;
            int largestNumberDesignations = 0;

            for (int i = 0; i < numbers.GetLength(0); i++ )
            {
                for (int j = 0; j < numbers.GetLength(1); j++)
                {
                    numbers[i, j] = random.Next(numbersMin, numbersMax);
                    Console.Write(numbers[i, j] + "  ");
                }

                Console.WriteLine();
            }

            foreach (int i in numbers)
            {
                if (i > largestNumber)
                {
                    largestNumber = i;
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
