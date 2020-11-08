using System;
using System.IO;

using filex.Common;
using filex.Objects;

namespace filex
{
    public class ModelRunner
    {
        public ModelRunner(string modelFile = Constants.DEFAULT_MODEL_FILENAME)
        {
            if (string.IsNullOrEmpty(modelFile))
            {
                throw new ArgumentNullException(nameof(modelFile));
            }

            LoadModel(modelFile);
        }

        private static void LoadModel(string fileName)
        {
            if (!File.Exists(fileName))
            {
                throw new FileNotFoundException(fileName);
            }

            // Load sie model
        }

        public ModelPredictionResponse RunModel(string fileName)
        {
            var response = new ModelPredictionResponse();

            // Run sie model

            return response;
        }
    }
}