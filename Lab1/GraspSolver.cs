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

        var solutions = new PartialSolution[Iterations];

        for (int j = 0; j < solutions.Length; j++)
        {
            var solution = solutions[j] = new PartialSolution();

            // Construction phase
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

            // Finalize construction
            foreach (var player in solution.Squad)
            {
                if (solution.FirstTeam.Count == 11)
                    break;

                if (solution.CanAddToFirstTeam(player))
                    solution.FirstTeam.Add(player);
            }

            // Local search

            Debug.Assert(solution is {Squad.Count: 15, FirstTeam.Count: 11});
        }

        var bestSolution = solutions.MaxBy(x => x.Value)!;
        return new Solution(bestSolution.Squad, bestSolution.FirstTeam);
    }
}
