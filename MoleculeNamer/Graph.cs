﻿using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoleculeNamer
{
    public class Graph
    {
        public Node? Root;
        public List<Node> AllNodes = new();

        public Node CreateRoot(string name)
        {
            Root = CreateNode(name);
            return Root;
        }

        public Node CreateNode(string name)
        {
            var n = new Node(name);
            AllNodes.Add(n);
            return n;
        }

        public int GetNumNodes()
        {
            return AllNodes.Count;
        }
        public int?[,] CreateAdjMatrix()
        {
            // Matrix will be created here...
            int?[,] adj = new int?[AllNodes.Count, AllNodes.Count];

            // Iterate over all nodes
            for (int i = 0; i < AllNodes.Count; i++)
            {
                // iterate over all peers
                Node n1 = AllNodes[i];
                for (int j = 0; j < AllNodes.Count; j++)
                {
                    Node n2 = AllNodes[j];

                    var arc = n1.Arcs.FirstOrDefault(a => a.Child == n2);
                    if (arc != null)
                    {
                        adj[i, j] = arc.Weight;
                    }
                }
            }
            return adj;
        }

        public void PrintMatrix()
        {
            AdjMatrix adj = new(this);
            adj.PrintMatrix();
        }

        public string NameMolecule()
        {
            AdjMatrix adj = new(this);
            List<int> longest = new(adj.FindLongest());
            return adj.NameMolecule(longest);
        }
    }
}


