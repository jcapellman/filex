 using System;

 namespace filex
{
    class Program
    {
        static void Main(string[] args)
        {
            var (argResponse, validOption) = ArgumentParser.Parse(args);

            if (!validOption)
            {
                return;
            }

            var modelRunner = new ModelRunner();

            var prediction = modelRunner.RunModel(argResponse.FileNameForClassification);

            Console.WriteLine(prediction);
        }
    }
}