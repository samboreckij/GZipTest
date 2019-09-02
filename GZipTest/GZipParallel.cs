using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.IO.Compression;

namespace GZipTest
{

    public static class GZipParallel
    {
        private static string directoryPath = @".\temp";

        static GZipParallel()
        {
        }

        /// <summary>
        /// Method for parallel GZip compression.
        /// </summary>
        /// <param name="input">Input file name.</param>
        /// <param name="output">Output file name.</param>
        public static void Compress(FileInfo input, FileInfo output)
        {
            //foreach (FileInfo fileToCompress in directorySelected.GetFiles())
            //{
            //    using (FileStream originalFileStream = fileToCompress.OpenRead())
            //    {
            //        if ((File.GetAttributes(fileToCompress.FullName) &
            //           FileAttributes.Hidden) != FileAttributes.Hidden & fileToCompress.Extension != ".gz")
            //        {
            //            using (FileStream compressedFileStream = File.Create(fileToCompress.FullName + ".gz"))
            //            {
            //                using (GZipStream compressionStream = new GZipStream(compressedFileStream,
            //                   CompressionMode.Compress))
            //                {
            //                    originalFileStream.CopyTo(compressionStream);

            //                }
            //            }
            //            FileInfo info = new FileInfo(directoryPath + Path.DirectorySeparatorChar + fileToCompress.Name + ".gz");
            //            Console.WriteLine($"Compressed {fileToCompress.Name} from {fileToCompress.Length.ToString()} to {info.Length.ToString()} bytes.");
            //        }

            //    }
            //}
        }

        /// <summary>
        /// Method for parallel GZip decompression.
        /// </summary>
        /// <param name="input">Input file name.</param>
        /// <param name="output">Output file name.</param>
        public static void Decompress(FileInfo input, FileInfo output)
        {
            //using (FileStream originalFileStream = fileToDecompress.OpenRead())
            //{
            //    string currentFileName = fileToDecompress.FullName;
            //    string newFileName = currentFileName.Remove(currentFileName.Length - fileToDecompress.Extension.Length);

            //    using (FileStream decompressedFileStream = File.Create(newFileName))
            //    {
            //        using (GZipStream decompressionStream = new GZipStream(originalFileStream, CompressionMode.Decompress))
            //        {
            //            decompressionStream.CopyTo(decompressedFileStream);
            //            Console.WriteLine($"Decompressed: {fileToDecompress.Name}");
            //        }
            //    }
            //}
        }
    }
}
