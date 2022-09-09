using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _9._1.KontrolVihoda
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string exitProgram = "exit";
            string userInput;

            Console.WriteLine("Вас приветствует экран загрузки. Чтобы прервать загрузку введите " + exitProgram + ".");
            userInput = Console.ReadLine();

            while (userInput != exitProgram)
            {
                Console.WriteLine("Загрузка...");
                userInput = Console.ReadLine();
            }
        }
    }
}
