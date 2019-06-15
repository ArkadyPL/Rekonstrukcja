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
