using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Rekonstrukcja
{
    class Utils
    {    
        
        /**
         * Only affects upper part of the matrix
         */
        public static int[,] EnlargeMatrixBy1(int[,] matrix)
        {
            int n = (int) Math.Sqrt(matrix.Length);
            var biggerMatrix = new int[n + 1, n + 1];
            for (var i = 0; i < n; i++)
            {
                for (var j = 0; j < n; j++)
                {
                    biggerMatrix[i, j] = matrix[i, j];
                }
            }
            return biggerMatrix;
        }

        public static void DisplayMatrix(int[,] matrix, int longestNumberLength)
        {
            int n = (int) Math.Sqrt(matrix.Length);
            for (var i = 0; i < n; i++)
            {
                Console.Write("|");
                for (var j = 0; j < n; j++)
                {
                    for (var k = 0; k < longestNumberLength - NumberLength(matrix[i, j]) + 1; k++)
                    {
                        Console.Write(" ");
                    }
                    Console.Write(matrix[i, j].ToString());
                }
                Console.WriteLine(" |");
            }
        }

        private static int NumberLength(int number)
        {
            return number.ToString().Length;
        }

        internal static List<List<int>> ConvertTreeToNeighboursList(Node root)
        {
            var neighboursList = new List<List<int>>();
            var neigboursDictonary = new Dictionary<int, List<int>>();

            List<int> visited = new List<int>();
            Stack<Node> toVisit = new Stack<Node>();
            toVisit.Push(root);
            visited.Add(root.Index);

            while (toVisit.Count > 0)
            {
                var node = toVisit.Pop();
                var list = new List<int>();
                foreach(var neighbour in node.Neighbours)
                {
                    if (!visited.Contains(neighbour.Index))
                    {
                        toVisit.Push(neighbour);
                        visited.Add(neighbour.Index);
                    }

                    list.Add(neighbour.Index);
                }

                list = list.OrderBy(x => x).ToList();
                neigboursDictonary.Add(node.Index, list);
            }
            
            foreach (var pair in neigboursDictonary.OrderBy(x => x.Key))
            {
                neighboursList.Add(pair.Value);
            }

            return neighboursList;
        }

        internal static void WriteNeighborsListToStream(List<List<int>> neighborsList, Stream stream)
        {
            using (var writer = new StreamWriter(stream))
            {
                writer.WriteLine(neighborsList.Count);

                foreach (var vertexNeighbors in neighborsList)
                {
                    for (int i = 0; i < vertexNeighbors.Count; i++)
                    {
                        writer.Write(vertexNeighbors[i]);
                        if (i < vertexNeighbors.Count - 1)
                        {
                            writer.Write(';');
                        }
                    }
                    writer.WriteLine();
                }
            }
        }
    }
}
