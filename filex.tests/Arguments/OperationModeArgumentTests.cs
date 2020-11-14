using filex.Arguments;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace filex.tests.Arguments
{
    [TestClass]
    public class OperationModeArgumentTests
    {
        [TestMethod]
        public void OperationModeArgument_UsageText()
        {
            var omArgument = new OperationModeArgument();

            Assert.IsNotNull(omArgument.UsageText);
        }

        [TestMethod]
        public void OperationModeArgument_DefaultValue()
        {
            var omArgument = new OperationModeArgument();

            Assert.IsFalse(omArgument.SupportsDefaultValue);
        }
    }
}