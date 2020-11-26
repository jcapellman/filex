using System;
using System.IO;

using filex.Parsers;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace filex.tests.Parsers
{
    [TestClass]
    public class PCAPParserTests
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void PCAPParser_NullArguments()
        {
            var pParser = new PCAPParser();

            pParser.IsParseable(null, null);
        }

        [TestMethod]
        [ExpectedException(typeof(FileNotFoundException))]
        public void PCAPParser_FNFArguments()
        {
            var pParser = new PCAPParser();

            pParser.IsParseable(null, "wick");
        }

        [TestMethod]
        public void PCAPParser_InvalidFileArguments()
        {
            var pParser = new PCAPParser();

            var result = pParser.IsParseable(null, Path.Combine(Environment.GetEnvironmentVariable("windir") ?? string.Empty, "explorer.exe"));

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void PCAPParser_BenginPCAPArguments()
        {
            var pParser = new PCAPParser();

            Assert.IsTrue(pParser.Name == "PCAP");

            var path = Path.Combine(AppContext.BaseDirectory, @"..\..\..\Samples\benign_pcap.pcap");

            var result = pParser.IsParseable(null, path);

            Assert.IsTrue(result);

            var modelRun = pParser.RunModel(null, path);

            Assert.IsNotNull(modelRun);
        }
    }
}