using System;
using System.Collections.Generic;
using System.IO;

using filex.Objects;

namespace filex.Parsers.Base
{
    public abstract class BaseParser
    {
        public abstract string Name { get; }

        public abstract bool IsParseable(byte[] data, string fileName);

        public abstract ModelPredictionResponse RunModel(byte[] data, string fileName);

        public abstract void LoadModel();

        public abstract void TrainModel(string trainingPath);

        protected static IEnumerable<string> GetFiles(string path)
        {
            if (string.IsNullOrEmpty(path))
            {
                throw new ArgumentNullException($"Path was not set");
            }

            if (!Directory.Exists(path))
            {
                throw new DirectoryNotFoundException($"Path: {path} not found");
            }

            return Directory.GetFiles(path);
        }
    }
}