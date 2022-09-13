﻿using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _17.StepenDvoyki
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Random random = new Random();
            int numberMin = 1;
            int numberMax = 1000;
            int number;
            int numericalDegree = 2;
            int Degree = 0;
            int sum = 1;

            number = random.Next(numberMin, numberMax);

            while (sum <= number)
            {
                sum *= numericalDegree;
                Degree++;
            }

            Console.WriteLine("Число - " + number);
            Console.WriteLine("Минимальная степень - " + Degree);
            Console.WriteLine("Число в степени - " + sum);
        }
    }
}
