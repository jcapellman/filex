using System;
using System.Collections.Generic;
using System.Linq;
using filex.Arguments.Base;
using filex.Enums;

namespace filex.Objects
{
    public class ArgumentResponseItem
    {
        public string FileNameForClassification { get; set; }

        public OperationMode Mode { get; set; }

        public bool Verbose { get; set; }

        public string TrainingPath { get; set; }

        public ArgumentResponseItem(IEnumerable<BaseArgument> arguments)
        {
            if (arguments == null)
            {
                throw new ArgumentNullException(nameof(arguments));
            }

            foreach (var argument in arguments.Where(a => !a.KeyOnly))
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

        public bool IsValid()
        {
            switch (Mode)
            {
                case OperationMode.MODEL_PREDICTION:
                    return !string.IsNullOrEmpty(FileNameForClassification);
                case OperationMode.MODEL_TRAIN:
                    return !string.IsNullOrEmpty(TrainingPath);
                default:
                    return false;
            }
        }
    }
}