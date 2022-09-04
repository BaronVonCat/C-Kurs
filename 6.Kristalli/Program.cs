using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _6.Kristalli
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int userGold;
            int crystals;
            int crystalPrice = 5;

            Console.WriteLine("Вы пришли в магазин кристаллов и хотите их купить. С собой вы взяли...\nВведите колличество золота.");
            userGold = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("...С собой вы взяли " + userGold + " золота.");
            Console.ReadLine();
            Console.WriteLine("Вы находите продавца и спрашиваете у него сколько стоят кристаллы.\nПродавец: Кристаллы стоят по " + crystalPrice + " золотых каждый. Сколько тебе нужно?" +
                "\nВведите колличество кристаллов которые хотиете приобрести.");
            crystals = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Вы приобрели кристаллы в колличестве " + crystals + " едениц.");
            Console.ReadLine();
            userGold -= crystals * crystalPrice; 
            Console.WriteLine("Инвентарь:\nЗолото - " + userGold + "\nКристаллы - " + crystals);
        }
    }
}
