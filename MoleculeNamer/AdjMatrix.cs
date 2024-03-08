using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection.Metadata;
using System.Runtime.CompilerServices;
using System.Runtime.ExceptionServices;
using System.Text;
using System.Threading.Tasks;

namespace MoleculeNamer
{
    public class AdjMatrix
    {
        Dictionary<int, string> prefix =  
        new Dictionary<int, string>(){{1, "meth"},{2, "eth"},{3, "prop"},
              {4,"but"},{5,"pent"},{6,"hex"},{7,"hept"},{8,"oct"},{9,"non"},
              {10,"dec"},{11,"undec"},{12,"dodec"},{13,"tridec"},{14,"tetradec"},
              {15,"pentadec"},{16,"hexadec"},{17,"heptadec"},{18,"octadec"},
              {19,"nonadec"},{20,"icos"}}; 
        Dictionary<string,int> reversedprefix = 
        new Dictionary<string, int>(){{"meth",1},{"eth",2},{"prop",3},{"but",4},
        {"pent",5},{"hex",6},{"hept",7},{"oct",8},{"non",9},{"dec",10},{"undec",11},
        {"dodec",12},{"tridec",13},{"tetradec",14},{"pentadec",15},{"hexadec",16},
        {"heptadec",17},{"octadec",18},{"nonadec",19},{"icos",20}};
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
                        allRouteCombinations.Add(mergedroute);//adds the created route to a lisst of every route
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
        public string nameMolecule(List<int> MainRoute){
            string name = "";
            int length = MainRoute.Count;
            
            List<int> nodes = new List<int>();
            List<int> notInList = new List<int>();
            for (int i = 0; i < matrix.GetLength(1); i++)
            {nodes.Add(i);}// makes a list of all nodes
            foreach(int node in nodes){

                if(!MainRoute.Contains(node)){
                    notInList.Add(node);// checks for alkyl groups
                }
            }
            name = prefix[length]+ "ane";

            if(notInList.Count==0){
                //adds the prefix to the name
                Console.WriteLine(name);
                return name;
            }
            else{
                //find how many branches
                //find location of branch on main chain
                //find length of branch
                //use length and position to correctly name the molecule
                int nobranch = 0;
                List<int> branches = new List<int>();
                foreach(int excess in notInList){
                    foreach(int listed in MainRoute){
                        if(isLinked(excess,listed)){//checks if it has a direct conection to the main chain
                            nobranch++;
                            branches.Add(listed); //node that the branches are connected
                        }
                    }
                }
                List<int> Blengths = new List<int>(FindBranchLength(branches,MainRoute));
                List<int> Blengthsordered = new List<int>(Blengths);
                List<string> Blengthsprefix = new List<string>();  
                foreach (int bran in Blengths){Blengthsprefix.Add(prefix[bran]);}
                Blengthsprefix.Sort();//putting the alkyl groups in alphabetical order
                foreach(string thing in Blengthsprefix){Blengthsordered.Add(reversedprefix[thing]);}//now have an ordered numbers alphabetically
                
                return name;
            }
        }
        private List<int> FindBranchLength(List<int> branches,List<int> main){
            //create a new list that gives a list of branch lengths that directly corrolate to "branches"
            List<int> branchlengths = new List<int>();
            List<int> route = new List<int>();
            List<int> visited = new List<int>();
            List<int> neighbours = new List<int>();
            
            int currentNode;
            foreach(int branch in branches){// only works for simple alkyl groups at the moment
                bool moreToGo = true;
                neighbours.Clear();
                currentNode = branch;
                neighbours.AddRange(buildNeighbourlist(currentNode));
                while(moreToGo){
                    
                    visited.Add(branch);
                    if (neighbours.Count>=2){
                        foreach(int neighbour in neighbours){
                            if (!visited.Contains(neighbour)&& !main.Contains(neighbour)){
                                route.Add(neighbour);
                                visited.Add(neighbour);
                                currentNode = neighbour;
                            }
                        }
                    }
                    else{moreToGo=false;}
                }
                branchlengths.Add(route.Count);
            }
            return branchlengths;
        }
        
    }
}
