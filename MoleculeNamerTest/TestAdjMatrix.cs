using System.ComponentModel.Design.Serialization;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MoleculeNamer;

namespace MoleculeNamer.UnitTests
{
    [TestClass]
    public class MoleculeNamer_AdjMatrix_AdjMatrixShould
    {
        private MoleculeNamer.AdjMatrix _adjMatrix;
        public MoleculeNamer_AdjMatrix_AdjMatrixShould()
        {
            _adjMatrix = new MoleculeNamer.AdjMatrix();
        }

        [TestMethod]
        public void AdjMatrix_emptyGraph()
        {
            Graph test_graph = new Graph();
            _adjMatrix.addGraph(test_graph);

            Assert.IsTrue(_adjMatrix.getNumNodes() == 0, "empty graph will have no nodes");

        }

        [TestMethod]
        public void AdjMatrix_singleNodeGraph()
        {
            //  1
            Graph test_graph = new Graph();
            Node node1 = test_graph.CreateRoot("Node1");

            _adjMatrix.addGraph(test_graph);
            Assert.IsTrue(_adjMatrix.getNumNodes() == 1, "Single Node graph will have one node");
            Assert.IsTrue(_adjMatrix.FindLongest().Count() == 1, "Single nodes means that longest length = 1");
        }

        [TestMethod]
        public void AdjMatrix_twoNodeGraphUnconnected()
        {
            //  1    2
            Graph test_graph = new Graph();
            Node node1 = test_graph.CreateRoot("Node1");
            Node node2 = test_graph.CreateNode("Node2");

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
            Graph test_graph = new Graph();
            Node node1 = test_graph.CreateRoot("Node1");
            Node node2 = test_graph.CreateNode("Node2");
            node1.AddArc(node2);
            node2.AddArc(node1);

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
            Graph test_graph = new Graph();
            Node node1 = test_graph.CreateRoot("Node1");
            Node node2 = test_graph.CreateNode("Node2");
            Node node3 = test_graph.CreateNode("Node3");
            node1.AddArc(node2);
            node2.AddArc(node1);
            node2.AddArc(node3);
            node3.AddArc(node2);
            _adjMatrix.addGraph(test_graph);
            Assert.IsTrue(_adjMatrix.getNumNodes() == 3, "three Node graph will have three nodes");
            Assert.IsTrue(_adjMatrix.isLinked(0, 1), "Nodes 0 and 1 should be connected");
            Assert.IsTrue(_adjMatrix.isLinked(1, 0), "Nodes 1 and 0 should be connected");
            Assert.IsTrue(_adjMatrix.isLinked(1, 2), "Nodes 1 and 2 should be connected");
            Assert.IsTrue(_adjMatrix.isLinked(2, 1), "Nodes 2 and 1 should be connected");
            Assert.IsTrue(_adjMatrix.FindLongest().Count() == 3, "3 Connected nodes means that longest chain = 3");

        }

        [TestMethod]
        public void AdjMatrix_PropaneRoot2()
        {
            // 2 <===> 1 <==>3
            Graph test_graph = new Graph();
            Node node1 = test_graph.CreateRoot("Node1");
            Node node2 = test_graph.CreateNode("Node2");
            Node node3 = test_graph.CreateNode("Node3");
            node1.AddArc(node2);
            node2.AddArc(node1);
            node1.AddArc(node3);
            node3.AddArc(node1);
            _adjMatrix.addGraph(test_graph);
            Assert.IsTrue(_adjMatrix.getNumNodes() == 3, "three Node graph will have three nodes");
            Assert.IsTrue(_adjMatrix.isLinked(0, 1), "Nodes 0 and 1 should be connected");
            Assert.IsTrue(_adjMatrix.isLinked(1, 0), "Nodes 1 and 0 should be connected");
            Assert.IsTrue(_adjMatrix.isLinked(0, 2), "Nodes 0 and 2 should be connected");
            Assert.IsTrue(_adjMatrix.isLinked(2, 0), "Nodes 2 and 0 should be connected");
            //Assert.IsTrue(_adjMatrix.FindLongest().Count() == 3, "3 Connected nodes means that longest chain = 3");
        }

    }
}