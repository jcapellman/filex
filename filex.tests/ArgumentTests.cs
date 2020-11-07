using System;
using System.IO;

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
        [ExpectedException(typeof(FileNotFoundException))]
        public void FileNotFoundArguments()
        {
            var parsed = ArgumentParser.Parse(new string[] { "Testing123.123" });
        }

        [TestMethod]
        public void FileFoundArguments()
        {
            var tempFile = Path.GetRandomFileName();

            File.WriteAllText(tempFile, "test");

            var parsed = ArgumentParser.Parse(new string[] { tempFile });
        }
    }
}