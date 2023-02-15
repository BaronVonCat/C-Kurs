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

            words = CreateWords(words);

            while (isWorking == false)
            {
                const string CommandExit = "0";

                string userInput;
                string manual = "Введите команду или слово - значение которого вы хотите получить: ";

                Console.WriteLine($"{manual}\n\n{CommandExit}. Выход из программы");
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

        static Dictionary<string, string> CreateWords(Dictionary<string, string> words)
        {
            const string Word1 = "Врач-человек, что предотворощает болезни и недуги.";
            const string Word2 = "Архитектор - человек, что проектирует здания и сооружения.";
            const string Word3 = "Велосипед-средство передвижения на двух колёсах.";
            
            string[] fileWords = { Word1, Word2, Word3 };

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
                Console.WriteLine("Команда или слово не найдено!");
            }

            Console.ReadKey();
            Console.Clear();
        }
    }
}
