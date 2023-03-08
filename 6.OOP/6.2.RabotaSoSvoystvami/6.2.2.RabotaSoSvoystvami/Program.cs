using System;

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
        public Player(char sign, int positionX, int positionY)
        {
            Sign = sign;
            PositionX = positionX;
            PositionY = positionY;
        }

        public char Sign { get; private set; }
        public int PositionX { get; private set; }
        public int PositionY { get; private set; }
    }

    class Renderer
    {
        public void DrawPlayer(Player player)
        {
            Console.SetCursorPosition(player.PositionX, player.PositionY);
            Console.Write(player.Sign);
        }
    }
}
