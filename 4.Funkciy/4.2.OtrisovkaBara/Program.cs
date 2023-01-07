using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _4._2.OtrisovkaBara
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Random random = new Random();
            int healthPointLimit = 100;
            int healthPointMin = 0;
            int healthPointMax = healthPointLimit + 1;
            int healthPoint = random.Next(healthPointMin, healthPointMax);

            Console.WriteLine(healthPoint);
            RenderinghealthPointBar(healthPoint, healthPointLimit);
        }

        static void RenderinghealthPointBar(int healthPoint, int healthPointLimit)
        {
            double lenghtBar = 200;

            Console.Write("|");

            for (int i = 0; i < lenghtBar; i++)
            {
                if (healthPointLimit / lenghtBar * i < healthPoint)
                {
                    Console.Write("+");
                }
                else
                {
                    Console.Write("-");
                }
            }

            Console.WriteLine("|");
        }
    }
}
