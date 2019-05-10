using System;
using System.Collections.Generic;
using System.IO;

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

        internal static List<List<int>> ConvertMatrixToNeighborsList(int[,] result)
        {
            var resultMatrixSize = result.GetLength(0);
            var neighborsList = new List<List<int>>(resultMatrixSize);
            for (int i = 0; i < resultMatrixSize; i++)
            {
                neighborsList.Add(new List<int>());
            }
            var newVertexIndex = resultMatrixSize;
            for (int i = 0; i < resultMatrixSize; i++)
            {
                for (int k = i + 1; k < resultMatrixSize; k++)
                {
                    if (result[i, k] == 1)
                    {
                        neighborsList[i].Add(k);
                        neighborsList[k].Add(i);
                    }
                    if (result[i,k] > 1)
                    { 
                        for (int l = 1; l < result[i,k]; l++)
                        {
                            neighborsList.Add(new List<int>());
                            if (l == 1)
                            {
                                neighborsList[i].Add(newVertexIndex);
                                neighborsList[newVertexIndex].Add(i);
                            }
                            else
                            {
                                neighborsList[newVertexIndex].Add(newVertexIndex - 1);
                            }

                            if (l == result[i,k] - 1)
                            {
                                neighborsList[k].Add(newVertexIndex);
                                neighborsList[newVertexIndex].Add(k);
                            }
                            else
                            {
                                neighborsList[newVertexIndex].Add(newVertexIndex + 1);
                            }

                            newVertexIndex++;
                        }
                    }
                }
            }
            return neighborsList;
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
