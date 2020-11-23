using Microsoft.ML.Data;

namespace filex.Objects
{
    public class ModelTrainingMetricsResponse
    {
        public double AUC { get; }

        public double Entropy { get; }

        public ModelTrainingMetricsResponse(CalibratedBinaryClassificationMetrics metric)
        {
            AUC = metric.AreaUnderRocCurve;
            Entropy = metric.Entropy;
        }
    }
}