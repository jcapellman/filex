using System;
using System.IO;
using System.Linq;

using filex.Enums;
using filex.Objects;

namespace filex
{
    public class ArgumentParser
    {
        private static string VerifyInputFile(string file)
        {
            if (!File.Exists(file))
            {
                throw new FileNotFoundException($"File: {file} does not exist");
            }

            return file;
        }

        /// <summary>
        /// Parses the command line argument
        /// </summary>
        /// <param name="args">Contains the file name to scan</param>
        /// <returns>Filename of the file to scan</returns>
        /// <exception cref="System.ArgumentNullException">Thrown when args is null</exception>
        /// <exception cref="System.IO.FileNotFoundException">Throw when the file is not found</exception>
        /// <exception cref="System.ArgumentException">Thrown when args is empty</exception>
        public static ArgumentResponseItem Parse(string[] args)
        {
            if (args == null)
            {
                throw new ArgumentNullException(nameof(args));
            }

            if (!args.Any())
            {
                throw new ArgumentException("Args was empty");
            }

            if (args.Length == 1)
            {
                return new ArgumentResponseItem
                {
                    FileNameForClassification = VerifyInputFile(args[0])
                };
            }

            if (args.Length % 2 != 0)
            {
                throw new ArgumentException("Arguments come in pairs");
            }

            var response = new ArgumentResponseItem();

            for (var x = 0; x < args.Length; x += 2)
            {
                args[x] = args[x].ToLower();

                switch (args[x])
                {
                    case "file":
                        response.FileNameForClassification = VerifyInputFile(args[x+1]);
                        break;
                    case "mode":
                        if (!Enum.TryParse(args[x + 1], true, out OperationMode mode))
                        {
                            Console.WriteLine($"Invalid value for mode: {args[x+1]}");
                            continue;
                        }

                        response.Mode = mode;

                        break;
                    default:
                        Console.WriteLine($"Invalid option: {args[x]}");
                        break;
                }
            }

            return response;
        }
    }
}