using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _3._5.PodmassivPovtoreniaChisel
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Random random = new Random();
            int[] numbers = new int[30];
            int numberMin = 0;
            int numberMax = 6;
            int sum = 0;
            int consecutiveNumbers = 1;
            int maximumConsecutiveNumber = 0;

            for (int i = 0; i < numbers.Length; i++)
            {
                numbers[i] = random.Next(numberMin, numberMax);
                Console.Write(numbers[i] + " ");

                if (i == 0)
                {
                    sum++;
                    maximumConsecutiveNumber = numbers[i];
                }
                else if (numbers[i] == numbers[i - 1])
                {
                    sum++;

                    if (sum > consecutiveNumbers)
                    {
                        consecutiveNumbers = sum;
                        maximumConsecutiveNumber = numbers[i];
                    }
                }
                else
                {
                    sum = 1;
                }
            }

            Console.WriteLine("\nЧисло с максимальной последовательностю чисел - "
                + maximumConsecutiveNumber +
                "\nМаксимальная последовательность - " + 
                consecutiveNumbers);
        }
    }
}
