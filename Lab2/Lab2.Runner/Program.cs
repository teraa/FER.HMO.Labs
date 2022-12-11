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
    new("Greedy", greedySolver),
    new("Tabu+Random", new TabuSolver {InitialSolver = randomSolver}),
    new("Tabu+Greedy", new TabuSolver {InitialSolver = greedySolver}),
    new("Grasp", new GraspSolver()),
    new("Sa+Random", new SaSolver {InitialSolver = randomSolver}),
    new("Sa+Greedy", new SaSolver {InitialSolver = greedySolver}),
};

foreach (var file in files)
{
    var instance = InstanceLoader.LoadFromFile(file);
    Console.WriteLine($"Instance: {file}");

    foreach (var solverInfo in solvers)
    {
        var solution = solverInfo.Solver.Solve(instance);
        Console.WriteLine($"{solverInfo.Name}: {solution.Value} ({solution.Cost})");
    }
}

file record SolverInfo(string Name, ISolver Solver);
