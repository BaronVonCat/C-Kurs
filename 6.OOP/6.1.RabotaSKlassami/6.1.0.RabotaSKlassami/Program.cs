using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _6._1.RabotaSKlassami
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Player player = new Player("Бекташи", "Снайпер", 1);

            player.ShowInfo();
        }
    }

    class Player
    {
        private string _name;
        private string _grade;
        private int _level;

        public Player(string name, string grade, int level)
        {
            _name = name;
            _grade = grade;
            _level = level;
        }

        public void ShowInfo()
        {
            Console.WriteLine($"Данные об игроке:\n\n" +
                $"Имя - {_name}\n" +
                $"Класс - {_grade}\n" +
                $"Уровень - {_level}");
        }
    }
}
