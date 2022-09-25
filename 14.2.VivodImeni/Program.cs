using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _14._2.VivodImeni
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string lineSymbols;
            string userName;
            char userSymbol;
            int numberSymbols;
            string lineName;

            Console.Write("Введите ваше имя: ");
            userName = Console.ReadLine();
            Console.Write("Введите символ: ");
            userSymbol = Convert.ToChar(Console.ReadLine());
            numberSymbols = userName.Length;
            lineName = userSymbol + userName + userSymbol;

            for (int i = numberSymbols; i != -2; i--)
            {
                Console.Write(userSymbol);

                if (i == -1)
                {
                    Console.WriteLine();
                }
            }

            Console.WriteLine(lineName);

            for (int i = numberSymbols; i != -2; i--)
            {
                Console.Write(userSymbol); 

                if (i == -1)
                {
                    Console.WriteLine();
                }
            }
        }
    }
}
