 using System;

 using filex.Objects;

 namespace filex
{
    class Program
    {
        private static void DisplayArgumentHelp()
        {
            var arguments = ArgumentParser.BuildHelpContext();

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