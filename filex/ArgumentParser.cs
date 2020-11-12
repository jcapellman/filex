using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

using filex.Arguments.Base;
using filex.Objects;

namespace filex
{
    public class ArgumentParser
    {
        private static IEnumerable<BaseArgument> SupportedArguments =>
            Assembly.GetExecutingAssembly().GetTypes()
                .Where(a => a.BaseType == typeof(BaseArgument) && !a.IsAbstract)
                .Select(a => (BaseArgument)Activator.CreateInstance(a)).ToList();

        public static List<string> BuildHelpContext() => 
            SupportedArguments.Select(argument => $"-{argument.Argument} (Default: {argument.DefaultValue}) - {argument.UsageText}").ToList();

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
            
           

            var response = new ArgumentResponseItem(SupportedArguments);

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

                response.UpdateProperty(argument.PropertyMap, argument.GetValue(argumentValue));
            }

            return (response, response.IsValid());
        }
    }
}