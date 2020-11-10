using System;
using System.Collections.Generic;

using filex.Arguments.Base;
using filex.Enums;

namespace filex.Objects
{
    public class ArgumentResponseItem
    {
        public string FileNameForClassification { get; set; }

        public OperationMode Mode { get; set; }

        public bool Verbose { get; set; }

        public ArgumentResponseItem(IEnumerable<BaseArgument> arguments)
        {
            foreach (var argument in arguments)
            {
                UpdateProperty(argument.PropertyMap, argument.DefaultValue);
            }
        }

        public bool UpdateProperty(string propertyName, object value)
        {
            var property = this.GetType().GetProperty(propertyName);

            if (property == null)
            {
                Console.WriteLine($"Could not map {propertyName} to the object");

                return false;
            }

            property.SetValue(this, value, null);

            return true;
        }

        public bool IsValid() =>
            Mode switch
            {
                OperationMode.MODEL_PREDICTION => !string.IsNullOrEmpty(FileNameForClassification),
                OperationMode.MODEL_TRAIN => false,
                _ => false
            };
    }
}