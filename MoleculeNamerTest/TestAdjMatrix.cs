//using System.ComponentModel.Design.Serialization;
using Microsoft.VisualStudio.TestTools.UnitTesting;
//using MoleculeNamer;

namespace MoleculeNamer.UnitTests
{
    [TestClass]
    public class MoleculeNamer_AdjMatrix_AdjMatrixShould
    {
        private readonly AdjMatrix _adjMatrix;
        public MoleculeNamer_AdjMatrix_AdjMatrixShould()
        {
            _adjMatrix = new AdjMatrix();
        }

        [TestMethod]
        public void AdjMatrix_emptyGraph()
        {
            Graph test_graph = new();
            _adjMatrix.addGraph(test_graph);

            Assert.IsTrue(_adjMatrix.getNumNodes() == 0, "empty graph will have no nodes");

        }

        [TestMethod]
        public void AdjMatrix_singleNodeGraph()
        {
            //  1
            Graph test_graph = new();
            _ = test_graph.CreateRoot("Node1");

            _adjMatrix.addGraph(test_graph);
            Assert.IsTrue(_adjMatrix.getNumNodes() == 1, "Single Node graph will have one node");
            Assert.IsTrue(_adjMatrix.FindLongest().Count() == 1, "Single nodes means that longest length = 1");
        }

        [TestMethod]
        public void AdjMatrix_twoNodeGraphUnconnected()
        {
            //  1    2
            Graph test_graph = new();
            _ = test_graph.CreateRoot("Node1");
            _ = test_graph.CreateNode("Node2");

            _adjMatrix.addGraph(test_graph);
            Assert.IsTrue(_adjMatrix.getNumNodes() == 2, "Two Node graph will have two nodes");
            Assert.IsFalse(_adjMatrix.isLinked(0, 1), "Nodes are unconnected");
            Assert.IsFalse(_adjMatrix.isLinked(1, 0), "Nodes are unconnected");
            Assert.IsTrue(_adjMatrix.FindLongest().Count() == 1, "Unconnected nodes means that longest length = 1");
        }

        [TestMethod]
        public void AdjMatrix_twoNodeGraphConnected()
        {
            // 1 <===> 2
            Graph test_graph = new();
            Node node1 = test_graph.CreateRoot("Node1");
            Node node2 = test_graph.CreateNode("Node2");
            _ = node1.AddArc(node2);
            _ = node2.AddArc(node1);

            _adjMatrix.addGraph(test_graph);
            Assert.IsTrue(_adjMatrix.getNumNodes() == 2, "Two Node graph will have two nodes");
            Assert.IsTrue(_adjMatrix.isLinked(0, 1), "Nodes 0 and 1 should be connected");
            Assert.IsTrue(_adjMatrix.isLinked(1, 0), "Nodes 1 and 0 should be connected");
            Assert.IsTrue(_adjMatrix.FindLongest().Count() == 2, "2 Connected nodes means that longest chain = 2");
        }
        [TestMethod]
        public void AdjMatrix_Propane()
        {
            // 1 <===> 2 <==>3
            Graph test_graph = new();
            Node node1 = test_graph.CreateRoot("Node1");
            Node node2 = test_graph.CreateNode("Node2");
            Node node3 = test_graph.CreateNode("Node3");
            _ = node1.AddArc(node2);
            _ = node2.AddArc(node1);
            _ = node2.AddArc(node3);
            _ = node3.AddArc(node2);
            _adjMatrix.addGraph(test_graph);
            Assert.IsTrue(_adjMatrix.getNumNodes() == 3, "three Node graph will have three nodes");
            Assert.IsTrue(_adjMatrix.isLinked(0, 1), "Nodes 0 and 1 should be connected");
            Assert.IsTrue(_adjMatrix.isLinked(1, 0), "Nodes 1 and 0 should be connected");
            Assert.IsTrue(_adjMatrix.isLinked(1, 2), "Nodes 1 and 2 should be connected");
            Assert.IsTrue(_adjMatrix.isLinked(2, 1), "Nodes 2 and 1 should be connected");
            List<int> Longest = new(_adjMatrix.FindLongest());
            Assert.IsTrue(Longest.Count() == 3, "3 Connected nodes means that longest chain = 3");
            Assert.IsTrue(_adjMatrix.nameMolecule(Longest) == "propane", "the name for this molecule is meant to be propane");

        }

        [TestMethod]
        public void AdjMatrix_PropaneRoot2()
        {
            // 2 <===> 1 <==>3
            Graph test_graph = new();
            Node node1 = test_graph.CreateRoot("Node1");
            Node node2 = test_graph.CreateNode("Node2");
            Node node3 = test_graph.CreateNode("Node3");
            _ = node1.AddArc(node2);
            _ = node2.AddArc(node1);
            _ = node1.AddArc(node3);
            _ = node3.AddArc(node1);
            _adjMatrix.addGraph(test_graph);
            Assert.IsTrue(_adjMatrix.getNumNodes() == 3, "three Node graph will have three nodes");
            Assert.IsTrue(_adjMatrix.isLinked(0, 1), "Nodes 0 and 1 should be connected");
            Assert.IsTrue(_adjMatrix.isLinked(1, 0), "Nodes 1 and 0 should be connected");
            Assert.IsTrue(_adjMatrix.isLinked(0, 2), "Nodes 0 and 2 should be connected");
            Assert.IsTrue(_adjMatrix.isLinked(2, 0), "Nodes 2 and 0 should be connected");

            List<int> Longest = new(_adjMatrix.FindLongest());
            Assert.IsTrue(Longest.Count() == 3, "3 Connected nodes means that longest chain = 3");
            Assert.IsTrue(_adjMatrix.nameMolecule(Longest) == "propane", "the name for this molecule is meant to be propane");
        }
        [TestMethod]
        public void AdjMatrix_2_methylPentane()
        {
            // 2 <===> 1 <==>3
            Graph test_graph = new();
            Node node1 = test_graph.CreateRoot("Node1");
            Node node2 = test_graph.CreateNode("Node2");
            Node node3 = test_graph.CreateNode("Node3");
            Node node4 = test_graph.CreateNode("Node4");
            Node node5 = test_graph.CreateNode("Node5");
            Node node6 = test_graph.CreateNode("Node6");
            _ = node1.AddArc(node2);
            _ = node2.AddArc(node1);
            _ = node2.AddArc(node3);
            _ = node2.AddArc(node5);
            _ = node3.AddArc(node2);
            _ = node3.AddArc(node4);
            _ = node5.AddArc(node2);
            _ = node5.AddArc(node6);
            _ = node6.AddArc(node5);
            _adjMatrix.addGraph(test_graph);
            Assert.IsTrue(_adjMatrix.getNumNodes() == 6, "6 Node graph will have six nodes");
            Assert.IsTrue(_adjMatrix.isLinked(0, 1), "Nodes 0 and 1 should be connected");
            Assert.IsTrue(_adjMatrix.isLinked(2, 1), "Nodes 2 and 1 should be connected");
            Assert.IsTrue(_adjMatrix.isLinked(4, 1), "Nodes 4 and 1 should be connected");
            Assert.IsTrue(_adjMatrix.isLinked(4, 5), "Nodes 4 and 5 should be connected");
            Assert.IsTrue(_adjMatrix.isLinked(2, 3), "Nodes 2 and 3 should be connected");
            Assert.IsTrue(_adjMatrix.FindLongest().Count() == 5, "5 Connected nodes means that longest chain = 5");

        }

    }
}