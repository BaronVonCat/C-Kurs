using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _15.ProgrammaPodParolem
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string userPassowrd = "qwe123";
            int totalAttempts = 3;
            string userInput;
            string massege = "ЯМАТЕ КУДАСАЙ СЕМПАЙ!!!";

            for (int i = 0; i < totalAttempts; i++)
            {
                Console.Write("Введите пароль: ");
                userInput = Console.ReadLine();

                if (userInput == userPassowrd)
                {
                    Console.WriteLine("Вы вошли в систему." +
                        " Открыт доступ к тайному сообщению.");
                    Console.ReadLine();
                    Console.Clear();
                    Console.WriteLine("Сообщение - " + massege);
                    break;
                }
                else
                {
                    int remainingAttempts;

                    remainingAttempts = totalAttempts - i - 1;
                    Console.WriteLine("Пароль неверен! Осталось попыток - " + remainingAttempts);
                }
            }
        }
    }
}
