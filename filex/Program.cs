 using System;

 using filex.Common;
 using filex.Objects;

 namespace filex
{
    class Program
    {
        private static void DisplayArgumentHelp()
        {
            var arguments = ArgumentParser.BuildHelpContext();

            Console.WriteLine($"{Constants.APP_NAME}{System.Environment.NewLine}Usage Help");

            foreach (var argumentText in arguments)
            {
                Console.WriteLine(argumentText);
            }
        }

        static void Main(string[] args)
        {
            ArgumentResponseItem argResponse;
            bool validOption;

            try
            {
                (argResponse, validOption) = ArgumentParser.Parse(args);
            }
            catch (ArgumentNullException)
            {
                DisplayArgumentHelp();

                return;
            }
            catch (ArgumentException)
            {
                DisplayArgumentHelp();

                return;
            }

            if (!validOption)
            {
                DisplayArgumentHelp();

                return;
            }

            var modelRunner = new ModelRunner();

            var prediction = modelRunner.RunModel(argResponse);

            Console.WriteLine(prediction);
        }
    }
}