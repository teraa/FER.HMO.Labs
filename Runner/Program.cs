using Common;
using Lab1;

foreach (var fileName in new[] {"2022_instance1.csv", "2022_instance2.csv", "2022_instance3.csv"})
{
    var instance = InstanceLoader.LoadFromFile(fileName);
    var solver = new GreedySolver(instance);
    var solution = solver.Solve();
    var message =
        $"Instance: {fileName}\n" +
        $"Squad: {string.Join(',', solution.Squad.Select(x => x.Id))}\n" +
        $"First team: {string.Join(',', solution.FirstTeam.Select(x => x.Id))}\n" +
        $"Squad cost: {solution.Squad.Sum(x => x.Price)}\n" +
        $"First team points: {solution.FirstTeam.Sum(x => x.Points)}\n";
    Console.WriteLine(message);
}
