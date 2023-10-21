using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoleculeNamer
{
    public class AdjMatrix
    {
        int?[,] matrix; // The adjacency matrix is a 2D array mapping arcs between start and end nodes
        int count;

        public AdjMatrix(int?[,] _adj, int _count)
        {
            matrix = _adj;
            count = _count;
        }

        public void PrintMatrix()
        {
            Console.Write("       ");
            for (int i = 0; i < count; i++)
            {
                Console.Write("{0}  ", (char)('0' + i));
            }

            Console.WriteLine();

            for (int i = 0; i < count; i++)
            {
                Console.Write("{0} | [ ", (char)('0' + i));

                for (int j = 0; j < count; j++)
                {
                    if (i == j)
                    {
                        Console.Write(" x,");
                    }
                    else if (matrix[i, j] == null)
                    {
                        Console.Write(" .,");
                    }
                    else
                    {
                        Console.Write(" {0},", matrix[i, j]);
                    }

                }
                Console.Write(" ]\r\n");
            }
            Console.Write("\r\n");
        }


    }
}
