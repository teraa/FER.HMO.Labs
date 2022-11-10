using System.Diagnostics;
using Common;

namespace Lab1;

public class GraspSolver : ISolver
{
    private readonly IReadOnlyList<Player> _instance;
    private readonly Random _random = new();

    public GraspSolver(IReadOnlyList<Player> instance)
    {
        _instance = instance;
    }

    public Func<Player, double> PlayerValue { get; set; } = x => Math.Pow(x.Points, 2) / (double) x.Price;
    public double Alpha { get; set; } = 0.5;
    public int Iterations { get; set; } = 10;

    public Solution Solve()
    {
        var players = _instance
            .OrderByDescending(PlayerValue)
            .ToList();

        var solutions = new List<PartialSolution>();

        for (int k = 0; k < Iterations; k++)
        {
            var solution = Construct(players);

            LocalSearch(solution);

            Debug.Assert(solution is {Squad.Count: 15, FirstTeam.Count: 11});

            solutions.Add(solution);
        }

        var bestSolution = solutions.MaxBy(x => x.Value)!;
        return bestSolution.ToSolution();
    }

    private PartialSolution Construct(IReadOnlyList<Player> players)
    {
        var solution = new PartialSolution();

        for (int i = 0; i < 15; i++)
        {
            // Use Skip() instead of Except() to emulate GreedySolver
            // which will produce mostly same results except in cases where there are multiple players with same value
            // one of which will be chosen randomly
            var eligiblePlayers = players
                .Except(solution.Squad)
                // .Skip(i == 0 ? 0 : players.IndexOf(solution.Squad.Last()) + 1)
                .Where(x => solution.CanAddToSquad(x))
                .ToList();

            double max = PlayerValue(eligiblePlayers[0]);
            double min = PlayerValue(eligiblePlayers[^1]);
            double threshold = max - Alpha * (max - min);

            var rcl = eligiblePlayers
                .Where(x => PlayerValue(x) >= threshold)
                .ToList();

            Debug.Assert(rcl.Count > 0);

            var player = rcl[_random.Next(rcl.Count)];
            solution.Squad.Add(player);
        }

        // Greedy pick the first team
        foreach (var player in solution.Squad)
        {
            if (solution.FirstTeam.Count == 11)
                break;

            if (solution.CanAddToFirstTeam(player))
                solution.FirstTeam.Add(player);
        }

        return solution;
    }

    private void LocalSearch(PartialSolution solution)
    {
        // Local search
    }
}
