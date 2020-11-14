using System;
using System.Reflection;

using filex.Arguments.Base;

namespace filex.Arguments
{
    public class VersionArgument : BaseArgument
    {
        public override string Argument => "version";

        public override string UsageText => "Retrieves the version of the application";

        public override object DefaultValue => throw new NotImplementedException();

        public override string PropertyMap => throw new NotImplementedException();

        public override bool ValidArgument(string argValue) => throw new NotImplementedException();

        public override object GetValue(string argValue) => Assembly.GetExecutingAssembly().GetName().Version;

        public override bool KeyOnly => true;
    }
}