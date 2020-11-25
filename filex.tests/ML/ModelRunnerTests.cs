using System.Collections.Generic;
using System.IO;

using filex.Arguments.Base;
using filex.Objects;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace filex.tests.ML
{
    [TestClass]
    public class ModelRunnerTests
    {
        [TestMethod]
        [ExpectedException(typeof(FileNotFoundException))]
        public void ModelRunner_FileNotExistsValidModelRun()
        {
            var model = new ModelRunner();

            var response = model.RunModel(new ArgumentResponseItem(new List<BaseArgument>()));

            Assert.IsNotNull(response);
            Assert.IsFalse(response.Prediction);
        }
    }
}