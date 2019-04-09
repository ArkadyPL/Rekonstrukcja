using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rekonstrukcja
{
    class Utils
    {    

        public static void DisplayMatrix(int[,] matrix, int longestNumberLength)
        {
            int n = (int) Math.Sqrt(matrix.Length);
            for (var i = 0; i < n; i++)
            {
                Console.Write("| ");
                for (var j = 0; j < n; j++)
                {
                    Console.Write(matrix[i, j]);
                    for (var k = 0; k < longestNumberLength - NumberLength(matrix[i, j]) + 1; k++)
                    {
                        Console.Write(" ");
                    }
                }
                Console.WriteLine("|");
            }
        }

        private static int NumberLength(int number)
        {
            return number.ToString().Length;
        }

    }
}
