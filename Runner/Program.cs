using Common;
using Lab1;

int i = 0;
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

    var files = Directory.GetFiles(dir, "*.csv");

    if (files.Length == 0)
    {
        Console.Error.WriteLine("No instance files found.");
        return;
    }

    foreach (var file in files)
    {
        var fileName = Path.GetFileName(file);

        var instance = InstanceLoader.LoadFromFile(file);
        var solver = new GraspSolver(instance)
        // var solver = new GreedySolver(instance)
        {
            PlayerValue = func,
            Alpha = 0.2,
        };
        var solution = solver.Solve();
        solutions.Add(solution);
        var message =
            $"Instance: {fileName}\n" +
            $"Squad: {string.Join(',', solution.Squad.Select(x => x.Id))}\n" +
            $"First team: {string.Join(',', solution.FirstTeam.Select(x => x.Id))}\n" +
            $"Squad cost: {solution.Squad.Sum(x => x.Price)}\n" +
            $"First team points: {solution.FirstTeam.Sum(x => x.Points)}\n";
        // Console.WriteLine(message);
    }

    var averagePoints = solutions.Select(static x => x.FirstTeam.Sum(static x => x.Points)).Average();
    Console.WriteLine($"Func: {++i}, Average: {averagePoints}");
}
