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
                //return output.ToArray();
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
                // TODO: Algorithm must be implemented.
                //while (CanRead or QueueIsNotEmpty)
                //{
                //  if (CanRead and usingThreadsCount < MaxThreadsCount) {
                //      ReadForCompress
                //      Enqueue
                //  }
                //  if (Thread on Queue's Peek == stopped) {
                //      SaveResult
                //      Dequeue
                //  }
                //}

                using (FileStream inStream = input.OpenRead())
                {
                    if ((File.GetAttributes(input.FullName) & FileAttributes.Hidden) != FileAttributes.Hidden & input.Extension != ".gz")
                    {
                        using (FileStream outStream = File.Create(output.FullName))
                        {
                            int tail;

                            Console.Write("Let's begin...");
                            while (inStream.Position < inStream.Length || writerQueue.Count > 0)
                            {
                                Thread compressThread;

                                if (inStream.Position < inStream.Length && writerQueue.Count < 8)
                                {
                                    Console.Write(".");
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
                                }
                                if (writerQueue.TryPeek(out compressThread) && compressThread.ThreadState == ThreadState.Stopped)
                                {

                                }
                                //
                                //
                                //if (usingThreadsCount < threadCount)
                                //{
                                //    
                                //    
                                //    usingThreadsCount++;
                                //}
                                //if (writerQueue.TryPeek(out compressThread))
                                //{
                                //    if (compressThread.ThreadState == ThreadState.Stopped)
                                //    {
                                //        compressThread
                                //        BitConverter.GetBytes(compressedDataArray[portionCount].Length + 1).CopyTo(compressedDataArray[portionCount], 4);
                                //        outFile.Write(compressedDataArray[portionCount], 0, compressedDataArray[portionCount].Length);
                                //    }
                                //}
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Error: " + e.Message);
            }
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
