using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _3._3.LokalniyMaximum
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Random random = new Random();
            int[] numbers = new int[10];
            int numberMax = 100;
            int numberMin = 0;

            for (int i = 0; i < numbers.Length; i++)
            {
                numbers[i] = random.Next(numberMin, numberMax);
                Console.Write(numbers[i] + " ");
            }

            Console.WriteLine("\nЛокальные максимумы:");            
            
            if (numbers[0] >= numbers[1])
            {
                    Console.WriteLine(numbers[0]);
            }

            for (int i = 1; i < numbers.Length - 1; i++)
            {
                if (numbers[i] >= numbers[i - 1] && numbers[i] >= numbers[i + 1])
                {
                        Console.WriteLine(numbers[i]);
                }
            }
            
            if (numbers[numbers.Length - 1] > numbers[numbers.Length - 2])
            {
                    Console.WriteLine(numbers[numbers.Length - 1]);
            }
        }
    }
}
