using Microsoft.VisualStudio.TestTools.UnitTesting;
//using MoleculeNamer;

namespace MoleculeNamer.UnitTests
{
    [TestClass]
    public class MoleculeNamer_MoleculeProcessor_ProcessMoleculeShould
    {
        private readonly MoleculeProcesssor _moleculeProcessor;

        public MoleculeNamer_MoleculeProcessor_ProcessMoleculeShould()
        {
            _moleculeProcessor = new MoleculeProcesssor();
        }

        [TestMethod]
        public void MoleculeProcesssor_testEmptyString()
        {
            Graph result = _moleculeProcessor.processMolecule("");
            //Assert.IsFalse(result_graph, "emptyString should fail test");
            Assert.IsTrue(result.getNumNodes() == 0, "\"\" will result in an empty list");
        }

        [TestMethod]
        public void MoleculeProcesssor_testOpenBracket()
        {
            Graph result = _moleculeProcessor.processMolecule("(");
            Assert.IsTrue(result.getNumNodes() == 0, "( is invalid so no graph will be returned");
        }

        [TestMethod]
        public void MoleculeProcesssor_testCloseBracket()
        {
            Graph result = _moleculeProcessor.processMolecule(")");
            Assert.IsTrue(result.getNumNodes() == 0, ") is invalid so no graph will be returned");
        }

        [TestMethod]
        public void MoleculeProcesssor_testBasicMolecule()
        {
            Graph result = _moleculeProcessor.processMolecule("C");
            Assert.IsTrue(result.AllNodes.Count == 1, "Need Proper test logic here");
        }

        [TestMethod]
        public void MoleculeProcesssor_testBasicMoleculeWithCloseBracket()
        {
            Graph result = _moleculeProcessor.processMolecule("C)");
            Assert.IsTrue(result.getNumNodes() == 0, "C) is invalid so no graph will be returned");
        }

        [TestMethod]
        public void MoleculeProcesssor_testBasicMoleculeWithOpenBracket()
        {
            Graph result = _moleculeProcessor.processMolecule("C(");
            Assert.IsTrue(result.getNumNodes() == 0, "C( is invalid so no graph will be returned");
        }

        [TestMethod]
        public void MoleculeProcesssor_testEmptyBrackets()
        {
            Graph result = _moleculeProcessor.processMolecule("()");
            Assert.IsTrue(result.getNumNodes() == 0, "() is invalid so no graph will be returned");
        }

        [TestMethod]
        public void MoleculeProcesssor_testBasicMoleculeWithEmptyBrackets()
        {
            Graph result = _moleculeProcessor.processMolecule("C()");
            Assert.IsTrue(result.getNumNodes() == 0, "C() is invalid so no graph will be returned");
        }

        [TestMethod]
        public void MoleculeProcesssor_testBasicMoleculesWithEmptyBrackets()
        {
            Graph result = _moleculeProcessor.processMolecule("C()C");
            Assert.IsTrue(result.getNumNodes() == 0, "C()C is invalid so no graph will be returned");
        }
        [TestMethod]
        public void MoleculeProcesssor_testBasicMoleculeBracketsWithNoFollowOnC()
        {
            Graph result = _moleculeProcessor.processMolecule("C(C)");
            Assert.IsTrue(result.getNumNodes() == 0, "C(C)C is invalid so no graph will be returned");
        }
        [TestMethod]
        public void MoleculeProcesssor_testBasicMoleculeBracketsWithFollowOnC()
        {
            Graph result = _moleculeProcessor.processMolecule("C(C)C");
            Assert.IsTrue(result.getNumNodes() == 3, "C(C)C is valid so a 3 node graph will be returned");
        }

        [TestMethod]
        public void MoleculeProcesssor_testEmptyBracketsWithFollowOnC()
        {
            Graph result = _moleculeProcessor.processMolecule("()C");
            Assert.IsTrue(result.getNumNodes() == 0, "()C is invalid so no graph will be returned");
        }

        [TestMethod]
        public void MoleculeProcesssor_testBasicMoleculeWithNestedBrackets()
        {
            Graph result = _moleculeProcessor.processMolecule("C((C))C");
            Assert.IsTrue(result.getNumNodes() == 0, "C((C))C is invalid so no graph will be returned");
        }

