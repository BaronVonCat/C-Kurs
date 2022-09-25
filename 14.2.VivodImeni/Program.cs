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
            string userName;
            char userSymbol;
            int numberSymbols;
            string lineName;
            string lineSymbols;

            Console.Write("Введите ваше имя: ");
            userName = Console.ReadLine();
            Console.Write("Введите символ: ");
            userSymbol = Convert.ToChar(Console.ReadLine());
            numberSymbols = userName.Length;
            lineSymbols = userSymbol.ToString() + userSymbol.ToString();
            lineName = userSymbol + userName + userSymbol;

            for (int i = numberSymbols; i != 0; i--)
            {
                lineSymbols += userSymbol;
            }

            Console.WriteLine(lineSymbols + "\n" + lineName + "\n" + lineSymbols);
        }
    }
}
