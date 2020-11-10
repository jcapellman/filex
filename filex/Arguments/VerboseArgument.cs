using filex.Arguments.Base;
using filex.Objects;

namespace filex.Arguments
{
    public class VerboseArgument : BaseArgument
    {
        public override string Argument => "verbose";

        public override object DefaultValue => false;

        public override string PropertyMap => nameof(ArgumentResponseItem.Verbose);

        public override bool ValidArgument(string argValue) => bool.TryParse(argValue, out var _);

        public override object GetValue(string argValue) => bool.Parse(argValue);
    }
}