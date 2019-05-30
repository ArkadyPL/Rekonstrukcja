using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using RandomInputGenerator;

namespace Rekonstrukcja
{

    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length == 1)
            {
                Console.WriteLine("Tree reconstruction\n");
                var distanceMatrix = ReadInput(args[0]);
                var result = TreeFinder.FindTree(distanceMatrix);
                OutputResult(result, Console.OpenStandardOutput());
                Console.Read();
            }
            else
            {
                RunTest();
            }
        }

        static double[,] ReadInput(string filePath)
        { 
            // TODO: in the end switch to reading file of given name from the current working directory
            string[] lines = File.ReadAllLines(filePath);
            int n = lines.Length;
            var distanceMatrix = new double[n, n];
            int longestNumberLength = 0;
            for (var i = 0; i < n; i++)
            {
                var valuesInRow = lines[i].Split(';');
                for (var j = 0; j < n; j++)
                {
                    if (valuesInRow[j].Length > longestNumberLength)
                    {
                        longestNumberLength = valuesInRow[j].Length;
                    }
                    distanceMatrix[i, j] = double.Parse(valuesInRow[j]);
                }
            }

            Console.WriteLine("Input distance matrix:");
            //Utils.DisplayMatrix(distanceMatrix, longestNumberLength);
            return distanceMatrix;
        }

        static void OutputResult(double[,] result, Stream stream)
        {
            Console.WriteLine("\nResulting distance matrix");
            var longestNumberLength = (from double item in result select item.ToString().Length).Max();
            Utils.DisplayMatrix(result, longestNumberLength);
            var neighborsList = Utils.ConvertMatrixToNeighborsList(result);
            Utils.WriteNeighborsListToStream(neighborsList, stream);
        }

        static void RunTest()
        {
            var stopwatch = new Stopwatch();

            using (var stream = new FileStream("wyniki.txt", FileMode.Create))
            {
                using(var writer = new StreamWriter(stream))
                {
                    for (int i = 5; i <= 200; i += 5)
                    {
                        var matrix = InputGenerator.GenerateRandomInput(i);
                        stopwatch.Start();
                        TreeFinder.FindTree(matrix);
                        stopwatch.Stop();
                        writer.WriteLine($"{i};{stopwatch.ElapsedMilliseconds}");
                        stopwatch.Reset();
                    }
                }
            }
        }
    }
}
