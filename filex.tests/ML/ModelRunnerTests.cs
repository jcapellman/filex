using System;
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
        public void ModelRunner_NotFound()
        {
            var model = new ModelRunner("Testing");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ModelRunner_NullFilename()
        {
            var model = new ModelRunner(null);
        }

        [TestMethod]
        public void ModelRunner_FileExistsValidModelRun()
        {
            var file = Path.GetRandomFileName();

            File.WriteAllText(file, "");

            var model = new ModelRunner(file);

            var response = model.RunModel(new ArgumentResponseItem(new List<BaseArgument>()));

            Assert.IsNotNull(response);
            Assert.IsFalse(response.Prediction);
        }

        [TestMethod]
        public void ModelRunner_FileExistsButInvalidModel()
        {
            var file = Path.GetRandomFileName();

            File.WriteAllText(file, "");

            var model = new ModelRunner(file);
        }
    }
}
