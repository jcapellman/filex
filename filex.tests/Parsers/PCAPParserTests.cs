using System;

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
        [ExpectedException(typeof(ArgumentNullException))]
        public void PCAPParser_FNFArguments()
        {
            var pParser = new PCAPParser();

            pParser.IsParseable(null, "wick");
        }
    }
}