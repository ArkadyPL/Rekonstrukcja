using System.Collections.Generic;

namespace Rekonstrukcja
{
    public class Node
    {
        public int Index { get; set; }
        public List<Node> Neighbours { get; set; } = new List<Node>();
        public bool IsLeaf { get; set; }

        public Node(int index, bool isLeaf)
        {
            this.Index = index;
            this.IsLeaf = isLeaf;
        }

        public override string ToString()
        {
            return $"Node #{Index}, isLeaf: {IsLeaf}, # of neighbours: {Neighbours.Count}";
        }
    }
}
