using System;

using filex.Arguments;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace filex.tests.Arguments
{
    [TestClass]
    public class VersionArgumentTests
    {
        [TestMethod]
        public void VersionArgumentChecks()
        {
            var parsed = ArgumentParser.Parse(new[] { "version" });

            Assert.IsNotNull(parsed);
            Assert.IsFalse(parsed.ValidOption);
        }

        [TestMethod]
        public void VersionUsageText()
        {
            var verArgument = new VersionArgument();

            Assert.IsNotNull(verArgument.UsageText);
        }

        [TestMethod]
        public void VersionArgument_SupportDefault()
        {
            var verArgument = new VersionArgument();

            Assert.IsFalse(verArgument.SupportsDefaultValue);
        }

        [TestMethod]
        [ExpectedException(typeof(NotImplementedException))]
        public void VersionArgumentNotImplementedDefaultValue()
        {
            var version = new VersionArgument();

            var val = version.DefaultValue;
        }

        [TestMethod]
        [ExpectedException(typeof(NotImplementedException))]
        public void VersionArgumentNotImplementedPropertyMap()
        {
            var version = new VersionArgument();

            var val = version.PropertyMap;
        }

        [TestMethod]
        [ExpectedException(typeof(NotImplementedException))]
        public void VersionArgumentValidOption()
        {
            var version = new VersionArgument();

            version.ValidArgument(string.Empty);
        }

        [TestMethod]
        public void VersionArgumentGetValue()
        {
            var version = new VersionArgument();

            Assert.IsNotNull(version.GetValue(string.Empty));
        }
    }
}