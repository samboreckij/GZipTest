using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.IO;
using System.IO.Compression;

namespace GZipTest
{

    public static class GZipParallel
    {
        static int threadCount = Environment.ProcessorCount;
        static int blockSize = 1024 * 1024;
        static ConcurrentQueue<Thread> writerQueue = new ConcurrentQueue<Thread>();


        static GZipParallel()
        {
        }

        public static void CompressBlock(byte[] dataArray)
        {
            using (MemoryStream output = new MemoryStream(dataArray.Length))
            {
                using (GZipStream compressStream = new GZipStream(output, CompressionMode.Compress))
                {
                    compressStream.Write(dataArray, 0, dataArray.Length);
                }
                return output.ToArray();
            }
        }

        public static byte[] DecompressBlock(byte[] dataArray)
        {
            byte[] result = new ;
            using (MemoryStream input = new MemoryStream(dataArray))
            {
                using (GZipStream decompressStream = new GZipStream(input, CompressionMode.Decompress))
                {
                    decompressStream.Read(result, 0, dataArray.Length);
                }
            }
            return result;
        }

        /// <summary>
        /// Method for parallel GZip compression.
        /// </summary>
        /// <param name="input">Input file name.</param>
        /// <param name="output">Output file name.</param>
        public static void Compress(FileInfo input, FileInfo output)
        {
            try
            {
                //FileStream inStream = new FileStream(input.FullName, FileMode.Open);
                //FileStream outStream = new FileStream(output.FullName, FileMode.Append);
                //FileStream inFile = new FileStream(inFileName, FileMode.Open);
                //FileStream outFile = new FileStream(inFileName + ".gz", FileMode.Append);

                //int _dataPortionSize;
                //Thread[] tPool;

                //Console.Write("Compressing...");

                //while (inFile.Position < inFile.Length)
                //{
                //    Console.Write(".");
                //    tPool = new Thread[threadNumber];
                //    for (int portionCount = 0;
                //         (portionCount < threadNumber) && (inFile.Position < inFile.Length);
                //         portionCount++)
                //    {
                //        if (inFile.Length - inFile.Position <= dataPortionSize)
                //        {
                //            _dataPortionSize = (int)(inFile.Length - inFile.Position);
                //        }
                //        else
                //        {
                //            _dataPortionSize = dataPortionSize;
                //        }
                //        dataArray[portionCount] = new byte[_dataPortionSize];
                //        inFile.Read(dataArray[portionCount], 0, _dataPortionSize);

                //        tPool[portionCount] = new Thread(CompressBlock);
                //        tPool[portionCount].Start(portionCount);
                //    }

                //    for (int portionCount = 0; (portionCount < threadNumber) && (tPool[portionCount] != null);)
                //    {
                //        if (tPool[portionCount].ThreadState == ThreadState.Stopped)
                //        {
                //            BitConverter.GetBytes(compressedDataArray[portionCount].Length + 1)
                //                        .CopyTo(compressedDataArray[portionCount], 4);
                //            outFile.Write(compressedDataArray[portionCount], 0, compressedDataArray[portionCount].Length);
                //            portionCount++;
                //        }
                //    }

                //}

                //outFile.Close();
                //inFile.Close();
                // TODO: Algorithm must be completed.

                using (FileStream inStream = input.OpenRead())
                {
                    if ((File.GetAttributes(input.FullName) & FileAttributes.Hidden) != FileAttributes.Hidden & input.Extension != ".gz")
                    {
                        using (FileStream outStream = File.Create(output.FullName))
                        {
                            int tail, usingThreadsCount = 0;

                            Console.Write("Let's begin...");
                            while (inStream.Position < inStream.Length)
                            {
                                Thread compressThread;
                                Console.Write(".");
                                if (usingThreadsCount < threadCount)
                                {
                                    if (inStream.Length - inStream.Position <= blockSize)
                                    {
                                        tail = (int)(inStream.Length - inStream.Position);
                                    }
                                    else
                                    {
                                        tail = blockSize;
                                    }
                                    byte[] dataArray = new byte[tail];
                                    inStream.Read(dataArray, 0, tail);
                                    compressThread = new Thread(CompressBlock);
                                    writerQueue.Enqueue(compressThread);
                                    compressThread.Start(dataArray);
                                    usingThreadsCount++;
                                }
                                if (writerQueue.TryPeek(out compressThread))
                                {
                                    if (compressThread.ThreadState == ThreadState.Stopped)
                                    {
                                        compressThread
                                        BitConverter.GetBytes(compressedDataArray[portionCount].Length + 1).CopyTo(compressedDataArray[portionCount], 4);
                                        outFile.Write(compressedDataArray[portionCount], 0, compressedDataArray[portionCount].Length);
                                    }
                                }
                            }
                        }
                    }
                }


                //FileInfo info = new FileInfo(directoryPath + Path.DirectorySeparatorChar + fileToCompress.Name + ".gz");
                // TODO: Move console output to Main method.
                //Console.WriteLine($"Compressed {input.Name} from {input.Length.ToString()} to {output.Length.ToString()} bytes.");
            }
            catch (Exception e)
            {
                Console.WriteLine("Error: " + e.Message);
            }


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
