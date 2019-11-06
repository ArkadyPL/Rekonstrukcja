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
            string filePath;

            if (args.Length == 0)
            {
                Console.WriteLine("Do you want to run performance tests? (y/n)");
                string answer = Console.ReadLine();
                if (answer.ToLower() == "y" || answer.ToLower() == "yes")
                {
                    RunTest();
                    return;
                }

                Console.WriteLine("Enter path to the file (relative from current working directory):");
                filePath = Console.ReadLine();
            }
            else
            {
                filePath = args[0];
            }
            // The way to quickly run one specific file - comment above and uncomment below
            // filePath = "./../../exemplaryInputs/input5x5-a.txt";

            try
            {
                Console.WriteLine("Tree reconstruction\n");
                var distanceMatrix = ReadInput(filePath);
                var result = new TreeFinder().FindTree(distanceMatrix);
                Console.WriteLine("\nResulting distance matrix");
                OutputResult(result, Console.OpenStandardOutput());
                Console.WriteLine();
                OutputResult(result, new FileStream("result.txt", FileMode.Create));
                Console.WriteLine("Result has been saved to the file result.txt");
            }
            catch(Exception e)
            {
                Console.WriteLine("An error occured: {0}", e.Message);
            }
        }

        static int[,] ReadInput(string filePath)
        {
            string[] lines = File.ReadAllLines(filePath);
            int n = lines.Length;
            var distanceMatrix = new int[n, n];
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
                    distanceMatrix[i, j] = int.Parse(valuesInRow[j]);
                }
            }

            Console.WriteLine("Input distance matrix:");
            Utils.DisplayMatrix(distanceMatrix, longestNumberLength);
            return distanceMatrix;
        }

        static void OutputResult(Node result, Stream stream)
        {
            var neighborsList = Utils.ConvertTreeToNeighboursList(result);
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
                        // We check each size 10 times to avoid outliers
                        for (int j = 0; j < 10; j++)
                        {
                            var matrix = InputGenerator.GenerateRandomInput(i);
                            stopwatch.Start();
                            new TreeFinder().FindTree(matrix);
                            stopwatch.Stop();
                        }
                        writer.WriteLine($"{i};{stopwatch.ElapsedMilliseconds / 10.0}");
                        stopwatch.Reset();
                    }
                }
            }
            Console.WriteLine("Performance tests - finished, results were saved in " + resultsPath + " file");
        }
    }
}
