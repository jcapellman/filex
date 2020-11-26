using filex.Objects;

namespace filex.Parsers.Base
{
    public abstract class BaseParser
    {
        public abstract string Name { get; }

        public abstract bool IsParseable(byte[] data, string fileName);

        public abstract ModelPredictionResponse RunModel(byte[] data, string fileName);
    }
}