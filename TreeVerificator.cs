using System;
using System.Collections.Generic;
using System.ComponentModel.Design.Serialization;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Rekonstrukcja
{
    public class TreeVerificator
    {
        public static bool VerifyTree(Node tree, int[,] distanceMatrix)
        {
            var result = true;
            for (int i = 0; i < distanceMatrix.GetLength(0); i++)
            {
                for (int j = 0; j < i; j++)
                {
                    var expectedDistance = distanceMatrix[i, j];
                    int foundDistance = 0;
                    var start = FindVertex(tree, i, new List<Node>());
                    var end = FindVertex(start, j, new List<Node>(), ref foundDistance);

                    if (expectedDistance != foundDistance)
                    {
                        result = false;
                    }
                }
            }

            return result;
        }

        private static Node FindVertex(Node root, int index, List<Node> visitedVertices)
        {
            int placeholder = 0;
            return FindVertex(root, index, visitedVertices, ref placeholder);
        }

        private static Node FindVertex(Node root, int index, List<Node> visitedVertices, ref int distance)
        {
            if (root.Index == index)
            {
                return root;
            }

            if (root.IsLeaf && visitedVertices.Any())
            {
                return null;
            }

            visitedVertices.Add(root);
            //distance++;
            foreach (var node in root.Neighbours.Except(visitedVertices))
            {
                var newDistance = distance + 1;
                var result = FindVertex(node, index, visitedVertices, ref newDistance);
                if (result != null)
                {
                    distance = newDistance;
                    return result;
                }
            }

            return null;
        }
    }
}
