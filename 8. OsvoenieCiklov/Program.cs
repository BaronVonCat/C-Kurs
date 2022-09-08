using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _8.OsvoenieCiklov
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string text;
            int numberRepetitionsText;

            Console.Write("Введите текст: ");
            text = Console.ReadLine();
            Console.Clear();
            Console.Write("Введите количество повторений: ");
            numberRepetitionsText = Convert.ToInt32(Console.ReadLine());
            Console.Clear();
            while (numberRepetitionsText > 0) 
            {
                Console.WriteLine(text);
                numberRepetitionsText--;
            }
        }
    }
}
