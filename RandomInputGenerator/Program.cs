using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RandomInputGenerator
{
    class Program
    {
        static void Main(string[] args)
        {
            var matrixSize = 5;
            var fileName = "input.txt";
            if (args.Length > 0)
            {
                try
                {
                    matrixSize = int.Parse(args[0]);
                    fileName = args[1];
                }
                catch (IndexOutOfRangeException)
                {
                    Console.WriteLine("Incorrect number of arguments.");
                    return;
                }
                catch (FormatException)
                {
                    Console.WriteLine("Wrong arguments format.");
                    return;
                }
            }
            InputGenerator.GenerateRandomInput(matrixSize, fileName);
        }
    }
}
