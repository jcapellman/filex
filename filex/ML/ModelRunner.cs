using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

using filex.Objects;
using filex.Parsers.Base;

namespace filex
{
    public class ModelRunner
    {
        private readonly List<BaseParser> _parsers;

        public ModelRunner()
        {
            _parsers = GetType().Assembly.GetTypes().Where(a => a.BaseType == typeof(BaseParser) && !a.IsAbstract)
                .Select(b => (BaseParser) Activator.CreateInstance(b)).ToList();
        }

        public ModelPredictionResponse RunModel(ArgumentResponseItem responseItem)
        {
            var response = new ModelPredictionResponse();

            if (!File.Exists(responseItem.FileNameForClassification))
            {
                throw new FileNotFoundException($"Could not find file {responseItem.FileNameForClassification}");
            }

            var fileBytes = File.ReadAllBytes(responseItem.FileNameForClassification);

            var matchedParser = _parsers.FirstOrDefault(parser => parser.IsParseable(fileBytes, responseItem.FileNameForClassification));

            if (matchedParser == null)
            {
                throw new ArgumentOutOfRangeException($"No parser matched for {responseItem.FileNameForClassification}");
            }

            Console.WriteLine($"Using {matchedParser.Name} Parser to parse {responseItem.FileNameForClassification}");

            matchedParser.LoadModel();
            
            return matchedParser.RunModel(fileBytes, responseItem.FileNameForClassification);
        }
    }
}