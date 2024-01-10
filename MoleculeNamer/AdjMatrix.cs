using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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

        public List<string> FindLongest()
        {
            // build list of all routes from startnode
            List<List<string>> allRoutes = new List<List<string>>(); //list will hold all  of the routes in one place
            List<string> route = new List<string>();
            List<List<string>> allRouteCombinations = new List<List<string>>(); // intermediate list space

            // Depth first traversal of the adjacency matrix

            // find longest total route
            List<string> junctions = new List<string>();
            int[] connections = FindConections();
            List<string> longestChain = new List<string>();  // longest chain = {start node,...,end node}
            return longestChain;
        }
        private int[] FindConections()
        {
            int width = matrix.GetLength(1);
            int height = matrix.GetLength(0);
            int[] connectionsarray = new int[height];//finds the number of connections each  node has
            for (int j = 0; j <= height; j++)
            {

                int connections = 0;

                for (int i = 0; i <= width; i++)
                {
                    if (matrix[i, j] == '1')
                    {
                        connections++;
                    }

                }
                connectionsarray[j] = connections;
            }
            return connectionsarray;
        }
    }
}
