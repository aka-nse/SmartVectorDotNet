// See https://aka.ms/new-console-template for more information

IExample[] examples = [
    new GeneralizeMathFunc(),
    new VoctorizeSimply(),
    new CalculateVectorized(),
    new CalculateVectorizedInterceptor(),
];

foreach(var example in examples)
{
    Console.WriteLine($"\e[32m--- {example.GetType().Name} ---\e[0m");
    example.Run(Console.Out);
    Console.WriteLine();
}
