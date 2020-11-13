using System.Reflection;

using filex.Arguments.Base;

namespace filex.Arguments
{
    public class VersionArgument : BaseArgument
    {
        public override string Argument => "version";

        public override string UsageText => "Retrieves the version of the application";

        public override object DefaultValue => null;

        public override string PropertyMap => null;

        public override bool ValidArgument(string argValue) => false;

        public override object GetValue(string argValue) => Assembly.GetExecutingAssembly().GetName().Version;
    }
}