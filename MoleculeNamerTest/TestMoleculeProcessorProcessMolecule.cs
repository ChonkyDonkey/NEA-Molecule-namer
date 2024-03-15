using Microsoft.VisualStudio.TestTools.UnitTesting;
using MoleculeNamer;

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
        public void MoleculeProcesssor_testNameMolecule_2ane()
        {
            Graph graph = _moleculeProcessor.processMolecule("CC");
            Assert.AreEqual("2ane", graph.nameMolecule());
        }

        [TestMethod]
        public void MoleculeProcesssor_testNameMolecule_3ane()
        {
            Graph graph = _moleculeProcessor.processMolecule("CCC");
            Assert.AreEqual("3ane", graph.nameMolecule());
        }

        [TestMethod]
        public void MoleculeProcesssor_testNameMolecule_4ane()
        {
            Graph graph = _moleculeProcessor.processMolecule("CCCC");
            Assert.AreEqual("4ane", graph.nameMolecule());
        }

        [TestMethod]
        public void MoleculeProcesssor_testNameMolecule_5ane()
        {
            Graph graph = _moleculeProcessor.processMolecule("CCCCC");
            Assert.AreEqual("5ane", graph.nameMolecule());
        }

        [TestMethod]
        public void MoleculeProcesssor_testNameMolecule_6ane()
        {
            Graph graph = _moleculeProcessor.processMolecule("CCCCCC");
            Assert.AreEqual("6ane", graph.nameMolecule());
        }

        [TestMethod]
        public void MoleculeProcesssor_testNameMolecule_7ane()
        {
            Graph graph = _moleculeProcessor.processMolecule("CCCCCCC");
            Assert.AreEqual("7ane", graph.nameMolecule());
        }

        [TestMethod]
        public void MoleculeProcesssor_testNameMolecule_8ane()
        {
            Graph graph = _moleculeProcessor.processMolecule("CCCCCCCC");
            Assert.AreEqual("8ane", graph.nameMolecule());
        }

        [TestMethod]
        public void MoleculeProcesssor_testNameMolecule_9ane()
        {
            Graph graph = _moleculeProcessor.processMolecule("CCCCCCCCC");
            Assert.AreEqual("9ane", graph.nameMolecule());
        }

        [TestMethod]
        public void MoleculeProcesssor_testNameMolecule_10ane()
        {
            Graph graph = _moleculeProcessor.processMolecule("CCCCCCCCCC");
            Assert.AreEqual("10ane", graph.nameMolecule());
        }

        [TestMethod]
        public void MoleculeProcesssor_testNameMolecule_11ane()
        {
            Graph graph = _moleculeProcessor.processMolecule("CCCCCCCCCCC");
            Assert.AreEqual("11ane", graph.nameMolecule());
        }

        [TestMethod]
        public void MoleculeProcesssor_testNameMolecule_12ane()
        {
            Graph graph = _moleculeProcessor.processMolecule("CCCCCCCCCCCC");
            Assert.AreEqual("12ane", graph.nameMolecule());
        }

        [TestMethod]
        public void MoleculeProcesssor_testNameMolecule_13ane()
        {
            Graph graph = _moleculeProcessor.processMolecule("CCCCCCCCCCCCC");
            Assert.AreEqual("13ane", graph.nameMolecule());
        }

        [TestMethod]
        public void MoleculeProcesssor_testNameMolecule_14ane()
        {
            Graph graph = _moleculeProcessor.processMolecule("CCCCCCCCCCCCCC");
            Assert.AreEqual("14ane", graph.nameMolecule());
        }

        [TestMethod]
        public void MoleculeProcesssor_testNameMolecule_15ane()
        {
            Graph graph = _moleculeProcessor.processMolecule("CCCCCCCCCCCCCCC");
            Assert.AreEqual("15ane", graph.nameMolecule());
        }

        [TestMethod]
        public void MoleculeProcesssor_testNameMolecule_16ane()
        {
            Graph graph = _moleculeProcessor.processMolecule("CCCCCCCCCCCCCCCC");
            Assert.AreEqual("16ane", graph.nameMolecule());
        }

        [TestMethod]
        public void MoleculeProcesssor_testNameMolecule_17ane()
        {
            Graph graph = _moleculeProcessor.processMolecule("CCCCCCCCCCCCCCCCC");
            Assert.AreEqual("17ane", graph.nameMolecule());
        }

        [TestMethod]
        public void MoleculeProcesssor_testNameMolecule_18ane()
        {
            Graph graph = _moleculeProcessor.processMolecule("CCCCCCCCCCCCCCCCCC");
            Assert.AreEqual("18ane", graph.nameMolecule());
        }

        [TestMethod]
        public void MoleculeProcesssor_testNameMolecule_19ane()
        {
            Graph graph = _moleculeProcessor.processMolecule("CCCCCCCCCCCCCCCCCCC");
            Assert.AreEqual("19ane", graph.nameMolecule());
        }

        [TestMethod]
        public void MoleculeProcesssor_testNameMolecule_20ane()
        {
            Graph graph = _moleculeProcessor.processMolecule("CCCCCCCCCCCCCCCCCCCC");
            Assert.AreEqual("20ane", graph.nameMolecule());
        }

        [TestMethod]
        public void MoleculeProcesssor_testNameMolecule_blah()
        {
            Graph graph = _moleculeProcessor.processMolecule("C(CC)C");
            Assert.AreEqual("somemolecule name", graph.nameMolecule());
        }
        [TestMethod]
        public void MoleculeProcesssor_testNameMolecule_blah2()
        {
            Graph graph = _moleculeProcessor.processMolecule("CC(CC)CC");
            Assert.AreEqual("somemolecule name", graph.nameMolecule());
        }
    }
}