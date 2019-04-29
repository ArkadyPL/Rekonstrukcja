using System;

namespace Rekonstrukcja
{
    static class TreeFinder
    {
        /**
         * Matrix is always symmetric so it is enought to operate
         * in the upper half of the matrix throughout the whole algorithm.
         * 
         * <param name="d">Distance matrix</param>
         */
        public static int[,] FindTree(int[,] d)
        {
            var result = CreateInitialAdjacencyMatrix(d);
            var Q = CalculateQMatrix(d);
            var u = FindPairWithMinimalQValue(Q);
            result = ExtendAdjacencyMatrixWithNewVertex(result, u);
            UpdateDistanceMatrix(d, u);
            // TODO: perform remaining calculations
            return result;
        }

        /**
         * Creates initial adjecancy matrix which is completely empty,
         * because we do not know, what will leaves be connected to.
         */
        private static int[,] CreateInitialAdjacencyMatrix(int[,] d)
        {
            int n = (int) Math.Sqrt(d.Length);
            var initialAdjacencyMatrix = new int[n, n];
            for (var i = 0; i < n; i++)
            {
                for (var j = 0; j < n; j++)
                {
                    initialAdjacencyMatrix[i, j] = d[i, j];
                }
            }
            return initialAdjacencyMatrix;
        }

        /**
         * <param name="adjacencyMatrix">Adjecacency Matrix which is meant to be extended with new vertex.</param>
         * <param name="u">Pair of vertices for which a vertex will be created between them.</param>
         */
        private static int[,] ExtendAdjacencyMatrixWithNewVertex(int[,] adjacencyMatrix, Tuple<int, int> u)
        {
            int n = (int)Math.Sqrt(adjacencyMatrix.Length);
            var newAdjacencyMatrix = Utils.EnlargeMatrixBy1(adjacencyMatrix);
            var distance1 = FindDistanceToNewNode(adjacencyMatrix, u);
            var distance2 = adjacencyMatrix[u.Item1, u.Item2] - distance1;
            // vertex 1 and 2 are no longer connected...
            newAdjacencyMatrix[u.Item1, u.Item2] = 0;
            // ...but there is a connection through the new vertex
            newAdjacencyMatrix[u.Item1, n] = distance1;
            newAdjacencyMatrix[u.Item2, n] = distance2;
            return newAdjacencyMatrix;
        }

        private static int FindDistanceToNewNode(int[,] adjacencyMatrix, Tuple<int, int> u)
        {
            int n = (int) Math.Sqrt(adjacencyMatrix.Length);
            int sum1 = 0, sum2 = 0;
            for (var k = 0; k < n; k++)
            {
                sum1 += adjacencyMatrix[u.Item1, k];
                sum2 += adjacencyMatrix[u.Item2, k];
            }
            return (int) 0.5 * adjacencyMatrix[u.Item1, u.Item2] + 1 / (2 * n - 4) * (sum1 - sum2);
        }

        private static int[,] CalculateQMatrix(int[,] d)
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

        private static Tuple<int, int> FindPairWithMinimalQValue(int[,] Q)
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

        private static int[,] UpdateDistanceMatrix(int[,] d, Tuple<int, int> u)
        {
            int n = (int) Math.Sqrt(d.Length);
            var newDistanceMatrix = new int[n - 1, n - 1];
            // perform calculations
            return newDistanceMatrix;
        }
    }
}
