namespace Rekonstrukcja
{
    public class NodeBuilder
    {
        private int currentIndex = 0;

        public Node GetNode(bool isLeaf = false)
        {
            return new Node(currentIndex++, isLeaf);
        }
    }
}
