using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GZipTest
{
    class Program
    {
        static void Help()
        {
            Console.WriteLine("How to use this program?");
        }

        static int Main(string[] args)
        {
            if (args.Length == 0)
            {
                Help();
                return 1;
            }

            return 0;
        }
    }
}
