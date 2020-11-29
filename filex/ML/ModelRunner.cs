using System;
using System.IO;
using System.Linq;

using filex.ML.Base;
using filex.Objects;

namespace filex
{
    public class ModelRunner : BaseML
    {
        public ModelPredictionResponse RunModel(ArgumentResponseItem responseItem)
        {
            if (!File.Exists(responseItem.FileNameForClassification))
            {
                throw new FileNotFoundException($"Could not find file {responseItem.FileNameForClassification}");
            }

            var fileBytes = File.ReadAllBytes(responseItem.FileNameForClassification);

            var matchedParser = Parsers.FirstOrDefault(parser => parser.IsParseable(fileBytes, responseItem.FileNameForClassification));

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