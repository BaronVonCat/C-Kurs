using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _16.KratnieChisla
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int naturNumberMax = 999;
            int natureNumberMin = 100;
            Random random = new Random();
            int number;
            int numberMin = 1;
            int numberMax = 28;
            int totalNumbers = 0;


            number = random.Next(numberMin, numberMax);

            for (int sum = natureNumberMin; sum <= naturNumberMax; sum += number)
            {
                if (sum <= naturNumberMax)
                {
                    totalNumbers++;
                }
            }

            Console.WriteLine(number + "\n" + totalNumbers);
        }
    }
}
