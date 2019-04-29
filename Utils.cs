using System;

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

    }
}
