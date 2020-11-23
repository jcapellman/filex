using Microsoft.ML.Data;

namespace filex.Objects
{
    public class ModelTrainingMetricsResponse
    {
        public double AUC { get; private set; }

        public double Entropy { get; private set; }

        public ModelTrainingMetricsResponse(CalibratedBinaryClassificationMetrics metric)
        {
            AUC = metric.AreaUnderRocCurve;
            Entropy = metric.Entropy;
        }
    }
}