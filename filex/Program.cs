 using System;

 using filex.Objects;

 namespace filex
{
    class Program
    {
        static void DisplayArgumentHelp()
        {
            // TODO: Iterate through available options
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