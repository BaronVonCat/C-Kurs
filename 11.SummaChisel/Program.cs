using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _11.SummaChisel
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Random random = new Random();
            int randomNumberMax = 101;
            int rundomNumber;
            int number1 = 3;
            int number2 = 5;
            int sum = 0;

            rundomNumber = random.Next(randomNumberMax);
            Console.WriteLine(rundomNumber);

            for (int i = 1; i <= rundomNumber; i++)
            {
                if (i % number1 == 0 || i % number2 == 0)
                {
                    sum += i;
                }
            }

            Console.WriteLine(sum);
        }
    }
}
