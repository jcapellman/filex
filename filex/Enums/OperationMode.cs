using System;
using System.Collections.Generic;
using System.Linq;

namespace filex.Enums
{
    public enum OperationMode
    {
        UNSELECTABLE,
        MODEL_TRAIN,
        MODEL_PREDICTION
    }

    public static class OperationModeExtensions
    {
        public static List<string> GetOperationModeList() => Enum.GetNames(typeof(OperationMode)).ToList();
    }
}