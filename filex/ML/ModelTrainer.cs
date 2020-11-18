using System;
using System.Collections.Generic;
using System.IO;

using filex.Objects;

using Microsoft.ML;

namespace filex.ML
{
    public class ModelTrainer
    {
        private static List<ModelPredictionRequest> FeatureExtraction(string path)
        {
            var files = Directory.GetFiles(path);

            var requests = new List<ModelPredictionRequest>();

            foreach (var file in files)
            {
                var fi = new FileInfo(file);

                var fileBytes = File.ReadAllBytes(file);

                var isPe = System.Text.Encoding.ASCII.GetString(fileBytes.AsSpan().Slice(0, 2)) == "MZ";

                requests.Add( new ModelPredictionRequest { 
                    Label = fi.Name.Contains("benign"),
                    IsPE = isPe ? 1.0f : 0.0f,
                    FileSize = fi.Length
                    });
            }

            return requests;
        }

        public static bool TrainModel(string trainingDataPath)
        {
            var mlContext = new MLContext(1985);

            var data = FeatureExtraction(trainingDataPath);

            var dataView = mlContext.Data.LoadFromEnumerable(data);

            var split = mlContext.Data.TrainTestSplit(dataView);
            
            var pipeline = mlContext.Transforms.Concatenate(
                    "Features", nameof(ModelPredictionRequest.FileSize), nameof(ModelPredictionRequest.IsPE))
                .Append(mlContext.BinaryClassification.Trainers.FastTree(labelColumnName: nameof(ModelPredictionRequest.Label), 
                    featureColumnName: "Features"));

            ITransformer trainedModel = pipeline.Fit(split.TrainSet);

            var predictions = trainedModel.Transform(split.TestSet);

            var metrics = mlContext.BinaryClassification.Evaluate(data: predictions, 
                labelColumnName: nameof(ModelPredictionRequest.Label), scoreColumnName: "Score");

            Console.WriteLine($"F1 Score: {metrics.F1Score}");

            return true;
        }
    }
}