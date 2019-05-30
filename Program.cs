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
            string filePath = null;

            if (args.Length == 0)
            {
                Console.WriteLine("Do you want to run performance tests? (y/n)");
                string answer = Console.ReadLine();
                if (answer.ToLower() == "y" || answer.ToLower() == "yes")
                {
                    RunTest();
                    Console.Read();
                    return;
                }

                Console.WriteLine("Enter path to the file (relative from current working directory):");
                filePath = Console.ReadLine();
            }
            else
            {
                filePath = args[0];
            }
            // filePath = "./../../exemplaryInputs/input2.txt";

            Console.WriteLine("Tree reconstruction\n");
            var distanceMatrix = ReadInput(filePath);
            var result = TreeFinder.FindTree(distanceMatrix);
            OutputResult(result, Console.OpenStandardOutput());
            Console.Read();
        }

        static double[,] ReadInput(string filePath)
        {
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
            Utils.DisplayMatrix(distanceMatrix, longestNumberLength);
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
            string resultsPath = "wyniki.txt";
            var stopwatch = new Stopwatch();

            Console.WriteLine("Performance tests - started");
            using (var stream = new FileStream(resultsPath, FileMode.Create))
            {
                using(var writer = new StreamWriter(stream))
                {
                    for (int i = 5; i <= 200; i += 5)
                    {
                        Console.WriteLine("n = " + i);
                        var matrix = InputGenerator.GenerateRandomInput(i);
                        var longestNumberLength = (from double item in matrix select item.ToString().Length).Max();
                        Utils.DisplayMatrix(matrix, longestNumberLength);
                        stopwatch.Start();
                        TreeFinder.FindTree(matrix);
                        stopwatch.Stop();
                        writer.WriteLine($"{i};{stopwatch.ElapsedMilliseconds}");
                        stopwatch.Reset();
                    }
                }
            }
            Console.WriteLine("Performance tests - finished, results were saved in " + resultsPath + " file");
        }
    }
}
