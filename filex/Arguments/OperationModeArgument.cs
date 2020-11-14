using System;

using filex.Arguments.Base;
using filex.Enums;
using filex.Objects;

namespace filex.Arguments
{
    public class OperationModeArgument : BaseArgument
    {
        public override string Argument => "mode";

        public override string UsageText => $"Offers support for either training or prediction (Options:" 
                                            + $" {string.Join(',', OperationModeExtensions.GetOperationModeList())})";

        public override object DefaultValue => OperationMode.UNSELECTABLE;

        public override string PropertyMap => nameof(ArgumentResponseItem.Mode);

        public override bool ValidArgument(string argValue)
        {
            if (!Enum.TryParse(argValue, true, out OperationMode val))
            {
                return false;
            }

            return val != OperationMode.UNSELECTABLE;
        }

        public override object GetValue(string argValue) => Enum.Parse(typeof(OperationMode), argValue);

        public override bool KeyOnly => false;

        public override bool SupportsDefaultValue => false;
    }
}
