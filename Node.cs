using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
