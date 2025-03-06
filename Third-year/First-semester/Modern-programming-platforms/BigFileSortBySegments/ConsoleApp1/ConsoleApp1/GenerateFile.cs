using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Diagnostics;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            string CurFileName = "123.txt";
            Stopwatch CurStopwatch = new Stopwatch();

            
            RandomFile CurRandomFile = new RandomFile(CurFileName);

            CurStopwatch.Start();
            CurRandomFile.GenerateFile();
            CurStopwatch.Stop();
            Console.WriteLine("Generation File Took: " + CurStopwatch.ElapsedMilliseconds);
            CurStopwatch.Reset();
            
            
            SortFile CurSortFile = new SortFile(CurFileName);
            
            CurStopwatch.Start();
            CurSortFile.Sort();
            CurStopwatch.Stop();
            Console.WriteLine("Sorting File Took: " + CurStopwatch.ElapsedMilliseconds);
            CurStopwatch.Reset();
            
            Console.WriteLine("!!!");
            Console.ReadKey();
        }
    }

    public class SortFile
    {
        private readonly int RAMLimit = 5 * 1 * 1000 * 1000;
        private readonly int MaxWordLength = 30;
        public string FileName { get; set; }
        public SortFile(string CurFileName)
        {
            FileName = CurFileName;
        }

        private class TmpFile
        {
            public string FileName { get; set; }
            public int Position { get; set; }
            public TmpFile(string CurFileName)
            {
                FileName = CurFileName;
                Position = 0;
            }
        }
        private List<TmpFile> TmpFiles = new List<TmpFile>();
       
        private void SplitFileInParts()
        {
            using (StreamReader CurStreamReader = new StreamReader(FileName, Encoding.Unicode))
            {
                int CurCount;
                char[] CurBuffer = new char[RAMLimit + MaxWordLength + 2];
                while ((CurCount = CurStreamReader.ReadBlock(CurBuffer, 0, RAMLimit)) != 0)
                {
                    while (CurBuffer[CurCount - 1] != '\n')
                    {
                        CurBuffer[CurCount++] = (char)CurStreamReader.Read();
                    }
                    TmpFile CurTmpFile = new TmpFile("Tmp" + TmpFiles.Count + ".txt");
                    TmpFiles.Add(CurTmpFile);
                    string[] CurStrings = new string(CurBuffer, 0, CurCount).Split(new char[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);

                    Array.Sort(CurStrings);
                    File.WriteAllLines(CurTmpFile.FileName, CurStrings, Encoding.Unicode);
                }
            }
        }
        private void SortParts()
        {
            string SortedFileName = FileName.Insert(FileName.LastIndexOf('.', FileName.Length - 1), "_Sorted");
            File.Create(SortedFileName).Close();

            using (StreamReader StreamReaderFirst = new StreamReader(TmpFiles[0].FileName, Encoding.Unicode))
            {
                int CurCount;                
                char[] CurBuffer = new char[RAMLimit / TmpFiles.Count() + MaxWordLength + 2];
                while ((CurCount = StreamReaderFirst.ReadBlock(CurBuffer, 0, RAMLimit / TmpFiles.Count())) != 0)
                {
                    while (CurBuffer[CurCount - 1] != '\n')
                    {
                        CurBuffer[CurCount++] = (char)StreamReaderFirst.Read();
                    }
                    List<string> CurStrings = new string(CurBuffer, 0, CurCount).Split(new char[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries).ToList();
                    string CurStringComparison = CurStrings[CurStrings.Count() - 1];
                    bool CurCondition = StreamReaderFirst.Peek() == -1;
                    for (int j = 1; j < TmpFiles.Count; j++)
                    {
                        using (StreamReader CurStreamReader = new StreamReader(TmpFiles[j].FileName, Encoding.Unicode))
                        {
                            CurStreamReader.BaseStream.Seek(TmpFiles[j].Position, SeekOrigin.Begin);
                            string TmpStr;                            
                            while ((TmpStr = CurStreamReader.ReadLine()) != null && (string.Compare(CurStringComparison, TmpStr) >= 0 || CurCondition))
                            {
                                CurStrings.Add(TmpStr);
                                TmpFiles[j].Position += (TmpStr.Length + 2) * 2;
                            }
                        }
                    }
                    CurStrings.Sort();
                    File.AppendAllLines(SortedFileName, CurStrings, Encoding.Unicode);                    
                }
            }
        }
        private void RemoveTmpFiles()
        {
            for (int i = 0; i < TmpFiles.Count(); i++)
            {
                File.Delete(TmpFiles[i].FileName);
            }
        }

        public void Sort()
        {
            Stopwatch CurStopwatch = new Stopwatch();


            CurStopwatch.Start();
            SplitFileInParts();
            CurStopwatch.Stop();
            Console.WriteLine("Spliting File In Parts Took: " + CurStopwatch.ElapsedMilliseconds);
            CurStopwatch.Reset();

            CurStopwatch.Start();
            SortParts();
            CurStopwatch.Stop();
            Console.WriteLine("Sorting Parts Took: " + CurStopwatch.ElapsedMilliseconds);
            CurStopwatch.Reset();

            RemoveTmpFiles();
        }
    }

    public class RandomFile
    {
        private readonly int MaxFileLength = 5 * 100 * 1000 * 1000;
        private readonly int RAMLimit = 5 * 10 * 1000 * 1000;
        private readonly int MaxWordLength = 30;
        public string FileName { get; set; }
        public RandomFile(string CurFileName)
        {
            FileName = CurFileName;
        }
        public void GenerateFile()
        {
            File.Create(FileName).Close();

            Random CurRandom = new Random();
            int CurFileLength = 0;
            while (CurFileLength < MaxFileLength)
            {
                int CurRAM = 0;
                StringBuilder PieceOfFileText = new StringBuilder((RAMLimit + MaxWordLength + 2) * 2);
                while (CurRAM < RAMLimit)
                {
                    int CurWordLength = CurRandom.Next(1, MaxWordLength);
                    CurFileLength += CurWordLength + 2;
                    CurRAM += CurWordLength + 2;
                    for (int i = 0; i < CurWordLength; i++)
                    {
                        char CurRandomChar = (char)CurRandom.Next(65, 91);
                        PieceOfFileText.Append(CurRandomChar);
                    }
                    PieceOfFileText.Append("\r\n");
                }
                File.AppendAllText(FileName, PieceOfFileText.ToString(), Encoding.Unicode);
            }
        }
    }
}