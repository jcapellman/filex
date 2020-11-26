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
        public void PCAPParser_BenginPCAPArguments()
        {
            var pParser = new PCAPParser();

            var path = Path.Combine(AppContext.BaseDirectory, @"..\..\..\Samples\benign_pcap.pcap");

            var result = pParser.IsParseable(null, path);

            Assert.IsTrue(result);
        }
    }
}