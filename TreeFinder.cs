﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace Rekonstrukcja
{
    public class TreeFinder
    {
        private readonly NodeBuilder nodeBuilder = new NodeBuilder();
        public Node FindTree(int[,] distancesBetweenLeaves)
        {
            List<Node> subTrees = this.InitiateSubTrees(distancesBetweenLeaves.GetLength(0));
            List<Tuple<Node, Node, int>> distancesBetweenSubTrees = this.InitiateDistancesBetweenSubTrees(subTrees, distancesBetweenLeaves);

            while (subTrees.Count > 1)
            {
                var pair = this.PickPairToConnect(distancesBetweenSubTrees);
                distancesBetweenSubTrees.Remove(pair);
                pair.Deconstruct(out Node subTree1, out Node subTree2, out int distance);

                var firstNewNode = this.JoinSubTrees(subTree1, subTree2, distance);
                Node newSubTreeRoot = this.FindNewRoot(subTree1, subTree2, firstNewNode, distance, distancesBetweenSubTrees, out var distanceFromSubTree1);

                this.UpdateSubTrees(subTrees, newSubTreeRoot, subTree1, subTree2);
                distancesBetweenSubTrees = this.UpdateDistancesBetweenSubTrees(
                    distancesBetweenSubTrees, 
                    newSubTreeRoot, 
                    subTree1, 
                    subTree2, 
                    distanceFromSubTree1);
            }

            return subTrees[0];
        }

        private Tuple<Node, Node, int> PickPairToConnect(List<Tuple<Node, Node, int>> distancesBetweenSubTrees)
        {
            //var minDistance = distancesBetweenSubTrees.Min(x => x.Item3);
            var potentialPairs = distancesBetweenSubTrees.OrderBy(x => x.Item3);
            
            foreach(var pair in potentialPairs)
            {
                pair.Deconstruct(out Node subTree1, out Node subTree2, out int distance);

                var oldDistance = -1;
                var correct = true;
                foreach (var otherPair in distancesBetweenSubTrees.Where(x => x != pair && (x.Item1.Index == subTree1.Index || x.Item2.Index == subTree1.Index)))
                {
                    int otherNodeIndex;
                    if (otherPair.Item1.Index == subTree1.Index)
                    {
                        otherNodeIndex = otherPair.Item2.Index;
                    }
                    else
                    {
                        otherNodeIndex = otherPair.Item1.Index;
                    }

                    var distance1 = otherPair.Item3;
                    var distance2 = distancesBetweenSubTrees.Find(x => (x.Item1.Index == subTree2.Index && x.Item2.Index == otherNodeIndex) ||
                        (x.Item1.Index == otherNodeIndex && x.Item2.Index == subTree2.Index)).Item3;

                    var distanceFromSubTree1 = (distance + distance1 - distance2) / 2;
                    if (oldDistance != -1 && oldDistance != distanceFromSubTree1)
                    {
                        correct = false;
                    }
                    oldDistance = distanceFromSubTree1;
                }

                if (correct)
                {
                    return pair;
                }
            }

            throw new Exception("Wrong input table!");
        }

        private List<Tuple<Node, Node, int>> UpdateDistancesBetweenSubTrees(
            List<Tuple<Node, Node, int>> distancesBetweenSubTrees, 
            Node newSubTree,
            Node subTree1,
            Node subTree2,
            int distanceFromSubTree1)
        {
            var newDistances = new List<Tuple<Node, Node, int>>();
            var distances = new Dictionary<int, int>();
            foreach(var pair in distancesBetweenSubTrees)
            {
                if (pair.Item1.Index == subTree1.Index)
                {
                    newDistances.Add(new Tuple<Node, Node, int>(newSubTree, pair.Item2, pair.Item3 - distanceFromSubTree1));
                    distances.Add(pair.Item2.Index, pair.Item3 - distanceFromSubTree1);
                }

                if (pair.Item2.Index == subTree1.Index)
                {
                    newDistances.Add(new Tuple<Node, Node, int>(pair.Item1, newSubTree, pair.Item3 - distanceFromSubTree1));
                    distances.Add(pair.Item1.Index, pair.Item3 - distanceFromSubTree1);
                }
            }

            foreach(var pair in distancesBetweenSubTrees)
            {
                if (!(pair.Item1.Index == subTree1.Index || pair.Item1.Index == subTree2.Index ||
                    pair.Item2.Index == subTree1.Index || pair.Item2.Index == subTree2.Index))
                {
                    if (pair.Item3 > distances[pair.Item1.Index] + distances[pair.Item2.Index])
                    {
                        newDistances.Add(new Tuple<Node, Node, int>(pair.Item1, pair.Item2, distances[pair.Item1.Index] + distances[pair.Item2.Index]));
                    }
                    else
                    {
                        newDistances.Add(pair);
                    }
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

            var otherPair = distancesBetweenSubTrees.OrderBy(x => x.Item3).First(x => x.Item1.Index == subTree1.Index || x.Item2.Index == subTree1.Index);
            int otherNodeIndex;
            if (otherPair.Item1.Index == subTree1.Index)
            {
                otherNodeIndex = otherPair.Item2.Index;
            }
            else
            {
                otherNodeIndex = otherPair.Item1.Index;
            }

            var distance1 = otherPair.Item3;
            var distance2 = distancesBetweenSubTrees.Find(x => (x.Item1.Index == subTree2.Index && x.Item2.Index == otherNodeIndex) ||
                (x.Item1.Index == otherNodeIndex && x.Item2.Index == subTree2.Index)).Item3;

            distanceFromSubTree1 = (distance + distance1 - distance2) / 2;

            if ((distance + distance1 - distance2) % 2 != 0 || distanceFromSubTree1 < 0)
            {
                throw new Exception("Wrong input table!");
            }

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
                subTrees.Add(nodeBuilder.GetNode(true));
            }

            return subTrees;
        }
    }
}
