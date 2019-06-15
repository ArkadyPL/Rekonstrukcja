using System.Collections.Generic;

namespace Rekonstrukcja
{
    public class Node
    {
        public int Index { get; set; }
        public List<Node> Neighbours { get; set; } = new List<Node>();

        public Node(int index)
        {
            this.Index = index;
        }
    }
}
