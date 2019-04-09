using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rekonstrukcja
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Tree reconstruction");
            int[,] input = readInput();
            int[,] result = findTree(input);
            outputResult(result);
        }

        static int[,] readInput()
        {
            // TODO: read input from file
            // TODO: display input
            return new int[10, 10];
        }

        static int[,] findTree(int[,] input)
        {
            // TODO: perform calculations
            return input;
        }

        static void outputResult(int[,] result)
        {
            // TODO: display result
            // TODO: save result to the file
        }
    }
}
