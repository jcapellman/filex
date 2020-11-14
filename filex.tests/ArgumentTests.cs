using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

using filex.Arguments.Base;
using filex.Enums;
using filex.Objects;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace filex.tests
{
    [TestClass]
    public class ArgumentTests
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void NullArguments()
        {
            var parsed = ArgumentParser.Parse(null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void EmptyArguments()
        {
            var parsed = ArgumentParser.Parse(new string[]{});
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void KeyNoValue()
        {
            var parsed = ArgumentParser.Parse(new string[] { "trainingpath" });
        }

        [TestMethod]
        public void UsageTextReturns()
        {
            var parsed = ArgumentParser.BuildHelpContext();

            Assert.IsNotNull(parsed);
            Assert.IsTrue(parsed.Any());
        }

        
        [TestMethod]
        public void InvalidBlankspaceOption()
        {
            var parsed = ArgumentParser.Parse(new string[] { "filea", " " });

            Assert.AreEqual(string.Empty, parsed.ArgResponse.FileNameForClassification);
            Assert.IsFalse(parsed.ValidOption);
        }

        [TestMethod]
        public void InvalidOption()
        {
            var parsed = ArgumentParser.Parse(new string[] {"filea", "as"});

            Assert.AreEqual(string.Empty, parsed.ArgResponse.FileNameForClassification);
            Assert.IsFalse(parsed.ValidOption);
        }

        [TestMethod]
        public void InvalidMode()
        {
            var parsed = ArgumentParser.Parse(new string[] { "mode", "fileName" });

            Assert.IsFalse(parsed.ArgResponse.Mode == OperationMode.MODEL_PREDICTION);
            Assert.IsFalse(parsed.ValidOption);
        }

        [TestMethod]
        public void ValidPredictModeButNoFilePath()
        {
            var parsed = ArgumentParser.Parse(new string[] { "mode", OperationMode.MODEL_PREDICTION.ToString() });

            Assert.IsTrue(parsed.ArgResponse.Mode == OperationMode.MODEL_PREDICTION);
            Assert.IsFalse(parsed.ValidOption);
        }

        [TestMethod]
        public void ValidPredictModeWithFilePath()
        {
            var fileName = Path.GetRandomFileName();

            File.WriteAllText(fileName, "");

            var parsed = ArgumentParser.Parse(new string[] { "mode", OperationMode.MODEL_PREDICTION.ToString(), "file",  fileName});

            Assert.IsTrue(parsed.ArgResponse.Mode == OperationMode.MODEL_PREDICTION);
            Assert.IsTrue(parsed.ValidOption);
        }

        [TestMethod]
        public void PredictNullFile()
        {
            var parsed = ArgumentParser.Parse(new string[] { "mode", OperationMode.MODEL_PREDICTION.ToString(), "file", null });

            Assert.IsTrue(parsed.ArgResponse.Mode == OperationMode.MODEL_PREDICTION);
            Assert.IsFalse(parsed.ValidOption);
        }

        [TestMethod]
        public void TrainNullFile()
        {
            var parsed = ArgumentParser.Parse(new string[] { "mode", OperationMode.MODEL_TRAIN.ToString(), "trainingpath", null });

            Assert.IsTrue(parsed.ArgResponse.Mode == OperationMode.MODEL_TRAIN);
            Assert.IsFalse(parsed.ValidOption);
        }

        [TestMethod]
        public void UnselectableMode()
        {
            var parsed = ArgumentParser.Parse(new[] {"mode", OperationMode.UNSELECTABLE.ToString() });

            Assert.IsTrue(parsed.ArgResponse.Mode == OperationMode.UNSELECTABLE);
            Assert.IsFalse(parsed.ValidOption);
        }

        [TestMethod]
        public void ValidModeButNoTrainingPath()
        {
            var parsed = ArgumentParser.Parse(new string[] { "mode", OperationMode.MODEL_TRAIN.ToString() });

            Assert.IsTrue(parsed.ArgResponse.Mode == OperationMode.MODEL_TRAIN);
            Assert.IsFalse(parsed.ValidOption);
        }

        [TestMethod]
        public void ValidModeWithTrainingPath()
        {
            var parsed = ArgumentParser.Parse(new string[] { "mode", OperationMode.MODEL_TRAIN.ToString(), "trainingpath", "c:\\Windows" });

            Assert.IsTrue(parsed.ArgResponse.Mode == OperationMode.MODEL_TRAIN);
            Assert.IsTrue(parsed.ValidOption);
        }

        [TestMethod]
        public void InvalidVerbose()
        {
            var parsed = ArgumentParser.Parse(new string[] { "verbose", "blah" });

            Assert.IsFalse(parsed.ArgResponse.Verbose);
        }

        [TestMethod]
        public void ValidVerbose()
        {
            var parsed = ArgumentParser.Parse(new string[] { "verbose", "true" });

            Assert.IsTrue(parsed.ArgResponse.Verbose);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void NullArgumentResponseItemConstructor()
        {
            var argumentResponse = new ArgumentResponseItem(null);
        }

        [TestMethod]
        public void InvalidProperty()
        {
            var argumentResponse = new ArgumentResponseItem(new List<BaseArgument>());

            argumentResponse.UpdateProperty("blah", null);
        }
    }
}