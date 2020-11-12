using System.IO;

using filex.Arguments.Base;
using filex.Objects;

namespace filex.Arguments
{
    public class TrainingPathArgument : BaseArgument
    {
        public override string Argument => "trainingpath";

        public override string UsageText => "Path for a folder containing training data";

        public override object DefaultValue => string.Empty;

        public override string PropertyMap => nameof(ArgumentResponseItem.TrainingPath);

        public override bool ValidArgument(string argValue) => !string.IsNullOrEmpty(argValue) && Directory.Exists(argValue);

        public override object GetValue(string argValue) => argValue;
    }
}