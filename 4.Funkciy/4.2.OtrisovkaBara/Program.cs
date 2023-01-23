using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace _4._2.OtrisovkaBara
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string nameHealthPointBar = "Здоровье";

            CreatingPointBar(nameHealthPointBar);
        }

        static void CreatingPointBar(string namePointBar)
        {
            const int PercentMin = 0;
            const int PersentMax = 100;

            string manualLineNumber = "Введите номер строки на которой будет отрисована шкала: ";
            string manualPercent = "Введите процент заполненности шкалы: ";
            int lineNumber = 0;
            int percent;
            int positionPointBarX = 0;
            int positionPointBarY;

            lineNumber = GetNumber(manualLineNumber);
            positionPointBarY = lineNumber - 1;
            percent = GetNumber(manualPercent, PercentMin, PersentMax);
            Console.SetCursorPosition(positionPointBarX, positionPointBarY);
            Console.Write(namePointBar + " ");
            DrawPointBar(percent);
        }

        static void DrawPointBar(int percent)
        {
            const int PersentMax = 100;

            double lenghtBar = 10;
            double valueOnePercent = lenghtBar / PersentMax;
            double percentageToValue = percent * valueOnePercent;

            Console.Write("|");

            for (int i = 0; i < percentageToValue; i++)
            {
                Console.Write("+");
                lenghtBar--;
            }

            for (int i = 0; i < lenghtBar; i++)
            {
                Console.Write("-");
            }

            Console.WriteLine("|");
        }

        static int GetNumber(string manual, int numberMin = 1, int numberMax = int.MaxValue)
        {
            string userInput;
            bool isStringNumber;
            int enteredNumber = 0;
            bool isAppropriateNumber = false;

            while (isAppropriateNumber == false)
            {
                Console.SetCursorPosition(0, 0);
                Console.Write(manual);
                userInput = Console.ReadLine();
                Console.Clear();
                isStringNumber = int.TryParse(userInput, out enteredNumber);

                if (isStringNumber == true && enteredNumber >= numberMin && enteredNumber <= numberMax)
                {
                    isAppropriateNumber = true;
                }
                else
                {
                    Console.SetCursorPosition(Console.CursorTop, Console.CursorTop + 1);
                    Console.WriteLine("Некорректынй запрос!");
                }
            }

            return enteredNumber;
        }
    }
}
