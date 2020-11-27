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
        public void ModelRunner_FileNotExistsValidModelRun()
        {
            var model = new ModelRunner();

            var response = model.RunModel(new ArgumentResponseItem(new List<BaseArgument>()));

            Assert.IsNotNull(response);
            Assert.IsFalse(response.Prediction);
        }

        [TestMethod]
        public void ModelRunner_PCAPTest()
        {
            var model = new ModelRunner();

            var argumentResponseItem = new ArgumentResponseItem(new List<BaseArgument>());

            argumentResponseItem.FileNameForClassification = Path.Combine(AppContext.BaseDirectory, @"..\..\..\Samples\benign_pcap.pcap");
            
            var response = model.RunModel(argumentResponseItem);

            Assert.IsNotNull(response);
            Assert.IsFalse(response.Prediction);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void ModelRunner_EXETest()
        {
            var model = new ModelRunner();

            var argumentResponseItem = new ArgumentResponseItem(new List<BaseArgument>());

            argumentResponseItem.FileNameForClassification = Path.Combine(Environment.GetEnvironmentVariable("windir") ?? string.Empty, "explorer.exe");

            var response = model.RunModel(argumentResponseItem);

            Assert.IsNotNull(response);
            Assert.IsFalse(response.Prediction);
        }
    }
}