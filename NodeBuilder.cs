using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rekonstrukcja
{
    public class NodeBuilder
    {
        private int currentIndex = 0;

        public Node GetNode()
        {
            return new Node(currentIndex++);
        }
    }
}
