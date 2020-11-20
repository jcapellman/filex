using System;
using System.IO;

using filex.Objects;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace filex.tests.Objects
{
    [TestClass]
    public class ModelPredictionRequestTests
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ModelPredictionRequest_Nullfilename()
        {
            var request = new ModelPredictionRequest(null);
        }

        [TestMethod]
        [ExpectedException(typeof(FileNotFoundException))]
        public void ModelPredictionRequest_DNE_File()
        {
            var request = new ModelPredictionRequest("testingo");
        }

        [TestMethod]
        public void ModelPredictionRequest_PE_File()
        {
            var explorerPath =
                Path.Combine(Environment.GetEnvironmentVariable("windir") ?? string.Empty, "explorer.exe");

            var request = new ModelPredictionRequest(explorerPath);

            Assert.AreEqual(1.0, request.IsPE);
            Assert.IsFalse(request.Label);
            Assert.AreEqual(new FileInfo(explorerPath).Length, request.FileSize);
        }
    }
}
