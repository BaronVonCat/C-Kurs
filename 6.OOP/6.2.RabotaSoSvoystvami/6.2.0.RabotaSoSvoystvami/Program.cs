using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _6._2.RabotaSoSvoystvami
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Player player = new Player('O', 5, 10);
            Renderer renderer = new Renderer();

            renderer.DrawPlayer(player);
            Console.CursorVisible = false;
            Console.ReadKey();
        }
    }

    class Player
    {
        public char PlayerSign { get; private set;  }
        public int PositionX { get; private set; }
        public int PositionY { get; private set; }

        public Player(char playerSign, int positionX, int positionY)
        {
            PlayerSign = playerSign;
            PositionX = positionX;
            PositionY = positionY;
        }
    }

    class Renderer
    {
        public void DrawPlayer(Player player)
        {
            Console.SetCursorPosition(player.PositionX, player.PositionY);
            Console.Write(player.PlayerSign);
        }
    }
}
