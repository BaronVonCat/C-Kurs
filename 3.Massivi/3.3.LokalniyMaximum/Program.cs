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

            for (int i = 0; i < numbers.Length; i++)
            {
                if (numbers[i] == numbers[0])
                { 
                    if (numbers[i] >= numbers[i + 1])
                    {
                        Console.WriteLine(numbers[i]);
                    }
                }
                else if (i == numbers.Length - 1)
                {
                    if (numbers[i] >= numbers[i - 1])
                    {
                        Console.WriteLine(numbers[i]);
                    }
                }
                else if (numbers[i] != numbers[0] && numbers[i] != numbers.Length)
                {
                    if (numbers[i] >= numbers[i + 1] && numbers[i] >= numbers[i - 1])
                    {
                        Console.WriteLine(numbers[i]);
                    }
                }
            }
        }
    }
}
