using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.IO.Compression;

namespace GZipTest
{
    class Program
    {
        private static string directoryPath = @".\temp";
        static void Help()
        {
            Console.WriteLine("How to use this program?");
            // TODO: print types and values of arguments
        }

        static int Main(string[] args)
        {
            // Input args: compress/decompress [inputFileName] [outputFileName]
            //if (args.Length == 0)
            //{
              //  Help();
                //return 1;
            //}
            DirectoryInfo directorySelected = new DirectoryInfo(directoryPath);
            GZipParallel.Compress(directorySelected);

            foreach (FileInfo fileToDecompress in directorySelected.GetFiles("*.gz"))
            {
                GZipParallel.Decompress(fileToDecompress);
            }        
            
            return 0;
        }
    }
}
