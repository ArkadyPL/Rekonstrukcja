using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rekonstrukcja
{
    static class TreeFinder
    {
        /**
         * Matrix is always symmetric so it is enought to operate
         * in the upper half of the matrix throughout the whole algorithm.
         * 
         * d - stands for distanceMatrix
         */
        public static int[,] findTree(int[,] d)
        {
            var result = new int[0,0];
            var Q = calculateQMatrix(d);
            Tuple<int, int> u = findPairWithMinimalQValue(Q);
            result = extendAdjacencyMatrix(result, u);
            updateDistanceMatrix(d, u);
            // TODO: perform remaining calculations
            return result;
        }

        private static int[,] extendAdjacencyMatrix(int[,] matrix, Tuple<int, int> newPair)
        {
            // todo: add new connection to the matrix
            return matrix;
        }

        private static int[,] calculateQMatrix(int[,] d)
        {
            int n = (int) Math.Sqrt(d.Length);
            var Q = new int[n, n];

            for (var i = 0; i < n; i++)
            {
                for (var j = i + 1; j < n; j++)
                {
                    int sum1 = 0, sum2 = 0;
                    for (var k = 0; k < n; k++)
                    {
                        sum1 += d[i, k];
                        sum2 += d[j, k];
                    }
                    Q[i, j] = (n - 2) * d[i, j] - sum1 - sum2;
                }
            }
            return Q;
        }

        private static Tuple<int, int> findPairWithMinimalQValue(int[,] Q)
        {
            int n = (int) Math.Sqrt(Q.Length);
            int i = 1, j = 0;
            int minimalSoFar = Q[1, 0];
            for (var k = 0; k < n; k++)
            {
                for (var l = k + 1; l < n; l++)
                {
                    if (Q[k, l] < minimalSoFar)
                    {
                        minimalSoFar = Q[k, l];
                        i = k;
                        j = l;
                    }
                }
            }
            return new Tuple<int, int>(i, j);
        }

        private static int[,] updateDistanceMatrix(int[,] d, Tuple<int, int> u)
        {
            int n = (int) Math.Sqrt(d.Length);
            var newDistanceMatrix = new int[n + 1, n + 1];
            // perform calculations
            return newDistanceMatrix;
        }
    }
}
