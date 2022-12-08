using Common;
using Lab1.Solvers;

// int i = 0;
foreach (var func in new Func<Player, double>[]
         {
             // x => x.Points,
             // x => (double) (x.Points / x.Price),
             x => Math.Pow(x.Points, 2) / (double) x.Price,
             // x => Math.Pow(x.Points, 2.25) / (double) x.Price,
             // x => x.Points / Math.Log10((double) x.Price + 1),
         })
{
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

    var solvers = new Func<IReadOnlyList<Player>, ISolver>[]
    {
        x => new GreedySolver()
        {
            PlayerValue = func,
        },
        x => new GraspSolver()
        {
            PlayerValue = func,
            Alpha = 0.2,
        }
    };

    foreach (var solverFunc in solvers)
    {
        foreach (var (name, instance) in instances)
        {
            var solver = solverFunc(instance);
            var solution = solver.Solve(instance);
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
            Console.WriteLine(message);
        }
    }

    // var averagePoints = solutions.Select(static x => x.FirstTeam.Sum(static x => x.Points)).Average();
    // Console.WriteLine($"Func: {++i}, Average: {averagePoints}");
}
