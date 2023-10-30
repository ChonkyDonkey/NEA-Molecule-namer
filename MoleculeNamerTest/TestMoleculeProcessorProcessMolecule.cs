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
            Assert.IsFalse(true, "Need Proper test logic here");
        }

        [TestMethod]
        public void MoleculeProcesssor_testOpenBracket()
        {
            Graph result = _moleculeProcessor.processMolecule("(");
            Assert.IsTrue(result.AllNodes.Count == 0, "( is invalid so no graph will be returned");
        }

        [TestMethod]
        public void MoleculeProcesssor_testCloseBracket()
        {
            Graph result = _moleculeProcessor.processMolecule(")");
            //Assert.IsFalse(result, "\')\' bracket should fail test");
            Assert.IsFalse(true, "Need Proper test logic here");
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
            //Assert.IsFalse(result, "\'C)\' should fail test");
            Assert.IsFalse(true, "Need Proper test logic here");
        }

        [TestMethod]
        public void MoleculeProcesssor_testBasicMoleculeWithOpenBracket()
        {
            Graph result = _moleculeProcessor.processMolecule("C(");
            //Assert.IsFalse(result, "\'C(\' should fail test");
            Assert.IsFalse(true, "Need Proper test logic here");
        }

        [TestMethod]
        public void MoleculeProcesssor_testEmptyBrackets()
        {
            Graph result = _moleculeProcessor.processMolecule("()");
            //Assert.IsFalse(result, "\'()\' should Fail test");
            Assert.IsFalse(true, "Need Proper test logic here");
        }

        [TestMethod]
        public void MoleculeProcesssor_testBasicMoleculeWithEmptyBrackets()
        {
            Graph result = _moleculeProcessor.processMolecule("C()");
            //Assert.IsFalse(result, "\'C()\' should Fail test");
            Assert.IsFalse(true, "Need Proper test logic here");
        }

        [TestMethod]
        public void MoleculeProcesssor_testBasicMoleculesWithEmptyBrackets()
        {
            Graph result = _moleculeProcessor.processMolecule("C()C");
            //Assert.IsFalse(result, "\'C()C\' should fail test");
            Assert.IsFalse(true, "Need Proper test logic here");
        }
        [TestMethod]
        public void MoleculeProcesssor_testBasicMoleculeBracketsWithNoFollowOnC()
        {
            Graph result = _moleculeProcessor.processMolecule("C(C)");
            //Assert.IsFalse(result, "\'C(C)\' should fail test");
            Assert.IsFalse(true, "Need Proper test logic here");
        }
        [TestMethod]
        public void MoleculeProcesssor_testBasicMoleculeBracketsWithFollowOnC()
        {
            Graph result = _moleculeProcessor.processMolecule("C(C)C");
            //Assert.IsTrue(result, "\'C(C)C\' should pass test");
            Assert.IsFalse(true, "Need Proper test logic here");
        }

        [TestMethod]
        public void MoleculeProcesssor_testEmptyBracketsWithFollowOnC()
        {
            Graph result = _moleculeProcessor.processMolecule("()C");
            //Assert.IsFalse(result, "\'()C\' should Fail test");
            Assert.IsFalse(true, "Need Proper test logic here");
        }

        [TestMethod]
        public void MoleculeProcesssor_testBasicMoleculeWithNestedBrackets()
        {
            Graph result = _moleculeProcessor.processMolecule("C((C))C");
            //Assert.IsFalse(result, "\'C\' should Fail test");
            Assert.IsFalse(true, "Need Proper test logic here");
        }

        [TestMethod]
        public void MoleculeProcesssor_testBasicMoleculeValidNestedBrackets()
        {
            Graph result = _moleculeProcessor.processMolecule("C(C(C))C");
            //Assert.IsTrue(result, "\'C(C(C))C\' should pass test");
            Assert.IsFalse(true, "Need Proper test logic here");
        }

        [TestMethod]
        public void MoleculeProcesssor_testBasicMoleculeBracketMismatch()
        {
            Graph result = _moleculeProcessor.processMolecule("C(C(C)C");
            Assert.IsFalse(true, "Need Proper test logic here");
            //Assert.IsFalse(result, "\'C(C(C)C\' should Fail test");
            result = _moleculeProcessor.processMolecule("CC(C))C");
            //Assert.IsFalse(result, "\'CC(C))C\' should Fail test");
            Assert.IsFalse(true, "Need Proper test logic here");
        }
    }
}