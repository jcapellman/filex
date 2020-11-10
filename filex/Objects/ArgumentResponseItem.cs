using filex.Enums;

namespace filex.Objects
{
    public class ArgumentResponseItem
    {
        public string FileNameForClassification { get; set; }

        public OperationMode Mode { get; set; }

        public bool Verbose { get; set; }

        public ArgumentResponseItem()
        {
            Mode = OperationMode.MODEL_PREDICTION;
            Verbose = false;
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