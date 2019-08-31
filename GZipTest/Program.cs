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
            // TODO: print types and values of arguments
        }

        static int Main(string[] args)
        {
            if (args.Length == 0)
            {
                Help();
                return 1;
            }

            Console.WriteLine(args[0], args[1], args[2]);

            return 0;
        }
    }
}
