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
var graspSolver = new GraspSolver();
var randomSolver = new RandomSolver {Seed = null};
var tabuRandomSolver = new TabuSolver {InitialSolver = randomSolver};
var tabuGreedySolver = new TabuSolver {InitialSolver = greedySolver};
var saRandomSolver = new SaSolver {InitialSolver = randomSolver};
var saGreedySolver = new SaSolver {InitialSolver = greedySolver};
var instances = files.Select(InstanceLoader.LoadFromFile);

foreach (var instance in instances.Zip(files))
{
    Console.WriteLine($"Instance: {instance.Second}");

    var greedySolution = greedySolver.Solve(instance.First);
    Console.WriteLine($"Greedy: {greedySolution.Value} ({greedySolution.Cost})");

    var tabuGreedySolution = tabuGreedySolver.Solve(instance.First);
    Console.WriteLine($"Tabu+Greedy: {tabuGreedySolution.Value} ({tabuGreedySolution.Cost})");

    var tabuRandomSolution = tabuRandomSolver.Solve(instance.First);
    Console.WriteLine($"Tabu+Random: {tabuRandomSolution.Value} ({tabuRandomSolution.Cost})");

    var graspSolution = graspSolver.Solve(instance.First);
    Console.WriteLine($"Grasp: {graspSolution.Value} ({graspSolution.Cost})");

    var saGreedySolution = saGreedySolver.Solve(instance.First);
    Console.WriteLine($"Sa+Greedy: {saGreedySolution.Value} ({saGreedySolution.Cost})");

    var saRandomSolution = saRandomSolver.Solve(instance.First);
    Console.WriteLine($"Sa+Random: {saRandomSolution.Value} ({saRandomSolution.Cost})");
}
