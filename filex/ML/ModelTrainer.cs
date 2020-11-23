using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

using filex.Common;
using filex.Objects;

using Microsoft.ML;

namespace filex.ML
{
    public class ModelTrainer
    {
        public static IEnumerable<ModelPredictionRequest> FeatureExtraction(string path)
        {
            if (string.IsNullOrEmpty(path))
            {
                throw new ArgumentNullException($"Path was not set");
            }

            if (!Directory.Exists(path))
            {
                throw new DirectoryNotFoundException($"Path: {path} not found");
            }

            var files = Directory.GetFiles(path);

            return files.Select(file => new ModelPredictionRequest(file));
        }

        public static ModelTrainingMetricsResponse TrainModel(string trainingDataPath)
        {
            if (string.IsNullOrEmpty(trainingDataPath))
            {
                throw new ArgumentNullException($"TrainingDataPath was not set");
            }

            var mlContext = new MLContext(1985);

            var data = FeatureExtraction(trainingDataPath);

            var dataView = mlContext.Data.LoadFromEnumerable(data);

            var split = mlContext.Data.TrainTestSplit(dataView, testFraction: .5);

            var pipeline = mlContext.Transforms.Concatenate(
                    "Features", nameof(ModelPredictionRequest.FileSize), nameof(ModelPredictionRequest.IsPE))
                .Append(mlContext.BinaryClassification.Trainers.FastTree(
                    labelColumnName: nameof(ModelPredictionRequest.Label),
                    featureColumnName: "Features"));

            ITransformer trainedModel = pipeline.Fit(split.TrainSet);

            var predictions = trainedModel.Transform(split.TestSet);

            mlContext.Model.Save(trainedModel, predictions.Schema, Constants.DEFAULT_MODEL_FILENAME);

            var metrics = mlContext.BinaryClassification.Evaluate(
                data: predictions,
                labelColumnName: nameof(ModelPredictionRequest.Label),
                scoreColumnName: "Score",
                predictedLabelColumnName: nameof(ModelPredictionRequest.Label));

            return new ModelTrainingMetricsResponse(metrics);
        }
    }
}