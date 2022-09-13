using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _14.VivodImeny
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string userName;
            char userSymbol;
            int simbolsPerLineUp;
            int simbolsPerLineDown;

            Console.Write("Введите ваше имя: ");
            userName = Console.ReadLine();
            Console.WriteLine("Введите символ: ");
            userSymbol = Convert.ToChar(Console.ReadLine());
            simbolsPerLineUp = (userName.Length) + 2;
            simbolsPerLineDown = (userName.Length) + 2;

            for (int i = 1; simbolsPerLineUp != 0; simbolsPerLineUp -= i)
            {
                Console.Write(userSymbol);
            }

            Console.WriteLine("\n" + userSymbol + userName + userSymbol);

            for (int i = 1; simbolsPerLineDown != 0; simbolsPerLineDown -= i)
            {
                Console.Write(userSymbol);
            }

            Console.WriteLine("\n");
        }
    }
}
