using System;
using System.IO;

namespace RandomInputGenerator
{
    public class InputGenerator
    {
        public static double[,] GenerateRandomInput(int matrixSize)
        {
            var random = new Random();
            var matrix = new double[matrixSize, matrixSize];
            var distanceToCenter = new int[matrixSize];

            for (int i = 0; i < matrixSize; i++)
            {
                distanceToCenter[i] = random.Next(matrixSize) + 1;
            }

            for (int i = 0; i < matrixSize; i++)
            {
                for (int j = i; j < matrixSize; j++)
                {
                    if (i == j)
                    {
                        matrix[i, i] = 0;
                    }
                    else
                    {
                        matrix[i, j] = matrix[j, i] = distanceToCenter[i] + distanceToCenter[j];
                    }
                }
            }

            return matrix;
        }

        internal static void SaveInput(double[,] input, string filename)
        {
            int matrixSize = input.GetLength(0);

            using (var stream = new FileStream(filename, FileMode.Create))
            {
                using (var writer = new StreamWriter(stream))
                {
                    for (int i = 0; i < matrixSize; i++)
                    {
                        for (int j = 0; j < matrixSize; j++)
                        {
                            writer.Write(input[i, j]);
                            if (j < matrixSize - 1)
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
}