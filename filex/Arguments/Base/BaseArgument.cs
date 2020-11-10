namespace filex.Arguments.Base
{
    public abstract class BaseArgument
    {
        public abstract string Argument { get; }

        public abstract string PropertyMap { get; }

        public abstract bool ValidArgument(string argValue);

        public abstract object GetValue(string argValue);
    }
}