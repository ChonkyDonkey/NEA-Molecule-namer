﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection.Metadata;
using System.Runtime.CompilerServices;
using System.Runtime.ExceptionServices;
using System.Text;
using System.Threading.Tasks;

namespace MoleculeNamer
{
    public class AdjMatrix
    {
        int?[,] matrix; // The adjacency matrix is a 2D array mapping arcs between start and end nodes
        int count = 0; //  Number of nodes in the matrix

        // @TODO - These structures should be in a higher level function, rather than in the adjacency matrix itself
        // intermediate storage which helps understand the routes through the adjacency matrix
        List<List<int>> allRoutesFromRootNode = new List<List<int>>();
        List<List<int>> allRouteCombinations = new List<List<int>>(); // intermediate list space

        // @TODO - Move these chemical naming dictionaries elsewhere
        Dictionary<int, string> prefixDict =
            new Dictionary<int, string>(){{1, "meth"},{2, "eth"},{3, "prop"},
                {4,"but"},{5,"pent"},{6,"hex"},{7,"hept"},{8,"oct"},{9,"non"},
                {10,"dec"},{11,"undec"},{12,"dodec"},{13,"tridec"},{14,"tetradec"},
                {15,"pentadec"},{16,"hexadec"},{17,"heptadec"},{18,"octadec"},
                {19,"nonadec"},{20,"icos"}};
        Dictionary<int, string> multiplicityPrefixDict =
        new Dictionary<int, string>() { { 1, "" }, { 2, "di" }, { 3, "tri" }, { 4, "tetra" } };
        Dictionary<string, int> reversedprefixDict =
        new Dictionary<string, int>(){{"meth",1},{"eth",2},{"prop",3},{"but",4},
        {"pent",5},{"hex",6},{"hept",7},{"oct",8},{"non",9},{"dec",10},{"undec",11},
        {"dodec",12},{"tridec",13},{"tetradec",14},{"pentadec",15},{"hexadec",16},
        {"heptadec",17},{"octadec",18},{"nonadec",19},{"icos",20}};

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

