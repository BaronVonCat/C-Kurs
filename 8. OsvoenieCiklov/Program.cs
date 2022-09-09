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

            Console.Write("Введите текст: ");
            text = Console.ReadLine();
            Console.Clear();
            Console.Write("Введите количество повторений: ");

            for (int numberRepetitionsText = Convert.ToInt32(Console.ReadLine()); numberRepetitionsText > 0; numberRepetitionsText--) 
            {
                Console.WriteLine(text);
            }
        }
    }
}