        [TestMethod]
        public void MoleculeProcesssor_testBasicMoleculeValidNestedBrackets()
        {
            Graph result = _moleculeProcessor.processMolecule("C(C(C))C");
            Assert.IsTrue(result.getNumNodes() == 4, "C(C(C))C is valid so a graph should be returned");
        }

        [TestMethod]
        // Cases where the number of opening and closing brackets do not match
        public void MoleculeProcesssor_testBasicMoleculeBracketMismatch()
        {
            Graph result = _moleculeProcessor.processMolecule("C(C(C)C");
            Assert.IsTrue(result.getNumNodes() == 0, "C(C(C)C is invalid so no graph will be returned");

            result = _moleculeProcessor.processMolecule("CC(C))C");
            Assert.IsTrue(result.getNumNodes() == 0, "CCC(C))C is invalid so no graph will be returned");
        }

        [TestMethod]
        public void MoleculeProcesssor_testNameMolecule_methane()
        {
            Graph graph = _moleculeProcessor.processMolecule("C");
            Assert.AreEqual("methane", graph.nameMolecule());
        }

        [TestMethod]
        public void MoleculeProcesssor_testNameMolecule_ethane()
        {
            Graph graph = _moleculeProcessor.processMolecule("CC");
            Assert.AreEqual("ethane", graph.nameMolecule());
        }

        [TestMethod]
        public void MoleculeProcesssor_testNameMolecule_propane()
        {
            Graph graph = _moleculeProcessor.processMolecule("CCC");
            Assert.AreEqual("propane", graph.nameMolecule());
        }

        [TestMethod]
        public void MoleculeProcesssor_testNameMolecule_butane()
        {
            Graph graph = _moleculeProcessor.processMolecule("CCCC");
            Assert.AreEqual("butane", graph.nameMolecule());
        }

        [TestMethod]
        public void MoleculeProcesssor_testNameMolecule_pentane()
        {
            Graph graph = _moleculeProcessor.processMolecule("CCCCC");
            Assert.AreEqual("pentane", graph.nameMolecule());
        }

        [TestMethod]
        public void MoleculeProcesssor_testNameMolecule_hexane()
        {
            Graph graph = _moleculeProcessor.processMolecule("CCCCCC");
            Assert.AreEqual("hexane", graph.nameMolecule());
        }

        [TestMethod]
        public void MoleculeProcesssor_testNameMolecule_heptane()
        {
            Graph graph = _moleculeProcessor.processMolecule("CCCCCCC");
            Assert.AreEqual("heptane", graph.nameMolecule());
        }

        [TestMethod]
        public void MoleculeProcesssor_testNameMolecule_octane()
        {
            Graph graph = _moleculeProcessor.processMolecule("CCCCCCCC");
            Assert.AreEqual("octane", graph.nameMolecule());
        }

        [TestMethod]
        public void MoleculeProcesssor_testNameMolecule_nonane()
        {
            Graph graph = _moleculeProcessor.processMolecule("CCCCCCCCC");
            Assert.AreEqual("nonane", graph.nameMolecule());
        }

        [TestMethod]
        public void MoleculeProcesssor_testNameMolecule_decane()
        {
            Graph graph = _moleculeProcessor.processMolecule("CCCCCCCCCC");
            Assert.AreEqual("decane", graph.nameMolecule());
        }

        [TestMethod]
        public void MoleculeProcesssor_testNameMolecule_undecane()
        {
            Graph graph = _moleculeProcessor.processMolecule("CCCCCCCCCCC");
            Assert.AreEqual("undecane", graph.nameMolecule());
        }

        [TestMethod]
        public void MoleculeProcesssor_testNameMolecule_duodecane()
        {
            Graph graph = _moleculeProcessor.processMolecule("CCCCCCCCCCCC");
            Assert.AreEqual("dodecane", graph.nameMolecule());
        }

        [TestMethod]
        public void MoleculeProcesssor_testNameMolecule_tridecane()
        {
            Graph graph = _moleculeProcessor.processMolecule("CCCCCCCCCCCCC");
            Assert.AreEqual("tridecane", graph.nameMolecule());
        }

