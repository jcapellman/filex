using filex.Arguments;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace filex.tests.Arguments
{
    [TestClass]
    public class VerboseArgumentTests
    {
        [TestMethod]
        public void VerboseArgument_UsageText()
        {
            var vArgument = new VerboseArgument();

            Assert.IsNotNull(vArgument.UsageText);
        }
    }
}