using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _9.KontroleVihoda
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string text;
            int numberRepetitionsText;
            string exitTheLoop = "exit";
            string userInput;

            Console.Write("Введите текст: ");
            text = Console.ReadLine();
            Console.Clear();
            Console.Write("Введите количество повторений: ");
            numberRepetitionsText = Convert.ToInt32(Console.ReadLine());
            Console.Clear();
            Console.WriteLine("Чтобы выйти из программы введите - " + exitTheLoop);
            Console.ReadLine();

            while (numberRepetitionsText > 0)
            {
                Console.WriteLine(text);
                numberRepetitionsText--;
                userInput = Console.ReadLine();
                
                if (userInput == exitTheLoop)
                {
                    Console.WriteLine("Вы вышли из программы");
                    break;
                }

            }
        }
    }
}
