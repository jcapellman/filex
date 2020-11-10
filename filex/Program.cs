 using System;

 using filex.Objects;

 namespace filex
{
    class Program
    {
        static void Main(string[] args)
        {
            ArgumentResponseItem argResponse;
            bool validOption = false;

            try
            {
                (argResponse, validOption) = ArgumentParser.Parse(args);
            }
            catch (ArgumentNullException)
            {
                // TODO: Handle
                return;
            }
            catch (ArgumentException)
            {
                // TODO: Handle
                return;
            }

            if (!validOption)
            {
                return;
            }

            var modelRunner = new ModelRunner();

            var prediction = modelRunner.RunModel(argResponse);

            Console.WriteLine(prediction);
        }
    }
}