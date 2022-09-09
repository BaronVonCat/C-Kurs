using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _10.Posledovatelnoste
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int endSequence = 96;
            int increasingSequence = 7;

            for (int beginningSequence = 5; beginningSequence <= endSequence; beginningSequence += increasingSequence)
            {
                Console.WriteLine(beginningSequence);
            }
        }
    }
}
