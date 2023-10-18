using MoleculeNamer;

namespace MoleculeNamerTests.UnitTests
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
        public void testEmptyString()
        {
            bool result = _moleculeProcessor.validateString("");
            Assert.IsFalse(result, "emptyString should fail test");
        }

        [TestMethod]
        public void testOpenBracket()
        {
            bool result = _moleculeProcessor.validateString("(");
            Assert.IsFalse(result, "Open bracket should fail test");
        }

        [TestMethod]
        public void testCloseBracket()
        {
            bool result = _moleculeProcessor.validateString(")");
            Assert.IsFalse(result, "\')\' bracket should fail test");
        }

        [TestMethod]
        public void testBasicMolecule()
        {
            bool result = _moleculeProcessor.validateString("C");
            Assert.IsTrue(result, "\'C\' should pass test");
        }
    }
}