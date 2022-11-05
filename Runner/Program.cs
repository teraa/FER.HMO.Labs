using Common;
using Lab1;

int i = 0;
foreach (var func in new Func<Player, double>[]
         {
             x => x.Points,
             x => (double) (x.Points / x.Price),
             x => Math.Pow(x.Points, 2) / (double) x.Price,
             x => x.Points / Math.Log10((double) x.Price),
             x => Math.Pow(x.Points, 2.25) / (double) x.Price,
         })
{
    var solutions = new List<Solution>();

    foreach (var fileName in new[]
             {
                 "2022_instance1.csv", "2022_instance2.csv", "2022_instance3.csv",
                 "2021_instance1.csv"
             })
    {
        var instance = InstanceLoader.LoadFromFile(fileName);
        var solver = new GreedySolver(instance)
        {
            PlayerValue = func,
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
