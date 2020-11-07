using System.IO;

using filex.Common;

namespace filex
{
    public class ModelRunner
    {
        public ModelRunner(string modelFile = Constants.DEFAULT_MODEL_FILENAME)
        {
            LoadModel(modelFile);
        }

        private static void LoadModel(string fileName)
        {
            if (!File.Exists(fileName))
            {
                throw new FileNotFoundException(fileName);
            }


        }
    }
}