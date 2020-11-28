using Microsoft.ML.Data;

namespace filex.Objects
{
    public class ModelTrainingMetricsResponse
    {
        public double AUC { get; }

        public double Entropy { get; }

        public double F1Score { get; }

        public double PositiveRecall { get; }

        public double NegativeRecall { get; }

        public double FalsePositiveRate { get; set; }

        public ModelTrainingMetricsResponse(CalibratedBinaryClassificationMetrics metric)
        {
            AUC = metric.AreaUnderRocCurve;
            Entropy = metric.Entropy;
            F1Score = metric.F1Score;
            PositiveRecall = metric.PositiveRecall;
            NegativeRecall = metric.NegativeRecall;
        }

        public ModelTrainingMetricsResponse(AnomalyDetectionMetrics metrics)
        {
            AUC = metrics.AreaUnderRocCurve;
            FalsePositiveRate = metrics.DetectionRateAtFalsePositiveCount;
        }
    }
}