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
var greedySolver = new GreedySolver();
var randomSolver = new RandomSolver {Seed = null};

var solvers = new SolverInfo[]
{
    new("Random", randomSolver),
    new("Sa+Random", new SaSolver {InitialSolver = randomSolver}),
    // new("Tabu+Random", new TabuSolver {InitialSolver = randomSolver}),
    new("Greedy", greedySolver),
    new("Sa+Greedy", new SaSolver {InitialSolver = greedySolver}),
    // new("Tabu+Greedy", new TabuSolver {InitialSolver = greedySolver}),
    new("Grasp", new GraspSolver()),
};

foreach (var file in files)
{
    var instance = InstanceLoader.LoadFromFile(file);
    Console.WriteLine($"Instance: {file}");

    var sw = new Stopwatch();
    foreach (var solverInfo in solvers)
    {
        sw.Restart();
        var solution = solverInfo.Solver.Solve(instance);
        sw.Stop();

        Console.WriteLine($"[{sw.ElapsedMilliseconds,5}ms] {solverInfo.Name,15}: {solution.Value,4} ({solution.Cost,5})");
    }
}

file record SolverInfo(string Name, ISolver Solver);
