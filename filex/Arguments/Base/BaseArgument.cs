namespace filex.Arguments.Base
{
    public abstract class BaseArgument
    {
        public abstract string Argument { get; }

        public abstract string UsageText { get; }

        public abstract object DefaultValue { get; }

        public abstract string PropertyMap { get; }

        public abstract bool ValidArgument(string argValue);

        public abstract object GetValue(string argValue);

        public abstract bool KeyOnly { get; }

        public abstract bool SupportsDefaultValue { get; }
    }
}