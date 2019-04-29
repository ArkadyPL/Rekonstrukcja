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
         */
        public static int[,] findTree(int[,] d)
        {
            var Q = calculateQMatrix(d);
            // TODO: perform remaining calculations
            return d;
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
    }
}
