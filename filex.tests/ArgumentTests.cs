using System;
using System.Collections.Generic;
using System.IO;
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
        public void InvalidOption()
        {
            var parsed = ArgumentParser.Parse(new string[] {"filea", "as"});

            Assert.AreEqual(string.Empty, parsed.ArgResponse.FileNameForClassification);
            Assert.IsFalse(parsed.ValidOption);
        }

        [TestMethod]
        public void InvalidFile()
        {
            var parsed = ArgumentParser.Parse(new string[] { "file", "as" });

            Assert.IsTrue(string.IsNullOrEmpty(parsed.ArgResponse.FileNameForClassification));
            Assert.IsFalse(parsed.ValidOption);
        }

        [TestMethod]
        public void ValidFile()
        {
            var fileName = Path.GetRandomFileName();

            File.WriteAllText(fileName, "test");

            var parsed = ArgumentParser.Parse(new string[] { "file",  fileName});

            Assert.IsNotNull(parsed.ArgResponse.FileNameForClassification);
            Assert.IsTrue(parsed.ValidOption);
        }

        [TestMethod]
        public void InvalidMode()
        {
            var parsed = ArgumentParser.Parse(new string[] { "mode", "fileName" });

            Assert.IsTrue(parsed.ArgResponse.Mode == OperationMode.MODEL_PREDICTION);
            Assert.IsFalse(parsed.ValidOption);
        }

        [TestMethod]
        public void ValidMode()
        {
            var parsed = ArgumentParser.Parse(new string[] { "mode", OperationMode.MODEL_TRAIN.ToString() });

            Assert.IsTrue(parsed.ArgResponse.Mode == OperationMode.MODEL_TRAIN);
            Assert.IsFalse(parsed.ValidOption);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void OddArgumentCount()
        {
            var parsed = ArgumentParser.Parse(new[] {"file", "as", "verbose"});
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