using System.IO;

using filex.Arguments;
using filex.Enums;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace filex.tests.Arguments
{
    [TestClass]
    public class FileArgumentTests
    {
        [TestMethod]
        public void InvalidFile()
        {
            var parsed = ArgumentParser.Parse(new string[] { "file", "as" });

            Assert.IsTrue(string.IsNullOrEmpty(parsed.ArgResponse.FileNameForClassification));
            Assert.IsFalse(parsed.ValidOption);
        }

        [TestMethod]
        public void FileArgument_UsageText()
        {
            var fArgument = new FileArgument();

            Assert.IsNotNull(fArgument.UsageText);
        }

        [TestMethod]
        public void ValidFile()
        {
            var fileName = Path.GetRandomFileName();

            File.WriteAllText(fileName, "test");

            var parsed = ArgumentParser.Parse(new string[] { "file", fileName, "mode", OperationMode.MODEL_PREDICTION.ToString() });

            Assert.IsNotNull(parsed.ArgResponse.FileNameForClassification);
            Assert.IsTrue(parsed.ValidOption);
        }
    }
}
