using System;
using System.Collections.Generic;
using System.Linq;

namespace Rekonstrukcja
{
    // TODO: find out why it doesn't work in some cases (returns all zeros in some rows, doesn't build full solution)
    static class TreeFinder
    {
        private static readonly bool SUPPRESSED_VERTEX = true;
        private static List<bool> suppressedVertices;
        private static int initialSize;

        /**
         * Matrix is always symmetric so it is enought to operate
         * in the upper half of the matrix throughout the whole algorithm.
         * 
         * <param name="d">Distance matrix</param>
         */
        public static double[,] FindTree(double[,] d)
        {
            int n = d.GetLength(0);
            initialSize = n;
            suppressedVertices = new List<bool>();
            for (var i = 0; i < n; i++) suppressedVertices.Add(false);
            
            for (int i = 0; n <= 2 * initialSize - 3; i++)
            {
                var Q = CalculateQMatrix(d);
                var u = FindPairWithMinimalQValue(Q);
                d = UpdateDistanceMatrix(d, u);

                n = (int) Math.Sqrt(d.Length);

                // DisplayDebug(Q, d, i);
            }

            // For odd-sized matrices there will be one not cleared initial node, clear it now
            for (int i = 0; i < initialSize; i++)
            {
                if (suppressedVertices[i] == false)
                {
                    for (int j = 0; j < n - 1; j++)
                    {
                        d[i, j] = 0;
                        d[j, i] = 0;
                    }
                }
            }

            return d;
        }

        private static int GetAmountOfSuppressedVertices()
        {
            return suppressedVertices.ToList().Where(v => v.Equals(true)).Count();
        }

        private static int GetAmountOfInitiallSuppressedVertices()
        {
            return suppressedVertices.ToList().GetRange(0, initialSize).Where(v => v.Equals(true)).Count();
        }

        private static double[,] CalculateQMatrix(double[,] d)
        {
            int allNodes = d.GetLength(0);
            int notSuppressedNodes = allNodes - GetAmountOfSuppressedVertices();
            var Q = new double[allNodes, allNodes];

            for (var i = 0; i < allNodes; i++)
            {
                if (ShouldSkipVertex(i)) continue;
                for (var j = 0; j < allNodes; j++)
                {
                    if (ShouldSkipVertex(j) || i == j) continue;

                    double sum1 = 0, sum2 = 0;
                    for (var k = 0; k < allNodes; k++)
                    {
                        sum1 += ShouldSkipVertex(k) ? 0 : d[i, k];
                        sum2 += ShouldSkipVertex(k) ? 0 : d[j, k];
                    }
                    Q[i, j] = (notSuppressedNodes - 2) * d[i, j] - sum1 - sum2;
                }
            }
            return Q;
        }

        private static Tuple<int, int> FindPairWithMinimalQValue(double[,] Q)
        {
            int n = Q.GetLength(0);
            int i = 1, j = 0;
            double minimalSoFar = Q[1, 0];

            for (var k = n - 1; k > 0; k--)
            {
                for (var l = n - 1; l > 0; l--)
                {
                    if (k != l && !ShouldSkipVertex(l) && !ShouldSkipVertex(k) && Q[k, l] < minimalSoFar)
                    {
                        minimalSoFar = Q[k, l];
                        i = k;
                        j = l;
                    }
                }
            }
            return new Tuple<int, int>(i, j);
        }

        private static void DisplayDebug(double[,] qm, double[,] dm, int round)
        {
            Console.WriteLine($"\n\n Round: {round}");
            Console.WriteLine("QM");
            var longestNumberLength = (from double item in qm select item.ToString().Length ).Max();
            Utils.DisplayMatrix(qm, longestNumberLength);

            Console.WriteLine("DM");
            longestNumberLength = (from double item in dm select item.ToString().Length).Max();
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
        private static double[,] UpdateDistanceMatrix(double[,] d, Tuple<int, int> u)
        {
            int n = d.GetLength(0);
            var newDistanceMatrix = Utils.EnlargeMatrixBy1(d);
            suppressedVertices.Add(false); // we added new vertex, so we need one more place in the list
            var distance1 = FindDistanceToNewNode(newDistanceMatrix, u);
            var distance2 = d[u.Item1, u.Item2] - distance1;

            // vertex 1 and 2 are no longer connected to anything...
            for (int i = u.Item1; i < n; i++)
            {
                newDistanceMatrix[u.Item1, i] = 0;
                newDistanceMatrix[i, u.Item1] = 0;
            }
            for (int i = u.Item2; i < n; i++)
            {
                newDistanceMatrix[u.Item2, i] = 0;
                newDistanceMatrix[i, u.Item2] = 0;
            }
            // "suppress" deleted vertices
            suppressedVertices[u.Item1] = SUPPRESSED_VERTEX;
            suppressedVertices[u.Item2] = SUPPRESSED_VERTEX;
            // ...except there is a new vertex...
            for (int i = 0; i <= n; i++)
            {
                newDistanceMatrix[i, n] = 0;
                newDistanceMatrix[n, i] = 0;
            }
            // ... connected to them.
            if (initialSize - GetAmountOfInitiallSuppressedVertices() != 1)
            {
                newDistanceMatrix[u.Item2, n] = distance1;
                newDistanceMatrix[n, u.Item2] = distance1;
                newDistanceMatrix[u.Item1, n] = distance2;
                newDistanceMatrix[n, u.Item1] = distance2;
            }
            else
            {
                newDistanceMatrix[u.Item2, n] = distance2;
                newDistanceMatrix[n, u.Item2] = distance2;
                newDistanceMatrix[u.Item1, n] = distance1;
                newDistanceMatrix[n, u.Item1] = distance1;
            }
            // calculate missing distances for new vertex
            for (int i = 0; i < n; i++)
            {
                if (!ShouldSkipVertex(i))
                {
                    newDistanceMatrix[i, n] = CalculateDistanceForOtherNodes(d, u, i);
                    newDistanceMatrix[n, i] = newDistanceMatrix[i, n];
                }
            }
            newDistanceMatrix[n, n] = 0;

            return newDistanceMatrix;
        }

        private static double FindDistanceToNewNode(double[,] d, Tuple<int, int> u)
        {
            int allNodes = d.GetLength(0) - 1;
            int notSuppressedNodes = allNodes - GetAmountOfSuppressedVertices();

            double sum1 = 0, sum2 = 0;
            for (var k = 0; k < allNodes; k++)
            {
                sum1 += ShouldSkipVertex(u.Item1) ? 0 : d[u.Item1, k];
                sum2 += ShouldSkipVertex(u.Item2) ? 0 : d[u.Item2, k];
            }
            return Math.Round(0.5 * d[u.Item1, u.Item2] + 1 / (2 * notSuppressedNodes - 4) * (sum1 - sum2));
        }

        private static double CalculateDistanceForOtherNodes(double[,] d, Tuple<int, int> u, int k)
        {
            return Math.Round(0.5 * (d[u.Item1, k] + d[u.Item2, k] - d[u.Item1, u.Item2]));
        }
    }
}
