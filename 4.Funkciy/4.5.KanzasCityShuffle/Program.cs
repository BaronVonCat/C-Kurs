using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _4._5.KanzasCityShuffle
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int[] numbers = СreateNumbers();

            DrawNumbers(numbers);
            Console.WriteLine();
            numbers = ShuffleNumbers(numbers);
            DrawNumbers(numbers);
            Console.WriteLine();
            numbers = ShuffleNumbers(numbers);
            DrawNumbers(numbers);
            Console.WriteLine();
        }

        static int[] СreateNumbers()
        {
            int lenghtNumbers = 10;
            int[] numbers = new int[lenghtNumbers];

            for (int i = 0; i < numbers.Length; i++)
            {
                numbers[i] = i;
            }
            return numbers;
        }

        static void DrawNumbers(int[] numbers)
        {
            for (int i = 0; i < numbers.Length; i++)
            {
                Console.Write(numbers[i]);
            }
        }

        static int[] ShuffleNumbers(int[] numbers)
        {
            Random random= new Random();
            int shuffleNumber;

            for (int i = 0; i < numbers.Length; i++)
            {
                shuffleNumber = random.Next(0, numbers.Length);
                (numbers[i], numbers[shuffleNumber]) = (numbers[shuffleNumber], numbers[i]); 
            }

            return numbers;
        }
    }
}
