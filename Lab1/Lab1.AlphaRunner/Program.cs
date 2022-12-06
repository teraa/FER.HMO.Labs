using Common;
using Lab1.Solvers;

int i = 0;
var solutions = new List<Solution>();

string dir = args is {Length: 1}
    ? args[0]
    : Environment.CurrentDirectory;

var files = Directory.GetFiles(dir, "2022*.csv");

if (files.Length == 0)
{
    Console.Error.WriteLine("No instance files found.");
    return;
}

var instances = new List<(string name, IReadOnlyList<Player> instance)>();

foreach (var file in files)
{
    var name = Path.GetFileName(file);
    var instance = InstanceLoader.LoadFromFile(file);
    instances.Add((name, instance));
}

foreach (var (name, instance) in instances)
{
    for (double a = 0; a < 1; a += 0.1)
    {
        var solver = new GraspSolver(instance)
        {
            Alpha = a,
        };
        var solution = solver.Solve();
        solutions.Add(solution);

        var squad = solution.Squad.Select(x => x.Id);
        var firstTeam = solution.FirstTeam.Select(x => x.Id);
        var substitutions = solution.Squad.Except(solution.FirstTeam).Select(x => x.Id);
        var points = solution.FirstTeam.Sum(x => x.Points);
        var cost = solution.Squad.Sum(x => x.Price);

        var message =
            $"{solver.GetType().Name}, {name} " +
            // $"{fileName}\n" +
            // $"Squad: {string.Join(',', squad)}\n" +
            $"{string.Join(',', firstTeam)} + {string.Join(',', substitutions)} " +
            $"| p = {points}, c = {cost}";
        // Console.WriteLine(message);
        Console.WriteLine($"{a:0.#}, {points}");
    }
}

// var averagePoints = solutions.Select(static x => x.FirstTeam.Sum(static x => x.Points)).Average();
// Console.WriteLine($"Func: {++i}, Average: {averagePoints}");
