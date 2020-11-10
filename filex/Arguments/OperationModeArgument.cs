﻿using System;

using filex.Arguments.Base;
using filex.Enums;
using filex.Objects;

namespace filex.Arguments
{
    public class OperationModeArgument : BaseArgument
    {
        public override string Argument => "mode";

        public override string PropertyMap => nameof(ArgumentResponseItem.Mode);

        public override bool ValidArgument(string argValue) => Enum.TryParse(argValue, true, out OperationMode _);

        public override object GetValue(string argValue) => Enum.Parse(typeof(OperationMode), argValue);
    }
}