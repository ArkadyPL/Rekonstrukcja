using System;
using System.IO;

namespace RandomInputGenerator
{
    internal class InputGenerator
    {
        internal static void GenerateRandomInput(int matrixSize, string filename)
        {
            var random = new Random();
            var matrix = new int[matrixSize, matrixSize];
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

            using (var stream = new FileStream(filename, FileMode.Create))
            {
                using (var writer = new StreamWriter(stream))
                {
                    for (int i = 0; i < matrixSize; i++)
                    {
                        for (int j = 0; j < matrixSize; j++)
                        {
                            writer.Write(matrix[i, j]);
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