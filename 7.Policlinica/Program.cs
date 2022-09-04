using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;

namespace _7.Policlinica
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int oldLedies;
            int timePerReceptionMinuts = 10;
            int waitingTimeMinuts;
            int waitingTimeHours;
            int minutsInHour = 60;
            int remainingMinutesInHour;

            Console.WriteLine("Вы заходите в поликлиннику и видите огромную очередь состоящую из..." +
                "\nВведите колличество старушек.");
            oldLedies = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("... из " +oldLedies+ " старушек.");
            waitingTimeMinuts = oldLedies * timePerReceptionMinuts;
            waitingTimeHours = waitingTimeMinuts / minutsInHour;
            remainingMinutesInHour = waitingTimeMinuts - minutsInHour * waitingTimeHours;
            Console.WriteLine("Зная, что приём к врачу длиться " + timePerReceptionMinuts + " минут, вы подсчитываете время ожидания в очереди, " +
                "которое состовяет " + waitingTimeHours + " часов и " + remainingMinutesInHour + " минут.");
        }
    }
}
