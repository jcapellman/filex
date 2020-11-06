 using System;
 using System.IO;
 using System.Linq;

 namespace filex
{
    class Program
    {
        private static string ParseArgument(string[] args)
        {
            if (args == null)
            {
                return null;
            }

            if (!args.Any())
            {
                return null;
            }

            var file = args[0];

            if (!File.Exists(file))
            {
                return null;
            }

            return file;
        }

        static void Main(string[] args)
        {
            var fileName = ParseArgument(args);

            // Feature Extractor

            // Model Run

            // Model Output
        }
    }
}
