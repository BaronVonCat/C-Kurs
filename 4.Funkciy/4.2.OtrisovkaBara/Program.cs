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
            const int PercentMin = 0;
            const int PercentMax = 100;

            string manual1 = "Введите номер строки на которой будет отрисована шкала: ";
            string manual2 = "Введите процент заполненности шкалы: ";
            int stringNumber;
            int percent;

            stringNumber = ToCheckStringForNumber(manual1);
            percent = ToCheckStringForNumber(manual2, PercentMin, PercentMax);
            ToRenderHealthPointBar(stringNumber, percent);
        }

        static void ToRenderHealthPointBar(int stringNumber, int percent)
        {
            const int PersentMax = 100;

            int positionHealthPointBarX = 0;
            int positionHealthPointBarY = stringNumber - 1;
            double lenghtBar = 200;
            double valueOnePercent = lenghtBar / PersentMax; 
            double percentageToValue = percent * valueOnePercent;

            Console.SetCursorPosition(positionHealthPointBarX, positionHealthPointBarY);
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

        static int ToCheckStringForNumber(string manual, int numberMin = 1, int numberMax = int.MaxValue)
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
