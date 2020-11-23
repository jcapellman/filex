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

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ModelTrainer_TrainModel_NullPath()
        {
            ModelTrainer.TrainModel(null);
        }

        [TestMethod]
        [ExpectedException(typeof(DirectoryNotFoundException))]
        public void ModelTrainer_TrainModel_InvalidPath()
        {
            ModelTrainer.TrainModel("null");
        }

        [TestMethod]
        public void ModelTrainer_TrainModel_ValidPath()
        {
            var metrics = ModelTrainer.TrainModel(Path.Combine(AppContext.BaseDirectory, @"..\..\..\Samples"));

            Assert.IsTrue(metrics.AUC > 0.0);
            Assert.IsTrue(metrics.Entropy > 0.0);
            Assert.IsTrue(metrics.F1Score == 0.0);
            Assert.IsTrue(metrics.NegativeRecall > 0.0);
            Assert.IsTrue(metrics.PositiveRecall == 0.0);
        }
    }
}