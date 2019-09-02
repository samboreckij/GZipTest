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

        /// <summary>
        /// Print user manual.
        /// </summary>
        static void Help()
        {
            Console.WriteLine("How to use this program?");
            // TODO: print types and values of arguments
        }

        /// <summary>
        /// Making FileInfo object from string.
        /// </summary>
        /// <param name="fileName">File name string.</param>
        /// <returns></returns>
        private static FileInfo MakeFileInfo(string fileName)
        {
            return new FileInfo(fileName);
        }

        static int Main(string[] args)
        {
            // Input args: compress/decompress [inputFileName] [outputFileName]
            if (args.Length < 3)
            {
                Help();
                return 1;
            }

            FileInfo inputFile = new FileInfo(args[1]);
            FileInfo outputFile = new FileInfo(args[2]);

            if (args[0] == "compress")
            {
                GZipParallel.Compress(inputFile, outputFile);
            }
            if (args[0] == "decompress")
            {
                GZipParallel.Decompress(inputFile, outputFile);
            }

            return 0;
        }
    }
}
