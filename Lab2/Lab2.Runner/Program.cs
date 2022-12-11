using Common;
using Lab1.Solvers;
using Lab2.Solvers;

var files = new[]
{
    "../../Lab1/instances/2022_instance1.csv",
    "../../Lab1/instances/2022_instance2.csv",
    "../../Lab1/instances/2022_instance3.csv",
    "../instances/Lab2_inst1.csv",
    "../instances/Lab2_inst2.csv",
};
var tabuRandomSolver = new TabuSolver() {InitialSolver = new RandomSolver() {Seed = 100}};
var tabuGreedySolver = new TabuSolver() {InitialSolver = new GreedySolver()};
var greedySolver = new GreedySolver();
var graspSolver = new GraspSolver();
var instances = files.Select(x => InstanceLoader.LoadFromFile(x));

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
}


//
// foreach (var position in Enum.GetValues<Position>())
// {
//     var selection = Formation.ValidFormations.Select(x => x[position]).ToArray();
//     var min = selection.Min();
//     var max = selection.Max();
//
//     Console.WriteLine($"{position}: {min},{max}");
// }
