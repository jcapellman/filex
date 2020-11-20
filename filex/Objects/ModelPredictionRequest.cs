using System;
using System.IO;

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

        public ModelPredictionRequest(string fileName)
        {
            if (string.IsNullOrEmpty(fileName))
            {
                throw new ArgumentNullException(nameof(fileName));
            }

            if (!File.Exists(fileName))
            {
                throw new FileNotFoundException($"Could not find {fileName}");
            }

            Label = fileName.Contains("benign");

            var fileBytes = File.ReadAllBytes(fileName);

            IsPE = System.Text.Encoding.ASCII.GetString(fileBytes.AsSpan().Slice(0, 2)) == "MZ" ? 1.0f : 0.0f;

            FileSize = fileBytes.Length;
        }
    }
}