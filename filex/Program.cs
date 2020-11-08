 using System;

 namespace filex
{
    class Program
    {
        static void Main(string[] args)
        {
            var argumentResponse = ArgumentParser.Parse(args);

            var modelRunner = new ModelRunner();

            var prediction = modelRunner.RunModel(argumentResponse.FileNameForClassification);

            Console.WriteLine(prediction);
        }
    }
}