using System.Diagnostics;
using Common;
using Lab1.Solvers;
using Lab2.Solvers;

var files = new[]
{
    // "../../Lab1/instances/2022_instance1.csv",
    // "../../Lab1/instances/2022_instance2.csv",
    // "../../Lab1/instances/2022_instance3.csv",
    "../instances/Lab2_inst1.csv",
    "../instances/Lab2_inst2.csv",
};

var solver = new SaSolver()
{
    // InitialSolver = new RandomSolver
    // {
    //     Seed = 123,
    // },
    InitialSolver = new GreedySolver(),
    Decrement = SaFunctions.LinearDecrement,
    Seed = 456,
};

foreach (var file in files)
{
    var instance = InstanceLoader.LoadFromFile(file);
    Console.WriteLine($"Instance: {file}");

    for (int t = 0; t < 200; t += 1)
    {
        solver.InitialTemperature = t;
        var solution = solver.Solve(instance);
        Console.WriteLine($"{solution.Value}");
    }
}
