using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

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
            string[] fileWords = File.ReadAllLines("Data/words.txt");

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
            bool isWordSearch = false;

            foreach (var word in words)
            {
                if (word.Key.ToLower() == searchWord.ToLower())
                {
                    Console.WriteLine(word.Key + " - " +  word.Value);
                    isWordSearch = true;
                }
            }

            if (isWordSearch == false)
            {
                Console.WriteLine("Некорректный запрос!");
            }

            Console.ReadKey();
            Console.Clear();
        }
    }
}
