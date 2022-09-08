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
            for(int beginningSequence = 5, endSequence = 96, increasingSequence = 7;
                beginningSequence <= endSequence; beginningSequence += increasingSequence)
            {
                Console.WriteLine(beginningSequence);
            }
        }
    }
}
