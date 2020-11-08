 using System;

 namespace filex
{
    class Program
    {
        static void Main(string[] args)
        {
            var fileName = ArgumentParser.Parse(args);

            var modelRunner = new ModelRunner();

            var prediction = modelRunner.RunModel(fileName);

            Console.WriteLine(prediction);
        }
    }
}