        [TestMethod]
        public void MoleculeProcesssor_testNameMolecule_tetradecane()
        {
            Graph graph = _moleculeProcessor.processMolecule("CCCCCCCCCCCCCC");
            Assert.AreEqual("tetradecane", graph.nameMolecule());
        }

        [TestMethod]
        public void MoleculeProcesssor_testNameMolecule_pentadecane()
        {
            Graph graph = _moleculeProcessor.processMolecule("CCCCCCCCCCCCCCC");
            Assert.AreEqual("pentadecane", graph.nameMolecule());
        }

        [TestMethod]
        public void MoleculeProcesssor_testNameMolecule_hexadecane()
        {
            Graph graph = _moleculeProcessor.processMolecule("CCCCCCCCCCCCCCCC");
            Assert.AreEqual("hexadecane", graph.nameMolecule());
        }

        [TestMethod]
        public void MoleculeProcesssor_testNameMolecule_heptadecane()
        {
            Graph graph = _moleculeProcessor.processMolecule("CCCCCCCCCCCCCCCCC");
            Assert.AreEqual("heptadecane", graph.nameMolecule());
        }

        [TestMethod]
        public void MoleculeProcesssor_testNameMolecule_octaane()
        {
            Graph graph = _moleculeProcessor.processMolecule("CCCCCCCCCCCCCCCCCC");
            Assert.AreEqual("octadecane", graph.nameMolecule());
        }

        [TestMethod]
        public void MoleculeProcesssor_testNameMolecule_nonadecane()
        {
            Graph graph = _moleculeProcessor.processMolecule("CCCCCCCCCCCCCCCCCCC");
            Assert.AreEqual("nonadecane", graph.nameMolecule());
        }

        [TestMethod]
        public void MoleculeProcesssor_testNameMolecule_icosane()
        {
            Graph graph = _moleculeProcessor.processMolecule("CCCCCCCCCCCCCCCCCCCC");
            Assert.AreEqual("icosane", graph.nameMolecule());
        }

        [TestMethod]
        public void MoleculeProcesssor_testNameMolecule_butaneBrackets()
        {
            Graph graph = _moleculeProcessor.processMolecule("C(CC)C");
            Assert.AreEqual("butane", graph.nameMolecule());
        }
        [TestMethod]
        public void MoleculeProcesssor_testNameMolecule_3_methylpentane()
        {
            Graph graph = _moleculeProcessor.processMolecule("CC(CC)CC");
            Assert.AreEqual("3-methylpentane", graph.nameMolecule());
        }
        [TestMethod]
        public void MoleculeProcesssor_testNameMolecule_blah()
        {
            Graph graph = _moleculeProcessor.processMolecule("CC(CCC)CC");
            Assert.AreEqual("3-methylhexane", graph.nameMolecule());
        }

        [TestMethod]
        public void MoleculeProcesssor_testNameMolecule_blah2()
        {
            // @TODO /\/\
            //        /\_
            Graph graph = _moleculeProcessor.processMolecule("CC(CC)(CC)CC");
            AdjMatrix adjMatrix = new(graph);
            Assert.IsTrue(adjMatrix.getNumNodes() == 8, "three Node graph will have three nodes");
            Assert.IsTrue(adjMatrix.isLinked(0, 1), "Nodes 0 and 1 should be connected");
            Assert.IsTrue(adjMatrix.isLinked(1, 2), "Nodes 1 and 2 should be connected");
            Assert.IsTrue(adjMatrix.isLinked(1, 4), "Nodes 1 and 4 should be connected");
            Assert.IsTrue(adjMatrix.isLinked(1, 6), "Nodes 1 and 6 should be connected");
            Assert.IsTrue(adjMatrix.isLinked(2, 3), "Nodes 2 and 3 should be connected");
            Assert.IsTrue(adjMatrix.isLinked(4, 5), "Nodes 4 and 5 should be connected");
            Assert.IsTrue(adjMatrix.isLinked(6, 7), "Nodes 6 and 7 should be connected");
            List<int> Longest = new(adjMatrix.FindLongest());
            Assert.IsTrue(Longest.Count() == 5, "5 Connected nodes means that longest chain = 5");

            Assert.AreEqual("3-ethyl-3-methylpentane", graph.nameMolecule());
        }
    }
}