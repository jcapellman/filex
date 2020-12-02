using System;
using System.IO;

using filex.Parsers;
using filex.Parsers.PCAP;
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
        [ExpectedException(typeof(ArgumentNullException))]
        public void PCAPParser_Train_NullPath()
        {
            var pParser = new PCAPParser();

            var result = pParser.TrainModel(null);
        }

        [TestMethod]
        [ExpectedException(typeof(DirectoryNotFoundException))]
        public void PCAPParser_Train_InvalidPath()
        {
            var pParser = new PCAPParser();

            var result = pParser.TrainModel(@"c:\wicko");
        }

        [TestMethod]
        public void PCAPParser_Train_ValidPath()
        {
            var pParser = new PCAPParser();

            var path = Path.Combine(AppContext.BaseDirectory, @"..\..\..\Samples\pcap\");

            var result = pParser.TrainModel(path);

            Assert.IsTrue(result.AUC == 0.0);

            Assert.IsTrue(result.F1Score == 0.0);
        }

        [TestMethod]
        public void ddPCAPParser_BenginPCAPArguments()
        {
            var pParser = new PCAPParser();

            Assert.IsTrue(pParser.Name == "PCAP");

            var path = Path.Combine(AppContext.BaseDirectory, @"..\..\..\Samples\pcap\benign_pcap.pcap");

            var result = pParser.IsParseable(null, path);

            Assert.IsTrue(result);

            pParser.LoadModel();

            var modelRun = pParser.RunModel(null, path);

            Assert.IsNotNull(modelRun);
        }
    }
}