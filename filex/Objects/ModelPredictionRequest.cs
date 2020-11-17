using Microsoft.ML.Data;

namespace filex.Objects
{
    public class ModelPredictionRequest
    {
        [LoadColumn(0)]
        public float FileSize { get; set; }

        [LoadColumn(1)]
        public float IsPE { get; set; }

        [LoadColumn(2)]
        public bool Label { get; set; }
    }
}