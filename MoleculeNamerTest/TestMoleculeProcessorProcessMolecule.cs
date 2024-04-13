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
            Graph result = _moleculeProcessor.ProcessMolecule("");
            //Assert.IsFalse(result_graph, "emptyString should fail test");
            Assert.IsTrue(result.GetNumNodes() == 0, "\"\" will result in an empty list");
        }

        [TestMethod]
        public void MoleculeProcesssor_testOpenBracket()
        {
            Graph result = _moleculeProcessor.ProcessMolecule("(");
            Assert.IsTrue(result.GetNumNodes() == 0, "( is invalid so no graph will be returned");
        }

        [TestMethod]
        public void MoleculeProcesssor_testCloseBracket()
        {
            Graph result = _moleculeProcessor.ProcessMolecule(")");
            Assert.IsTrue(result.GetNumNodes() == 0, ") is invalid so no graph will be returned");
        }

        [TestMethod]
        public void MoleculeProcesssor_testBasicMolecule()
        {
            Graph result = _moleculeProcessor.ProcessMolecule("C");
            Assert.IsTrue(result.AllNodes.Count == 1, "Need Proper test logic here");
        }

        [TestMethod]
        public void MoleculeProcesssor_testBasicMoleculeWithCloseBracket()
        {
            Graph result = _moleculeProcessor.ProcessMolecule("C)");
            Assert.IsTrue(result.GetNumNodes() == 0, "C) is invalid so no graph will be returned");
        }

        [TestMethod]
        public void MoleculeProcesssor_testBasicMoleculeWithOpenBracket()
        {
            Graph result = _moleculeProcessor.ProcessMolecule("C(");
            Assert.IsTrue(result.GetNumNodes() == 0, "C( is invalid so no graph will be returned");
        }

        [TestMethod]
        public void MoleculeProcesssor_testEmptyBrackets()
        {
            Graph result = _moleculeProcessor.ProcessMolecule("()");
            Assert.IsTrue(result.GetNumNodes() == 0, "() is invalid so no graph will be returned");
        }

        [TestMethod]
        public void MoleculeProcesssor_testBasicMoleculeWithEmptyBrackets()
        {
            Graph result = _moleculeProcessor.ProcessMolecule("C()");
            Assert.IsTrue(result.GetNumNodes() == 0, "C() is invalid so no graph will be returned");
        }

        [TestMethod]
        public void MoleculeProcesssor_testBasicMoleculesWithEmptyBrackets()
        {
            Graph result = _moleculeProcessor.ProcessMolecule("C()C");
            Assert.IsTrue(result.GetNumNodes() == 0, "C()C is invalid so no graph will be returned");
        }
        [TestMethod]
        public void MoleculeProcesssor_testBasicMoleculeBracketsWithNoFollowOnC()
        {
            Graph result = _moleculeProcessor.ProcessMolecule("C(C)");
            Assert.IsTrue(result.GetNumNodes() == 0, "C(C)C is invalid so no graph will be returned");
        }
        [TestMethod]
        public void MoleculeProcesssor_testBasicMoleculeBracketsWithFollowOnC()
        {
            Graph result = _moleculeProcessor.ProcessMolecule("C(C)C");
            Assert.IsTrue(result.GetNumNodes() == 3, "C(C)C is valid so a 3 node graph will be returned");
        }

        [TestMethod]
        public void MoleculeProcesssor_testEmptyBracketsWithFollowOnC()
        {
            Graph result = _moleculeProcessor.ProcessMolecule("()C");
            Assert.IsTrue(result.GetNumNodes() == 0, "()C is invalid so no graph will be returned");
        }

        [TestMethod]
        public void MoleculeProcesssor_testBasicMoleculeWithNestedBrackets()
        {
            Graph result = _moleculeProcessor.ProcessMolecule("C((C))C");
            Assert.IsTrue(result.GetNumNodes() == 0, "C((C))C is invalid so no graph will be returned");
        }

        [TestMethod]
        public void MoleculeProcesssor_testBasicMoleculeValidNestedBrackets()
        {
            Graph result = _moleculeProcessor.ProcessMolecule("C(C(C))C");
            Assert.IsTrue(result.GetNumNodes() == 4, "C(C(C))C is valid so a graph should be returned");
        }
        [TestMethod]
        public void MoleculeProcesssor_testBasicMoleculeValidDoubleBrackets()
        {
            Graph result = _moleculeProcessor.ProcessMolecule("C(C)(C)C");
            AdjMatrix adjMatrix = new(result);
            adjMatrix.PrintMatrix();
            Assert.IsTrue(result.GetNumNodes() == 4, "C(C)(C)C is valid so a graph should be returned");
            Assert.IsTrue(adjMatrix.IsLinked(0, 1), "Nodes 0 and 1 should be connected");
            Assert.IsTrue(adjMatrix.IsLinked(0, 2), "Nodes 0 and 1 should be connected");
            Assert.IsTrue(adjMatrix.IsLinked(0, 3), "Nodes 0 and 1 should be connected");
        }

        [TestMethod]
        // Cases where the number of opening and closing brackets do not match
        public void MoleculeProcesssor_testBasicMoleculeBracketMismatch()
        {
            Graph result = _moleculeProcessor.ProcessMolecule("C(C(C)C");
            Assert.IsTrue(result.GetNumNodes() == 0, "C(C(C)C is invalid so no graph will be returned");

            result = _moleculeProcessor.ProcessMolecule("CC(C))C");
            Assert.IsTrue(result.GetNumNodes() == 0, "CCC(C))C is invalid so no graph will be returned");
        }

        [TestMethod]
        public void MoleculeProcesssor_testNameMolecule_methane()
        {
            Graph graph = _moleculeProcessor.ProcessMolecule("C");
            Assert.AreEqual("methane", graph.NameMolecule());
        }

        [TestMethod]
        public void MoleculeProcesssor_testNameMolecule_ethane()
        {
            Graph graph = _moleculeProcessor.ProcessMolecule("CC");
            Assert.AreEqual("ethane", graph.NameMolecule());
        }

        [TestMethod]
        public void MoleculeProcesssor_testNameMolecule_propane()
        {
            Graph graph = _moleculeProcessor.ProcessMolecule("CCC");
            Assert.AreEqual("propane", graph.NameMolecule());
        }

        [TestMethod]
        public void MoleculeProcesssor_testNameMolecule_butane()
        {
            Graph graph = _moleculeProcessor.ProcessMolecule("CCCC");
            Assert.AreEqual("butane", graph.NameMolecule());
        }

        [TestMethod]
        public void MoleculeProcesssor_testNameMolecule_pentane()
        {
            Graph graph = _moleculeProcessor.ProcessMolecule("CCCCC");
            Assert.AreEqual("pentane", graph.NameMolecule());
        }

        [TestMethod]
        public void MoleculeProcesssor_testNameMolecule_hexane()
        {
            Graph graph = _moleculeProcessor.ProcessMolecule("CCCCCC");
            Assert.AreEqual("hexane", graph.NameMolecule());
        }

        [TestMethod]
        public void MoleculeProcesssor_testNameMolecule_heptane()
        {
            Graph graph = _moleculeProcessor.ProcessMolecule("CCCCCCC");
            Assert.AreEqual("heptane", graph.NameMolecule());
        }

        [TestMethod]
        public void MoleculeProcesssor_testNameMolecule_octane()
        {
            Graph graph = _moleculeProcessor.ProcessMolecule("CCCCCCCC");
            Assert.AreEqual("octane", graph.NameMolecule());
        }

        [TestMethod]
        public void MoleculeProcesssor_testNameMolecule_nonane()
        {
            Graph graph = _moleculeProcessor.ProcessMolecule("CCCCCCCCC");
            Assert.AreEqual("nonane", graph.NameMolecule());
        }

        [TestMethod]
        public void MoleculeProcesssor_testNameMolecule_decane()
        {
            Graph graph = _moleculeProcessor.ProcessMolecule("CCCCCCCCCC");
            Assert.AreEqual("decane", graph.NameMolecule());
        }

        [TestMethod]
        public void MoleculeProcesssor_testNameMolecule_undecane()
        {
            Graph graph = _moleculeProcessor.ProcessMolecule("CCCCCCCCCCC");
            Assert.AreEqual("undecane", graph.NameMolecule());
        }

        [TestMethod]
        public void MoleculeProcesssor_testNameMolecule_duodecane()
        {
            Graph graph = _moleculeProcessor.ProcessMolecule("CCCCCCCCCCCC");
            Assert.AreEqual("dodecane", graph.NameMolecule());
        }

        [TestMethod]
        public void MoleculeProcesssor_testNameMolecule_tridecane()
        {
            Graph graph = _moleculeProcessor.ProcessMolecule("CCCCCCCCCCCCC");
            Assert.AreEqual("tridecane", graph.NameMolecule());
        }

        [TestMethod]
        public void MoleculeProcesssor_testNameMolecule_tetradecane()
        {
            Graph graph = _moleculeProcessor.ProcessMolecule("CCCCCCCCCCCCCC");
            Assert.AreEqual("tetradecane", graph.NameMolecule());
        }

        [TestMethod]
        public void MoleculeProcesssor_testNameMolecule_pentadecane()
        {
            Graph graph = _moleculeProcessor.ProcessMolecule("CCCCCCCCCCCCCCC");
            Assert.AreEqual("pentadecane", graph.NameMolecule());
        }

        [TestMethod]
        public void MoleculeProcesssor_testNameMolecule_hexadecane()
        {
            Graph graph = _moleculeProcessor.ProcessMolecule("CCCCCCCCCCCCCCCC");
            Assert.AreEqual("hexadecane", graph.NameMolecule());
        }

        [TestMethod]
        public void MoleculeProcesssor_testNameMolecule_heptadecane()
        {
            Graph graph = _moleculeProcessor.ProcessMolecule("CCCCCCCCCCCCCCCCC");
            Assert.AreEqual("heptadecane", graph.NameMolecule());
        }

        [TestMethod]
        public void MoleculeProcesssor_testNameMolecule_octaane()
        {
            Graph graph = _moleculeProcessor.ProcessMolecule("CCCCCCCCCCCCCCCCCC");
            Assert.AreEqual("octadecane", graph.NameMolecule());
        }

        [TestMethod]
        public void MoleculeProcesssor_testNameMolecule_nonadecane()
        {
            Graph graph = _moleculeProcessor.ProcessMolecule("CCCCCCCCCCCCCCCCCCC");
            Assert.AreEqual("nonadecane", graph.NameMolecule());
        }

        [TestMethod]
        public void MoleculeProcesssor_testNameMolecule_icosane()
        {
            Graph graph = _moleculeProcessor.ProcessMolecule("CCCCCCCCCCCCCCCCCCCC");
            Assert.AreEqual("icosane", graph.NameMolecule());
        }

        [TestMethod]
        public void MoleculeProcesssor_testNameMolecule_butaneBrackets()
        {
            Graph graph = _moleculeProcessor.ProcessMolecule("C(CC)C");
            Assert.AreEqual("butane", graph.NameMolecule());
        }
        [TestMethod]
        public void MoleculeProcesssor_testNameMolecule_3_methylpentane()
        {
            Graph graph = _moleculeProcessor.ProcessMolecule("CC(CC)CC");
            Assert.AreEqual("3-methylpentane", graph.NameMolecule());
        }
        [TestMethod]
        public void MoleculeProcesssor_testNameMolecule_blah()
        {
            Graph graph = _moleculeProcessor.ProcessMolecule("CC(CCC)CC");
            Assert.AreEqual("4-methylhexane", graph.NameMolecule());
        }

        [TestMethod]
        public void MoleculeProcesssor_testNameMolecule_blah2()
        {
            // @TODO /\/\
            //        /\_
            Graph graph = _moleculeProcessor.ProcessMolecule("CC(CC)(CC)CC");
            AdjMatrix adjMatrix = new(graph);
            Assert.IsTrue(adjMatrix.GetNumNodes() == 8, "three Node graph will have three nodes");
            Assert.IsTrue(adjMatrix.IsLinked(0, 1), "Nodes 0 and 1 should be connected");
            Assert.IsTrue(adjMatrix.IsLinked(1, 2), "Nodes 1 and 2 should be connected");
            Assert.IsTrue(adjMatrix.IsLinked(1, 4), "Nodes 1 and 4 should be connected");
            Assert.IsTrue(adjMatrix.IsLinked(1, 6), "Nodes 1 and 6 should be connected");
            Assert.IsTrue(adjMatrix.IsLinked(2, 3), "Nodes 2 and 3 should be connected");
            Assert.IsTrue(adjMatrix.IsLinked(4, 5), "Nodes 4 and 5 should be connected");
            Assert.IsTrue(adjMatrix.IsLinked(6, 7), "Nodes 6 and 7 should be connected");
            List<int> Longest = new(adjMatrix.FindLongest());
            Assert.IsTrue(Longest.Count() == 5, "5 Connected nodes means that longest chain = 5");

            Assert.AreEqual("3-ethyl-3-methylpentane", graph.NameMolecule());
        }
        [TestMethod]
        public void MoleculeProcesssor_testNameMolecule_blah3()
        {
            //          | 
            // @TODO /\/\/
            //         \_
            Graph graph = _moleculeProcessor.ProcessMolecule("CC(CC)C(CC)CC");
            AdjMatrix adjMatrix = new(graph);
            Assert.IsTrue(adjMatrix.GetNumNodes() == 9, "three Node graph will have three nodes");
            Assert.IsTrue(adjMatrix.IsLinked(0, 1), "Nodes 0 and 1 should be connected");
            Assert.IsTrue(adjMatrix.IsLinked(1, 2), "Nodes 1 and 2 should be connected");
            Assert.IsTrue(adjMatrix.IsLinked(1, 4), "Nodes 1 and 4 should be connected");
            Assert.IsTrue(adjMatrix.IsLinked(2, 3), "Nodes 2 and 3 should be connected");
            Assert.IsTrue(adjMatrix.IsLinked(4, 5), "Nodes 4 and 5 should be connected");
            Assert.IsTrue(adjMatrix.IsLinked(5, 6), "Nodes 1 and 6 should be connected");
            Assert.IsTrue(adjMatrix.IsLinked(4, 7), "Nodes 4 and 7 should be connected");
            Assert.IsTrue(adjMatrix.IsLinked(8, 7), "Nodes 8 and 7 should be connected");
            List<int> Longest = new(adjMatrix.FindLongest());
            Assert.IsTrue(Longest.Count() == 6, "6 Connected nodes means that longest chain = 6");

            Assert.AreEqual("4-ethyl-3-methylhexane", graph.NameMolecule());
        }
                [TestMethod]
        public void MoleculeProcesssor_alkyl_branches_from_OG_carbon()
        {
            Graph result = _moleculeProcessor.processMolecule("C(C)(C)(C)C");
            Assert.IsTrue(result.getNumNodes() == 5, "C(C)(C)(C)C is valid so a graph should be returned");
            result = _moleculeProcessor.processMolecule("CC(C)(C)(C)C");
            Assert.IsFalse(result.getNumNodes() == 6, "CC(C)(C)(C)C is valid so a graph should not be returned");
            result = _moleculeProcessor.processMolecule("C(C)(C)(C)(C)C");
            Assert.IsFalse(result.getNumNodes() == 6, "C(C)(C)(C)(C)C is valid so a graph should not be returned");
        }
    }
}