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
        List<List<int>> allRouteCombinations = new List<List<int>>(); // intermediate list space

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
            
            // Build list of routes from master
            // Using Depth first traversal of the adjacency matrix
            List<int> RouteSoFar1 = new List<int>();
            RouteSoFar1.Add(0);
            FindPaths(RouteSoFar1);
            // longestcalcs
            dumpRoute(longestRoute);
            mergepaths();

            foreach (var route in allRouteCombinations)
            {
                dumpRoute(route);
                if (route.Count > longestRoute.Count)
                {
                    longestRoute = route;
                }
            }
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

        private void mergepaths(){
            foreach(List<int>Primary in allRoutes){
                allRouteCombinations.Add(Primary);// copies allroutes previously found to thenew list
                
                foreach(List<int> secondary in allRoutes){
                    int matchpos=0;
                    int i = 0;
                    if(secondary != Primary){ // makes sure it doesnt compare the identical lists

                        while(Primary[i] == secondary[i])
                        {
                            matchpos = i;                            
                            i++;
                        }
                        //creates the merged route
                        List<int> mergedroute = [.. countBackward(matchpos,Primary), .. countForward(matchpos,secondary)];
                        dumpRoute(mergedroute);
                        allRouteCombinations.Add(mergedroute);
                    }
                }
            }
            
        }
        private List<int> countForward(int matchpos, List<int>route){
            List<int> forwardsection = new List<int>();
            for(int i = matchpos;i <= route.Count-1;i++){
                forwardsection.Add(route[i]);
            }
            return forwardsection;
        }
        private List<int> countBackward(int matchpos,List<int>route){
            List<int> backwardsection = new List<int>();
            for(int i = matchpos+1;i <= route.Count-1;i++){
                backwardsection.Add(route[i]);
            }
            backwardsection.Reverse();
            return backwardsection;
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
