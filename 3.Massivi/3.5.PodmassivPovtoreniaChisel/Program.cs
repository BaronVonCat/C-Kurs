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
            int numberMax = 2;
            int numericalSequence = 1;
            int numericalSequenceMax = 1;
            int maximumConsecutiveNumber = 0;

            for (int i = 0; i < numbers.Length; i++)
            {
                numbers[i] = random.Next(numberMin, numberMax);
                Console.Write(numbers[i] + " ");
            }

            maximumConsecutiveNumber = numbers[0];

            for (int i = 1; i < numbers.Length; i++)
            {
                if (numbers[i] == numbers[i - 1])
                {
                    numericalSequence++;

                    if (numericalSequence > numericalSequenceMax)
                    {
                        numericalSequenceMax = numericalSequence;
                        maximumConsecutiveNumber = numbers[i];
                    }
                }
                else
                {
                    numericalSequence = 1;
                }
            }

            Console.WriteLine("\nЧисло с максимальной последовательностю чисел - "
                + maximumConsecutiveNumber +
                "\nМаксимальная последовательность - " +
                numericalSequenceMax);
        }
    }
}
