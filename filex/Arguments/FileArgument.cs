using System.IO;

using filex.Arguments.Base;
using filex.Objects;

namespace filex.Arguments
{
    public class FileArgument : BaseArgument
    {
        public override string Argument => "file";

        public override string UsageText => "Path to the file to predict";

        public override object DefaultValue => string.Empty;

        public override string PropertyMap => nameof(ArgumentResponseItem.FileNameForClassification);

        public override bool ValidArgument(string argValue) => !string.IsNullOrEmpty(argValue) && File.Exists(argValue);

        public override object GetValue(string argValue) => argValue;

        public override bool KeyOnly => false;

        public override bool SupportsDefaultValue => false;
    }
}