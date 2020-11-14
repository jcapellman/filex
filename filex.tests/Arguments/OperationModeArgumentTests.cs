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
    }
}