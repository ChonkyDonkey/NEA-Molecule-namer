using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MoleculeNamer.UnitTests
{
    [TestClass]
    public class MoleculeNamer_MoleculeProcessor_ValidateStringShould
    {
        private readonly MoleculeProcesssor _moleculeProcessor;

        public MoleculeNamer_MoleculeProcessor_ValidateStringShould()
        {
            _moleculeProcessor = new MoleculeProcesssor();
        }

        [TestMethod]
        public void MoleculeProcesssor_testEmptyString()//Erroneous
        {
            bool result = _moleculeProcessor.ValidateString("");
            Assert.IsFalse(result, "emptyString should fail test");
        }

        [TestMethod]
        public void MoleculeProcesssor_testOpenBracket()//Erroneous
        {
            bool result = _moleculeProcessor.ValidateString("(");
            Assert.IsFalse(result, "Open bracket should fail test");
        }

        [TestMethod]
        public void MoleculeProcesssor_testCloseBracket()//Erroneous
        {
            bool result = _moleculeProcessor.ValidateString(")");
            Assert.IsFalse(result, "\')\' bracket should fail test");
        }

        [TestMethod]
        public void MoleculeProcesssor_testBasicMolecule()//typical
        {
            bool result = _moleculeProcessor.ValidateString("C");
            Assert.IsTrue(result, "\'C\' should pass test");
        }

        [TestMethod]
        public void MoleculeProcesssor_testBasicMoleculeWithCloseBracket()//Erroneous
        {
            bool result = _moleculeProcessor.ValidateString("C)");
            Assert.IsFalse(result, "\'C)\' should fail test");
        }

        [TestMethod]
        public void MoleculeProcesssor_testBasicMoleculeWithOpenBracket()//Erroneous
        {
            bool result = _moleculeProcessor.ValidateString("C(");
            Assert.IsFalse(result, "\'C(\' should fail test");
        }

        [TestMethod]
        public void MoleculeProcesssor_testEmptyBrackets()//Erroneous
        {
            bool result = _moleculeProcessor.ValidateString("()");
            Assert.IsFalse(result, "\'()\' should Fail test");
        }

        [TestMethod]
        public void MoleculeProcesssor_testBasicMoleculeWithEmptyBrackets()//Erroneous
        {
            bool result = _moleculeProcessor.ValidateString("C()");
            Assert.IsFalse(result, "\'C()\' should Fail test");
        }

        [TestMethod]
        public void MoleculeProcesssor_testBasicMoleculesWithEmptyBrackets()//Erroneous
        {
            bool result = _moleculeProcessor.ValidateString("C()C");
            Assert.IsFalse(result, "\'C()C\' should fail test");
        }
        [TestMethod]
        public void MoleculeProcesssor_testBasicMoleculeBracketsWithNoFollowOnC()//Erroneous
        {
            bool result = _moleculeProcessor.ValidateString("C(C)");
            Assert.IsFalse(result, "\'C(C)\' should fail test");
        }
        [TestMethod]
        public void MoleculeProcesssor_testBasicMoleculeBracketsWithFollowOnC()//Typical
        {
            bool result = _moleculeProcessor.ValidateString("C(C)C");
            Assert.IsTrue(result, "\'C(C)C\' should pass test");
        }

        [TestMethod]
        public void MoleculeProcesssor_testEmptyBracketsWithFollowOnC()//Erroneous
        {
            bool result = _moleculeProcessor.ValidateString("()C");
            Assert.IsFalse(result, "\'()C\' should Fail test");
        }

        [TestMethod]
        public void MoleculeProcesssor_testBasicMoleculeWithNestedBrackets()//Erroneous
        {
            bool result = _moleculeProcessor.ValidateString("C((C))C");
            Assert.IsFalse(result, "'C((C))C' should Fail test");
        }

        [TestMethod]
        public void MoleculeProcesssor_testBasicMoleculeValidNestedBrackets()//Typical
        {
            bool result = _moleculeProcessor.ValidateString("C(C(C))C");
            Assert.IsTrue(result, "\'C(C(C))C\' should pass test");
        }

        [TestMethod]
        public void MoleculeProcesssor_testBasicMoleculeBracketMismatch()//Erroneous
        {
            bool result = _moleculeProcessor.ValidateString("C(C(C)C");
            Assert.IsFalse(result, "\'C(C(C)C\' should Fail test");
            result = _moleculeProcessor.ValidateString("CC(C))C");
            Assert.IsFalse(result, "\'CC(C))C\' should Fail test");
        }
    }
}