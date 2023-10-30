using Microsoft.VisualStudio.TestTools.UnitTesting;
using MoleculeNamer;

namespace MoleculeNamer.UnitTests
{
    [TestClass]
    public class MoleculeNamer_MoleculeProcessorShould
    {
        private readonly MoleculeProcesssor _moleculeProcessor;

        public MoleculeNamer_MoleculeProcessorShould()
        {
            _moleculeProcessor = new MoleculeProcesssor();
        }

        [TestMethod]
        public void MoleculeProcesssor_testEmptyString()
        {
            bool result = _moleculeProcessor.validateString("");
            Assert.IsFalse(result, "emptyString should fail test");
        }

        [TestMethod]
        public void MoleculeProcesssor_testOpenBracket()
        {
            bool result = _moleculeProcessor.validateString("(");
            Assert.IsFalse(result, "Open bracket should fail test");
        }

        [TestMethod]
        public void MoleculeProcesssor_testCloseBracket()
        {
            bool result = _moleculeProcessor.validateString(")");
            Assert.IsFalse(result, "\')\' bracket should fail test");
        }

        [TestMethod]
        public void MoleculeProcesssor_testBasicMolecule()
        {
            bool result = _moleculeProcessor.validateString("C");
            Assert.IsTrue(result, "\'C\' should pass test");
        }

        [TestMethod]
        public void MoleculeProcesssor_testBasicMoleculeWithCloseBracket()
        {
            bool result = _moleculeProcessor.validateString("C)");
            Assert.IsFalse(result, "\'C)\' should fail test");
        }

        [TestMethod]
        public void MoleculeProcesssor_testBasicMoleculeWithOpenBracket()
        {
            bool result = _moleculeProcessor.validateString("C(");
            Assert.IsFalse(result, "\'C(\' should fail test");
        }

        [TestMethod]
        public void MoleculeProcesssor_testEmptyBrackets()
        {
            bool result = _moleculeProcessor.validateString("()");
            Assert.IsFalse(result, "\'()\' should Fail test");
        }

        [TestMethod]
        public void MoleculeProcesssor_testBasicMoleculeWithEmptyBrackets()
        {
            bool result = _moleculeProcessor.validateString("C()");
            Assert.IsFalse(result, "\'C()\' should Fail test");
        }

        [TestMethod]
        public void MoleculeProcesssor_testBasicMoleculesWithEmptyBrackets()
        {
            bool result = _moleculeProcessor.validateString("C()C");
            Assert.IsFalse(result, "\'C()C\' should fail test");
        }
        [TestMethod]
        public void MoleculeProcesssor_testBasicMoleculeBracketsWithNoFollowOnC()
        {
            bool result = _moleculeProcessor.validateString("C(C)");
            Assert.IsFalse(result, "\'C(C)\' should fail test");
        }
        [TestMethod]
        public void MoleculeProcesssor_testBasicMoleculeBracketsWithFollowOnC()
        {
            bool result = _moleculeProcessor.validateString("C(C)C");
            Assert.IsTrue(result, "\'C(C)C\' should pass test");
        }

        [TestMethod]
        public void MoleculeProcesssor_testEmptyBracketsWithFollowOnC()
        {
            bool result = _moleculeProcessor.validateString("()C");
            Assert.IsFalse(result, "\'()C\' should Fail test");
        }

        [TestMethod]
        public void MoleculeProcesssor_testBasicMoleculeWithNestedBrackets()
        {
            bool result = _moleculeProcessor.validateString("C((C))C");
            Assert.IsFalse(result, "\'C\' should Fail test");
        }

        [TestMethod]
        public void MoleculeProcesssor_testBasicMoleculeValidNestedBrackets()
        {
            bool result = _moleculeProcessor.validateString("C(C(C))C");
            Assert.IsTrue(result, "\'C(C(C))C\' should pass test");
        }

        [TestMethod]
        public void MoleculeProcesssor_testBasicMoleculeBracketMismatch()
        {
            bool result = _moleculeProcessor.validateString("C(C(C)C");
            Assert.IsFalse(result, "\'C(C(C)C\' should Fail test");
            result = _moleculeProcessor.validateString("CC(C))C");
            Assert.IsFalse(result, "\'CC(C))C\' should Fail test");
        }
    }
}