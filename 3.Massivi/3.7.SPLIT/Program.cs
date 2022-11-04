using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _3._7.SPLIT
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string text = "Дана строка с текстом";
            string[] words = text.Split(' ');

            Console.WriteLine(text + "\n");

            for (int i = 0; i < words.Length; i++)
            {
                Console.WriteLine(words[i]);
            }
        }
    }
}
