using Microsoft.ML.Data;

namespace filex.Objects
{
    public class ModelPredictionResponse
    {
        [ColumnName("PredictedLabel")]
        public bool Prediction;

        public float Probability;

        public float Score;
    }
}