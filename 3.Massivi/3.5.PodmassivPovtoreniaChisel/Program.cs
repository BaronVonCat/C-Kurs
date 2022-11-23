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
            int numberOfNumbers = 30;
            int[] numbers = new int[numberOfNumbers];
            int numberMin = 0;
            int numberMax = 3;
            int numericalSequence = 1;
            int numericalSequenceMax = 1;
            int[] maximumConsecutiveNumbers = new int[0];

            for (int i = 0; i < numbers.Length; i++)
            {
                numbers[i] = random.Next(numberMin, numberMax);
                Console.Write(numbers[i] + " ");
            }

            for (int i = 1; i < numbers.Length; i++)
            {
                if (numbers[i] == numbers[i - 1])
                {
                    numericalSequence++;

                    if (numericalSequence > numericalSequenceMax)
                    {
                        numericalSequenceMax = numericalSequence;
                    }
                }
                else
                {
                    numericalSequence = 1;
                }
            }

            numericalSequence = 1;

            for (int i = 1; i < numbers.Length; i++)
            {
                if (numbers[i] == numbers[i - 1])
                {
                    numericalSequence++;

                    if (numericalSequence == numericalSequenceMax)
                    {
                        bool isTheMaximumConsecutiveNumberRepeated = false;

                        for (int j = 0; j < maximumConsecutiveNumbers.Length; j++)
                        {
                            if (maximumConsecutiveNumbers[j] == numbers[i])
                            {
                                isTheMaximumConsecutiveNumberRepeated = true;
                            }
                        }

                        if (isTheMaximumConsecutiveNumberRepeated == false)
                        {
                            int[] tempMaximumConsecutiveNumbers =
                            new int[maximumConsecutiveNumbers.Length + 1];

                            for (int l = 0; l < maximumConsecutiveNumbers.Length; l++)
                            {
                                tempMaximumConsecutiveNumbers[l] =
                                    maximumConsecutiveNumbers[l];
                            }

                            tempMaximumConsecutiveNumbers[tempMaximumConsecutiveNumbers.Length - 1] =
                                numbers[i];
                            maximumConsecutiveNumbers = tempMaximumConsecutiveNumbers;
                            numericalSequence = 0;
                        }
                    }
                }
                else
                {
                    numericalSequence = 1;
                }
            }

            Console.Write("\nЧисла с максимальной последовательностю чисел - ");

            for (int i = 0; i < maximumConsecutiveNumbers.Length; i++)
            {
                Console.Write(maximumConsecutiveNumbers[i] + " ");
            }

            Console.WriteLine("\nМаксимальная последовательность - " +
                numericalSequenceMax);
        }
    }
}
