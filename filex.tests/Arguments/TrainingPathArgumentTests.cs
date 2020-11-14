using filex.Arguments;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace filex.tests.Arguments
{
    [TestClass]
    public class TrainingPathArgumentTests
    {
        [TestMethod]
        public void TrainingPathArgument_UsageText()
        {
            var tpArgument = new TrainingPathArgument();

            Assert.IsNotNull(tpArgument.UsageText);
        }
    }
}