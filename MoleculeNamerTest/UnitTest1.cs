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
        public void MoleculeProcesssor_testBasicMolecule()
        {
            bool result = _moleculeProcessor.validateString("C)");
            Assert.IsFalse(result, "\'C)\' should fail test");
        }

        [TestMethod]
        public void MoleculeProcesssor_testBasicMolecule()
        {
            bool result = _moleculeProcessor.validateString("C(");
            Assert.IsFalse(result, "\'C(\' should fail test");
        }

        [TestMethod]
        public void MoleculeProcesssor_testBasicMolecule()
        {
            bool result = _moleculeProcessor.validateString("()");
            Assert.IsFalse(result, "\'()\' should Fail test");
        }

        [TestMethod]
        public void MoleculeProcesssor_testBasicMolecule()
        {
            bool result = _moleculeProcessor.validateString("C()");
            Assert.IsFalse(result, "\'C()\' should Fail test");
        }
       
        [TestMethod]
        public void MoleculeProcesssor_testBasicMolecule()
        {
            bool result = _moleculeProcessor.validateString("C()C");
            Assert.IsFalse(result, "\'C()C\' should fail test");
        }
        [TestMethod]
        public void MoleculeProcesssor_testBasicMolecule()
        {
            bool result = _moleculeProcessor.validateString("C(C)");
            Assert.IsFalse(result, "\'C(C)\' should fail test");
        }
        [TestMethod]
        public void MoleculeProcesssor_testBasicMolecule()
        {
            bool result = _moleculeProcessor.validateString("C(C)C");
            Assert.IsTrue(result, "\'C(C)C\' should pass test");
        }

        [TestMethod]
        public void MoleculeProcesssor_testBasicMolecule()
        {
            bool result = _moleculeProcessor.validateString("()C");
            Assert.IsFalse(result, "\'()C\' should Fail test");
        }
        
        [TestMethod]
        public void MoleculeProcesssor_testBasicMolecule()
        {
            bool result = _moleculeProcessor.validateString("C((C))C");
            Assert.IsFalse(result, "\'C\' should Fail test");
        }
        
        [TestMethod]
        public void MoleculeProcesssor_testBasicMolecule()
        {
            bool result = _moleculeProcessor.validateString("C(C(C))C");
            Assert.IsPass(result, "\'C(C(C))C\' should pass test");
        }
        
        [TestMethod]
        public void MoleculeProcesssor_testBasicMolecule()
        {
            bool result = _moleculeProcessor.validateString("C(C(C)C");
            Assert.IsFalse(result, "\'C(C(C)C\' should Fail test");
        }

    }
}