using System;
using System.IO;
using System.Linq;

using filex.ML;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace filex.tests.ML
{
    [TestClass]
    public class ModelTrainerTests
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ModelTrainer_FeatureExtraction_NullPath()
        {
            ModelTrainer.FeatureExtraction(null);
        }

        [TestMethod]
        [ExpectedException(typeof(DirectoryNotFoundException))]
        public void ModelTrainer_FeatureExtraction_FNF()
        {
            ModelTrainer.FeatureExtraction("nullo");
        }

        [TestMethod]
        public void ModelTrainer_FeatureExtraction_ValidPath()
        {
            var files = ModelTrainer.FeatureExtraction(Environment.GetEnvironmentVariable("windir"));

            Assert.IsTrue(files.Any());
        }
    }
}