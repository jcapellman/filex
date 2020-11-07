 using System;
 using System.IO;
 using System.Linq;

 namespace filex
{
    class Program
    {
        /// <summary>
        /// Parses the command line argument
        /// </summary>
        /// <param name="args">Contains the file name to scan</param>
        /// <returns>Filename of the file to scan</returns>
        /// <exception cref="System.ArgumentNullException">Thrown when args is null</exception>
        /// <exception cref="System.IO.FileNotFoundException">Throw when the file is not found</exception>
        /// <exception cref="System.ArgumentException">Thrown when args is empty</exception>
        private static string ParseArgument(string[] args)
        {
            if (args == null)
            {
                throw new ArgumentNullException(nameof(args));
            }

            if (!args.Any())
            {
                throw new ArgumentException("Args was empty");
            }

            var file = args[0];

            if (!File.Exists(file))
            {
                throw new FileNotFoundException($"File: {file} does not exist");
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
