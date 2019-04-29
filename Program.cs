using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rekonstrukcja
{

    // General TODO: prepare a few exemplary input files

    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Tree reconstruction\n");
            var input = readInput();
            var result = TreeFinder.findTree(input);
            outputResult(result);
            Console.Read();
        }

        static int[,] readInput()
        { 
            // TODO: add possibility to generate random input of selected size
            // TODO: in the end switch to reading file of given name from the current working directory
            string[] lines = File.ReadAllLines(".\\..\\..\\input.txt");
            int n = lines.Length;
            var result = new int[n, n];
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
                    result[i, j] = int.Parse(valuesInRow[j]);
                }
            }

            Console.WriteLine("Input adjacency matrix:");
            Utils.DisplayMatrix(result, longestNumberLength);
            return result;
        }

        static void outputResult(int[,] result)
        {
            // TODO: display result
            // TODO: save result to the file
        }
    }
}
