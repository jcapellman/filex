using System;
using System.Linq;
using System.Reflection;

using filex.Arguments.Base;
using filex.Objects;

namespace filex
{
    public class ArgumentParser
    {
        /// <summary>
        /// Parses the command line argument
        /// </summary>
        /// <param name="args">Contains the file name to scan</param>
        /// <returns>Filename of the file to scan</returns>
        /// <exception cref="System.ArgumentNullException">Thrown when args is null</exception>
        /// <exception cref="System.ArgumentException">Thrown when args is empty</exception>
        public static (ArgumentResponseItem ArgResponse, bool ValidOption) Parse(string[] args)
        {
            if (args == null)
            {
                throw new ArgumentNullException(nameof(args));
            }

            if (!args.Any())
            {
                throw new ArgumentException("Args was empty");
            }

            if (args.Length % 2 != 0)
            {
                throw new ArgumentException("Arguments come in pairs only");
            }

            var response = new ArgumentResponseItem();

            var validArguments = Assembly.GetExecutingAssembly().GetTypes()
                .Where(a => a.BaseType == typeof(BaseArgument) && !a.IsAbstract)
                .Select(a => (BaseArgument) Activator.CreateInstance(a)).ToList();

            for (var x = 0; x < args.Length; x += 2)
            {
                var argumentKey = args[x].ToLower();
                var argumentValue = args[x + 1];

                var argument = validArguments.FirstOrDefault(a => a.Argument == argumentKey);

                if (argument == null)
                {
                    Console.WriteLine($"Invalid option: {argumentKey}");

                    continue;
                }

                if (!argument.ValidArgument(argumentValue))
                {
                    Console.WriteLine($"{argumentValue} is an invalid value for {argumentKey}");

                    continue;
                }

                var property = response.GetType().GetProperty(argument.PropertyMap);

                if (property == null)
                {
                    Console.WriteLine($"Could not map {argument.PropertyMap} to the object");

                    continue;
                }

                property.SetValue(response, argument.GetValue(argumentValue), null);
            }

            return (response, response.IsValid());
        }
    }
}