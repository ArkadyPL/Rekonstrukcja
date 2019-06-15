using System;
using System.Collections.Generic;
using System.Linq;

namespace Rekonstrukcja
{
    class TreeFinder
    {
        private readonly NodeBuilder nodeBuilder = new NodeBuilder();
        public Node FindTree(int[,] distancesBetweenLeaves)
        {
            List<Node> subTrees = this.InitiateSubTrees(distancesBetweenLeaves.GetLength(0));
            List<Tuple<Node, Node, int>> distancesBetweenSubTrees = this.InitiateDistancesBetweenSubTrees(subTrees, distancesBetweenLeaves);

            while (subTrees.Count > 1)
            {
                var pair = distancesBetweenSubTrees.OrderBy(x => x.Item3).First();
                distancesBetweenSubTrees.Remove(pair);
                pair.Deconstruct(out Node subTree1, out Node subTree2, out int distance);

                var firstNewNode = this.JoinSubTrees(subTree1, subTree2, distance);
                Node newSubTreeRoot = this.FindNewRoot(subTree1, subTree2, firstNewNode, distance, distancesBetweenSubTrees, out var distanceFromSubTree1);

                this.UpdateSubTrees(subTrees, newSubTreeRoot, subTree1, subTree2);
                distancesBetweenSubTrees = this.UpdateDistancesBetweenSubTrees(distancesBetweenSubTrees, newSubTreeRoot, subTree1, subTree2, distance, distanceFromSubTree1);
            }

            return subTrees[0];
        }

        private List<Tuple<Node, Node, int>> UpdateDistancesBetweenSubTrees(
            List<Tuple<Node, Node, int>> distancesBetweenSubTrees, 
            Node newSubTree,
            Node subTree1,
            Node subTree2,
            int distance,
            int distanceFromSubTree1)
        {
            var newDistances = new List<Tuple<Node, Node, int>>();

            foreach(var pair in distancesBetweenSubTrees)
            {
                if (!(pair.Item1.Index == subTree1.Index || pair.Item1.Index == subTree2.Index ||
                    pair.Item2.Index == subTree1.Index || pair.Item2.Index == subTree2.Index))
                {
                    newDistances.Add(pair);
                }

                if (pair.Item1.Index == subTree1.Index)
                {
                    newDistances.Add(new Tuple<Node, Node, int>(newSubTree, pair.Item2, pair.Item3 - distanceFromSubTree1));
                }

                if (pair.Item2.Index == subTree1.Index)
                {
                    newDistances.Add(new Tuple<Node, Node, int>(newSubTree, pair.Item1, pair.Item3 - distanceFromSubTree1));
                }
            }

            return newDistances;
        }

        private void UpdateSubTrees(List<Node> subTrees, Node newSubTreeRoot, Node subTree1, Node subTree2)
        {
            subTrees.Remove(subTree1);
            subTrees.Remove(subTree2);
            subTrees.Add(newSubTreeRoot);
        }

        private Node FindNewRoot(Node subTree1, Node subTree2, Node firstNewNode, int distance, List<Tuple<Node, Node, int>> distancesBetweenSubTrees, out int distanceFromSubTree1)
        {
            if (distancesBetweenSubTrees.Count == 0)
            {
                distanceFromSubTree1 = 0;
                return subTree1;
            }
            var distance1 = distancesBetweenSubTrees.Find(x => x.Item1.Index == subTree1.Index || x.Item2.Index == subTree1.Index).Item3;
            var distance2 = distancesBetweenSubTrees.Find(x => x.Item1.Index == subTree2.Index || x.Item2.Index == subTree2.Index).Item3;

            distanceFromSubTree1 = (distance + distance1 - distance2) / 2;
            if (distanceFromSubTree1 == 0)
            {
                return subTree1;
            }

            var previousNode = subTree1;
            var currentNode = firstNewNode;
            for (int i = 1; i < distanceFromSubTree1; i++)
            {
                var tmp = currentNode;
                currentNode = currentNode.Neighbours.Find(x => x.Index != previousNode.Index);
                previousNode = tmp;
            }

            return currentNode;
        }

        private Node JoinSubTrees(Node subTree1, Node subTree2, int distance)
        {
            var previousTree = subTree2;
            for (int i = 0; i < distance - 1; i++)
            {
                var currentTree = nodeBuilder.GetNode();
                previousTree.Neighbours.Add(currentTree);
                currentTree.Neighbours.Add(previousTree);

                previousTree = currentTree;
            }

            previousTree.Neighbours.Add(subTree1);
            subTree1.Neighbours.Add(previousTree);

            return previousTree;
        }

        private List<Tuple<Node, Node, int>> InitiateDistancesBetweenSubTrees(List<Node> subTrees, int[,] distancesBetweenLeaves)
        {
            var distances = new List<Tuple<Node, Node, int>>();

            for (int i = 0; i < subTrees.Count; i++)
            {
                for (int j = 0; j < i; j++)
                {
                    distances.Add(new Tuple<Node, Node, int>(subTrees[i], subTrees[j], distancesBetweenLeaves[i, j]));
                }
            }

            return distances;
        }

        private List<Node> InitiateSubTrees(int leavesCount)
        {
            var subTrees = new List<Node>();
            for (int i = 0; i < leavesCount; i++)
            {
                subTrees.Add(nodeBuilder.GetNode());
            }

            return subTrees;
        }
    }
}
