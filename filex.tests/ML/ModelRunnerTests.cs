using System;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace filex.tests.ML
{
    [TestClass]
    public class ModelRunnerTests
    {
        [TestMethod]
        [ExpectedException(typeof(FileNotFoundException))]
        public void ModelNotFound()
        {
            var model = new ModelRunner("Testing");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ModelNullFound()
        {
            var model = new ModelRunner(null);
        }
    }
}
