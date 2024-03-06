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
            FindPaths(RouteSoFar1);
            // longestcalcs
            foreach (var route in allRoutes)
            {
                dumpRoute(route);
                if (route.Count > longestRoute.Count)
                {
                    longestRoute = route;
                }
            }
            dumpRoute(longestRoute);

            return longestRoute;

        }

        //Helper function to dump a route
        private void dumpRoute(List<int> route)
        {
            Console.Write("Route: ");
            foreach (int r in route)
                Console.Write(r + " ");
            Console.WriteLine("");
        }

        private void FindPaths(List<int> RouteSoFar)
        {
            int currentNode = RouteSoFar.Last(); // What was the last node?
            // find the neighbours of the current node
            List<int> neighbours = buildNeighbourlist(currentNode);
            bool validNextFound = false;

            // scan over all the neighbours
            foreach (int a in neighbours)
            {
                // Have we visited this previously
                if (!RouteSoFar.Contains(a))
                {
                    // Then this is a node to test on our route
                    validNextFound = true;
                    // Make a copy of the existing list
                    List<int> RouteSoFar1 = new List<int>(RouteSoFar);
                    // add the neighbout to it
                    RouteSoFar1.Add(a);
                    dumpRoute(RouteSoFar1);
                    // now test for this list
                    FindPaths(RouteSoFar1);
                }
            }

            if (!validNextFound)
            {
                Console.WriteLine("No new neighbour was found - therefore at end of a route");
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