            // Using Depth first traversal of the adjacency matrix
            List<int> RouteSoFar1 = new List<int>();
            RouteSoFar1.Add(0);
            FindPaths(RouteSoFar1);
            // longestcalcs
            dumpRoute(longestRoute);
            findAllRouteCombinations();

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
                allRoutesFromRootNode.Add(RouteSoFar);
                Console.WriteLine(allRoutesFromRootNode.Count);
            }
        }


        // Finds all possible routes through the adjacency matrix
        // Made up of
        //    - all the routes from root node
        //    - any routes that go via any node (either Root or interim)
        // Note - currently brute force 
        // - is not checking for repeats or reverse path
        private void findAllRouteCombinations()
        {
            foreach (List<int> primary in allRoutesFromRootNode)
            {
                allRouteCombinations.Add(primary);// copies allroutes previously found to thenew list

                foreach (List<int> secondary in allRoutesFromRootNode)
                {
                    int matchpos = 0;
                    int i = 0;

                    // makes sure it doesnt compare the identical lists
                    if (!Enumerable.SequenceEqual(secondary, primary))
                    {
                        // iterate through lists to find the latest common node
                        while (primary[i] == secondary[i])
                        {
                            matchpos = i;
                            i++;
                        }

                        // creates the merged route
                        List<int> mergedroute = [.. countBackward(matchpos, primary), .. countForward(matchpos, secondary)];
                        dumpRoute(mergedroute);
                        allRouteCombinations.Add(mergedroute);//adds the created route to a lisst of every route
                    }
                }
            }
        }

        // Helper function to extract the list range from matchpos to the end
        private List<int> countForward(int matchpos, List<int> route)
        {
            List<int> forwardsection = new List<int>();
            // TODO refactor to use a list segment
            for (int i = matchpos; i <= route.Count - 1; i++)
            {
                forwardsection.Add(route[i]);
            }
            return forwardsection;
        }

        // Helper function to extract the list range from matchpos+1 to the end and then reverse it.
        // This can then be concatenated onto the countForward list
        private List<int> countBackward(int matchpos, List<int> route)
        {
            List<int> backwardsection = new List<int>();
            // TODO refactor to use a list segment
            for (int i = matchpos + 1; i <= route.Count - 1; i++)
            {
                backwardsection.Add(route[i]);
            }
            backwardsection.Reverse();
            return backwardsection;
        }

        // Builds a list of the neighbours of a given node
        private List<int> buildNeighbourlist(int node)
        {
            List<int> neighbourList = new List<int>();
            for (int i = 0; i < matrix.GetLength(1); i++)
            {
                // checks if the current node is linked to another index
                if (isLinked(node, i))
                // if (matrix[node, i] == 1)
                {
                    neighbourList.Add(i);
                }
            }

            return neighbourList;
        }

        // Find Nodes adjacent to route
        private List<int> findNodesAdjacentToRoute(List<int> route, List<int> nodesToTest)
        {
            //find location of branch on main chain
            //find length of branch
            //use length and position to correctly name the molecule
            List<int> nodesAdjacentToRoute = new List<int>();  // list of nodes that spawn branches from the main Route
            // find 
            foreach (int excess in nodesToTest)
            {
                foreach (int nodeInRoute in route)
                {
                    if (isLinked(excess, nodeInRoute))
                    {  //checks if it has a direct conection to the main route
                        nodesAdjacentToRoute.Add(nodeInRoute);  //node that the branches are connected
                    }
                }
            }
            return nodesAdjacentToRoute;
        }


        private int countEntriesEqualToValue(List<int> list, int valueToFind)
        {
            int count = 0;
            foreach (int value in list)
            {
                if (value == valueToFind)
                {
                    count++;
                }
            }
            return count;
        }

        private string extendNameWithAlkylGroups(string suffix, List<int> mainRouteNodes, List<int> notInMainRoute)
        {
            // Find Nodes adjacent to route
            // find location of branch on main chain
            // find length of branch
            // use length and position to correctly name the molecule
            string name = "";

            List<int> nodesAdjacentToRoute = findNodesAdjacentToRoute(mainRouteNodes, notInMainRoute);
            List<int> Blengths = new List<int>(FindBranchLength(nodesAdjacentToRoute, mainRouteNodes));  // original list of branch lengths in the order of "branches"
            List<int> BLengthsOrdered = new List<int>(Blengths);  // copy of Blengths for the prefixes
            List<int> BlengthPositions = new List<int>(Blengths);
            List<string> BlengthsPrefix = new List<string>();
            List<int> positions = new List<int>();// positions on the chain
            foreach (int bran in Blengths) { BlengthsPrefix.Add(prefixDict[bran]); }  // same order ad "Blengths" but encoded by "Prefix"
            BlengthsPrefix.Sort();  // putting the alkyl groups in alphabetical order
            foreach (string thing in BlengthsPrefix) { BLengthsOrdered.Add(reversedprefixDict[thing]); }  // now have an ordered numbers alphabetically
            BLengthsOrdered.Reverse();  // reversed it as it will be adding fron right to left
            foreach (int branchLength in BLengthsOrdered)
            {
                // find the number of branches with the given length
                int noGiveBLength = countEntriesEqualToValue(BLengthsOrdered, branchLength); // number of similar branches
                name = "-" + multiplicityPrefixDict[noGiveBLength] + prefixDict[branchLength] + suffix;  // adds the miltiplicity and length of the chain
                // find all the positions of similar branches
                positions.Clear();

                for (int i = 0; i < noGiveBLength; i++)
                {
                    // adds to position the positions of the branches of based on the main chain
                    positions.Add(BlengthPositions.IndexOf(branchLength) - i + 1);//finds the first instance of branches
                    BlengthPositions.Remove(branchLength);
                }
                foreach (var item in positions)
                {
                    name = "," + item + name;
                }
                name.Replace(",", "-"); // remove(position,number of chracters)
            }
            return name;
        }

        public string nameMolecule(List<int> mainRouteNodes)
        {
            // seed name based on length of the main Route (e.g. octane)
            string name = prefixDict[mainRouteNodes.Count] + "ane";

            // populate notInMainRoute with all nodes not in the mainRoute
            List<int> notInMainRoute = new List<int>();
            foreach (int node in Enumerable.Range(0, getNumNodes() - 1))
            {
                if (!mainRouteNodes.Contains(node))
                {
                    notInMainRoute.Add(node);// checks for alkyl groups
                }
            }

            // Are there additional nodes to account for?
            if (notInMainRoute.Count > 0)
            {
                name = extendNameWithAlkylGroups(name, mainRouteNodes, notInMainRoute);
            }
            return name;
        }


        private List<int> FindBranchLength(List<int> mainBranchRoots, List<int> visitedNodes)
        {
            //create a new list that gives a list of branch lengths that directly corrolate to "branches"
            List<int> branchLengths = new List<int>();
            List<int> route = new List<int>();

            int currentNode;

            foreach (int branchRoot in mainBranchRoots)
            {
                // only works for simple alkyl groups at the moment
                bool moreToGo = true;
                currentNode = branchRoot;
                // Get the neighboures for the current Node
                List<int> neighbours = new List<int>(buildNeighbourlist(currentNode));
                while (moreToGo)
                {
                    //visitedNodes.Add(branchRoot);
                    // If there are two or more neighbours then we will have a branching situation,.
                    if (neighbours.Count >= 2)
                    {
                        // Scan the various neighbours
                        foreach (int neighbour in neighbours)
                        {
                            // if the neighbour has been visited
                            if (!visitedNodes.Contains(neighbour))
                            {
                                // Then this is the start of a spur in the molecule
                                route.Add(neighbour);
                                List<int> newVisitedNodes = new List<int>(visitedNodes);
                                newVisitedNodes.Add(neighbour);
                                currentNode = neighbour;
                                // Invoke recursion here
                            }
                        }
                    }
                    else { moreToGo = false; }
                }
                branchLengths.Add(route.Count);
            }
            return branchLengths;
        }

    }
}
