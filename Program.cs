﻿using System;
using System.IO;
using System.Linq;

namespace Rekonstrukcja
{

    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Tree reconstruction\n");
            var distanceMatrix = ReadInput();
            var result = TreeFinder.FindTree(distanceMatrix);
            OutputResult(result);
            Console.Read();
        }

        static double[,] ReadInput()
        { 
            // TODO: add possibility to generate random input of selected size
            // TODO: in the end switch to reading file of given name from the current working directory
            string[] lines = File.ReadAllLines(".\\..\\..\\przykładowe wejścia\\input1.txt");
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

        static void OutputResult(double[,] result)
        {
            Console.WriteLine("\nResulting distance matrix");
            var longestNumberLength = (from double item in result select item.ToString().Length).Max();
            Utils.DisplayMatrix(result, longestNumberLength);
            var neighborsList = Utils.ConvertMatrixToNeighborsList(result);
            Utils.WriteNeighborsListToStream(neighborsList, Console.OpenStandardOutput());
        }
    }
}
