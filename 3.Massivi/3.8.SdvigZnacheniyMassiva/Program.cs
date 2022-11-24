using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _3._8.SdvigZnacheniyMassiva
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int numberOfNumbers = 10;
            int[] numbers = new int[numberOfNumbers];
            int customShiftValue;

            for (int i = 0; i < numbers.Length; i++)
            {
                numbers[i] = i;
                Console.Write(numbers[i] + " ");
            }

            Console.Write("\nВведите число на которое хотите сдвинуть" +
                " последовательность - ");
            customShiftValue = Convert.ToInt32(Console.ReadLine());

            for (int i = customShiftValue; i > 0; i--)
            {
                int shiftedNumber = numbers[0];

                for (int j = 0; j < numbers.Length - 1; j++)
                {
                    numbers[j] = numbers[j + 1];
                }

                numbers[numbers.Length - 1] = shiftedNumber;
            }

            foreach (int number in numbers)
            {
                Console.Write(number + " ");
            }

            Console.WriteLine();
        }
    }
}
