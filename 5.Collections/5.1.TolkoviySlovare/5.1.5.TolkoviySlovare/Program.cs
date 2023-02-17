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
            Dictionary<string, string> words = new Dictionary<string, string>();
            bool isWorking = false;

            words = FillWords(words);

            while (isWorking == false)
            {
                const string CommandExit = "0";

                string userInput;
                string manual = "Введите номер команды или слово - значение которого вы хотите получить: ";

                Console.WriteLine($"{manual}\n" +
                    $"\nДоступные команды:\n" +
                    $"\n{CommandExit}. Выход из программы");
                OutputWords(words);
                Console.SetCursorPosition( manual.Length, 0 );
                userInput = Console.ReadLine();
                Console.Clear();

                if (userInput != CommandExit)
                {
                    SearchMeaningWord(words, userInput);
                }
                else
                {
                    isWorking = true;
                }
            }
        }

        static Dictionary<string, string> FillWords(Dictionary<string, string> words)
        {
            string wordAndValueDoctor = "Врач-человек, что предотворощает болезни и недуги.";
            string wordAndValueArchitect = "Архитектор-человек, что проектирует здания и сооружения.";
            string wordAndValueBicycle = "Велосипед-средство передвижения на двух колёсах.";
            string[] fileWords = { wordAndValueDoctor, wordAndValueArchitect, wordAndValueBicycle };

            for (int i = 0; i < fileWords.Length; i++)
            {
                string[] wordAndValue = fileWords[i].Split('-');
                string word = wordAndValue[0];
                string valueWord = wordAndValue[1];
                words.Add(word, valueWord);
            }

            return words;
        }

        static void OutputWords(Dictionary<string, string> words)
        {
            Console.WriteLine("\nДоступные слова:\n");

            foreach (var word in words)
            {
                Console.WriteLine($"{word.Key}");
            }
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
                Console.WriteLine("Команда или слово не найдены!");
            }

            Console.ReadKey();
            Console.Clear();
        }
    }
}
