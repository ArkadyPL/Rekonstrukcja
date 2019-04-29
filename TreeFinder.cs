using System;
using System.Linq;

namespace Rekonstrukcja
{
    static class TreeFinder
    {
        private static readonly bool SUPPRESSED_VERTEX = true;
        private static bool[] suppressedVertices;
        private static int initialSize;

        /**
         * Matrix is always symmetric so it is enought to operate
         * in the upper half of the matrix throughout the whole algorithm.
         * 
         * <param name="d">Distance matrix</param>
         */
        public static int[,] FindTree(int[,] d)
        {
            int n = (int) Math.Sqrt(d.Length);
            initialSize = n;
            // TODO: calculate maximal necessary size of the list
            suppressedVertices = new bool[2 * initialSize];

            // TODO: make sure the condition is correct
            for (int i = 0; n != getAmountOfSuppressedVertices() + 2; i++)
            {
                var Q = CalculateQMatrix(d, i);
                var u = FindPairWithMinimalQValue(Q);
                d = UpdateDistanceMatrix(d, u);

                n = (int) Math.Sqrt(d.Length);
                DisplayDebug(Q, d, i);
            }
            return d;
        }

        private static int getAmountOfSuppressedVertices()
        {
            return suppressedVertices.ToList().Where(v => v.Equals(true)).Count();
        }

        private static int[,] CalculateQMatrix(int[,] d, int round)
        {
            int n = (int) Math.Sqrt(d.Length);
            var Q = new int[n, n];

            for (var i = 0; i < n; i++)
            {
                if (ShouldSkipVertex(i)) continue;
                for (var j = 0; j < n; j++)
                {
                    if (ShouldSkipVertex(j) || i == j) continue;

                    int sum1 = 0, sum2 = 0;
                    for (var k = 0; k < n; k++)
                    {
                        sum1 += ShouldSkipVertex(k) ? 0 : d[i, k];
                        sum2 += ShouldSkipVertex(k) ? 0 : d[j, k];
                    }
                    Q[i, j] = (n - 2 - (2 * round)) * d[i, j] - sum1 - sum2;
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
                for (var l = 0; l < n; l++)
                {
                    if (!ShouldSkipVertex(l) && Q[k, l] < minimalSoFar)
                    {
                        minimalSoFar = Q[k, l];
                        i = k;
                        j = l;
                    }
                }
            }
            return new Tuple<int, int>(i, j);
        }

        private static void DisplayDebug(int[,] qm, int[,] dm, int round)
        {
            Console.WriteLine($"\n\n Round: {round}");
            Console.WriteLine("QM");
            var longestNumberLength = (from int item in qm select item.ToString().Length ).Max();
            Utils.DisplayMatrix(qm, longestNumberLength);

            Console.WriteLine("DM");
            longestNumberLength = (from int item in dm select item.ToString().Length).Max();
            Utils.DisplayMatrix(dm, longestNumberLength);

            Console.WriteLine("SM");
            Console.WriteLine("[" + String.Join(", ", suppressedVertices.ToArray()) + "]");
        }

        private static bool ShouldSkipVertex(int i)
        {
            return suppressedVertices[i] == SUPPRESSED_VERTEX;
        }

        /**
         * In the theoretical algorithm matrix is reduced by 1 at this point.
         * However here, we add new column instead, and "suppress" 2 columns that are meant to be deleted.
         * This way we don't have to care about changing indecies because they always remain the same.
         */
        private static int[,] UpdateDistanceMatrix(int[,] d, Tuple<int, int> u)
        {
            int n = (int) Math.Sqrt(d.Length);
            var newDistanceMatrix = Utils.EnlargeMatrixBy1(d);
            var distance1 = FindDistanceToNewNode(newDistanceMatrix, u);
            var distance2 = newDistanceMatrix[u.Item1, u.Item2] - distance1;
            // vertex 1 and 2 are no longer connected to anything...
            for (int i = u.Item1; i < initialSize; i++) newDistanceMatrix[u.Item1, i] = 0;
            for (int i = u.Item1; i < initialSize; i++) newDistanceMatrix[i, u.Item1] = 0;
            for (int i = u.Item2; i < initialSize; i++) newDistanceMatrix[u.Item2, i] = 0;
            for (int i = u.Item2; i < initialSize; i++) newDistanceMatrix[i, u.Item2] = 0;
            // ...except there is a new vertex...
            for (int i = 0; i <= n; i++)
            {
                newDistanceMatrix[i, n] = 0;
                newDistanceMatrix[n, i] = 0;
            }
            // ... connected to them.
            newDistanceMatrix[u.Item1, n] = distance1;
            newDistanceMatrix[n, u.Item1] = distance1;
            newDistanceMatrix[u.Item2, n] = distance2;
            newDistanceMatrix[n, u.Item2] = distance2;
            // calculate missing distances for new vertex
            for (int i = 0; i < n; i++)
            {
                if (i != u.Item1 && i != u.Item2 && !ShouldSkipVertex(i))
                {
                    newDistanceMatrix[i, n] = CalculateDistanceForOtherNodes(d, u, i);
                    newDistanceMatrix[n, i] = newDistanceMatrix[i, n];
                }
            }
            newDistanceMatrix[n, n] = 0;
            // "suppress" deleted vertices
            suppressedVertices[u.Item1] = SUPPRESSED_VERTEX;
            suppressedVertices[u.Item2] = SUPPRESSED_VERTEX;
            return newDistanceMatrix;
        }
        
        private static int FindDistanceToNewNode(int[,] d, Tuple<int, int> u)
        {
            int n = (int)Math.Sqrt(d.Length) - 1;
            int? sum1 = 0, sum2 = 0;
            for (var k = 0; k < n; k++)
            {
                sum1 += ShouldSkipVertex(u.Item1) ? 0 : d[u.Item1, k];
                sum2 += ShouldSkipVertex(u.Item2) ? 0 : d[u.Item2, k];
            }
            return (int)(0.5 * d[u.Item1, u.Item2] + 1.0 / (2 * n - 4) * (sum1 - sum2));
        }

        private static int CalculateDistanceForOtherNodes(int[,] d, Tuple<int, int> u, int k)
        {
            var x = (int)(0.5 * (d[u.Item1, k] + d[u.Item2, k] - d[u.Item1, u.Item2]));
            return x;
        }
    }
}
