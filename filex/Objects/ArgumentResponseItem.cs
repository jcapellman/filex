using filex.Enums;

namespace filex.Objects
{
    public class ArgumentResponseItem
    {
        public string FileNameForClassification { get; set; }

        public OperationMode Mode { get; set; }

        public ArgumentResponseItem()
        {
            Mode = OperationMode.MODEL_PREDICTION;
        }
    }
}