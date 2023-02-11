using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Security.Cryptography;
using System.Globalization;

namespace _5._1.TolkoviySlovare
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Dictionary<string, string> words = CreateWords();
            bool isUserExit = false;

            while (isUserExit == false)
            {
                const string CommandExit = "0";

                string userinput;
                string manual = "Введите номер команды или искомое слово: ";

                Console.WriteLine($"{manual}\n\n{CommandExit}. Выход из программы");
                Console.SetCursorPosition( manual.Length, 0 );
                userinput = Console.ReadLine();
                Console.Clear();

                if (userinput != CommandExit)
                {
                    SearchMeaningWord(words, userinput);
                }
                else
                {
                    isUserExit = true;
                }
            }
        }

        static Dictionary<string, string> CreateWords()
        {
            Dictionary<string, string> words = new Dictionary<string, string>();
            string[] fileWords = 
                { "Врач-человек, что предотворощает болезни и недуги.",
                "Архитектор-человек, что проектирует здания и сооружения.",
                "Велосипед-средство передвижения на двух колёсах."};

            for (int i = 0; i < fileWords.Length; i++)
            {
                string[] wordAndValue = fileWords[i].Split('-');
                string word = wordAndValue[0];
                string valueWord = wordAndValue[1];
                words.Add(word, valueWord);
            }

            return words;
        }

        static void SearchMeaningWord(Dictionary<string, string> words, string searchWord)
        {
            TextInfo textInfo = CultureInfo.CurrentCulture.TextInfo;

            searchWord = searchWord.ToLower();
            searchWord = textInfo.ToTitleCase(searchWord);

            if (words.ContainsKey(searchWord))
            {
                Console.WriteLine($"{searchWord} - {words[searchWord]}");
            }
            else
            {
                Console.WriteLine("Некорректный запрос!");
            }

            Console.ReadKey();
            Console.Clear();
        }
    }
}
