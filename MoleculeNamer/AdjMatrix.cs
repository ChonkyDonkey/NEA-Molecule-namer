using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.ExceptionServices;
using System.Text;
using System.Threading.Tasks;

namespace MoleculeNamer
{
    public class AdjMatrix
    {
        List<List<int>> allRoutes = new List<List<int>>();
        List<int >visited = new List<int>(); //list will hold all  of the routes in one place
        int?[,] matrix; // The adjacency matrix is a 2D array mapping arcs between start and end nodes
        int count = 0;


        public AdjMatrix()
        {
            //Default Constructor
        }

        public AdjMatrix(int?[,] _adj, int _count)
        {
            matrix = _adj;
            count = _count;
        }

        public AdjMatrix(Graph _graph)
        {
            matrix = _graph.CreateAdjMatrix();
            count = _graph.getNumNodes();
        }

        public void addGraph(Graph _graph)
        {
            matrix = _graph.CreateAdjMatrix();
            count = _graph.getNumNodes();
        }

        public int getNumNodes()
        {
            return count;
        }

        public bool isLinked(int i, int j)
        {
            return (matrix[i, j] is not null);
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

        public List<int> FindLongest()
        {
            // build list of all routes from startnode

            List<int> longestRoute = new List<int>();
            List<List<string>> allRouteCombinations = new List<List<string>>(); // intermediate list space

            // Build list of routes from master
            // Using Depth first traversal of the adjacency matrix
            
            List<int> RouteSoFar1 = new List<int>();
            RouteSoFar1.Add(0);
            visited.Add(0);
            FindPath(RouteSoFar1);
            // longestcalcs
            foreach (var sublist in allRoutes)
            {
                foreach (var obj in sublist)
                {
                    Console.WriteLine(obj);
                }
            }
            foreach(int a in visited){Console.WriteLine("visited : " + visited[a]);}
            return longestRoute;

        }
        private void FindPath(List<int> RouteSoFar)
        {
            int currentNode = RouteSoFar.Last();
            List<int> neighbours = buildNeighbourlist(currentNode);
            bool validNextFound = false;

            foreach (int a in neighbours)
            {
                if (!RouteSoFar.Contains(a)&& !visited.Contains(a))
                {
                    validNextFound = true;
                    visited.Add(a);
                    List<int> RouteSoFar1 = RouteSoFar;
                    RouteSoFar1.Add(a);
                    foreach(int r in RouteSoFar)
                    Console.WriteLine("route: " + r);
                    FindPath(RouteSoFar1);
                }
            }

            if (!validNextFound)
            {
                allRoutes.Add(RouteSoFar);
                Console.WriteLine(allRoutes.Count);
            }
        }

        private List<int> buildNeighbourlist(int currentNode)
        {
            List<int> neighbourList = new List<int>();
            for (int i = 0; i < matrix.GetLength(1); i++)
            {
                if (matrix[currentNode, i] == 1)//checks the matrix along the row of the current node loooking fo connections
                {
                    neighbourList.Add(i);
                }
            }
            
            return neighbourList;
        }
        private int[] FindConections()
        {
            int width = matrix.GetLength(1);
            int height = matrix.GetLength(0);
            int[] connectionsarray = new int[height]; //finds the number of connections each  node has
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
