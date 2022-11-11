using System.Diagnostics;
using Common;

namespace Lab1;

public class GraspSolver : ISolver
{
    private readonly IReadOnlyList<Player> _instance;
    private readonly Random _random = new();
    private int _lsCount;

    public GraspSolver(IReadOnlyList<Player> instance)
    {
        _instance = instance;
    }

    public Func<Player, double> PlayerValue { get; set; } = x => Math.Pow(x.Points, 2) / (double) x.Price;
    public double Alpha { get; set; } = 0.2;
    public int Iterations { get; set; } = 10;

    public Solution Solve()
    {
        var players = _instance
            .OrderByDescending(PlayerValue)
            .ToList();

        var solutions = new List<PartialSolution>();
        var maxLsCount = 0;

        for (int k = 0; k < Iterations; k++)
        {
            var solution = Construct(players);

            _lsCount = 0;
            solution = LocalSearch(solution, players);
            maxLsCount = Math.Max(_lsCount, maxLsCount);

            Debug.Assert(solution is {Squad.Count: 15, FirstTeam.Count: 11});

            solutions.Add(solution);
        }

        #if DEBUG
        Console.WriteLine($"{_instance.Count}, {maxLsCount}");
        #endif

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
                .Where(solution.CanAddToSquad)
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

        // Greedy reduce squad cost
        var substitutes = solution.Squad.Except(solution.FirstTeam).ToList();
        Debug.Assert(substitutes is {Count: 4});
        foreach (var substitute in substitutes)
        {
            solution.Squad.Remove(substitute);

            var newSubstitute = players
                .Except(solution.Squad)
                .Where(solution.CanAddToSquad)
                .MinBy(x => x.Price)!;

            Debug.Assert(newSubstitute is { });
            Debug.Assert(newSubstitute.Price <= substitute.Price);

            solution.Squad.Add(newSubstitute);
        }

        return solution;
    }

    private PartialSolution LocalSearch(PartialSolution previousSolution, IReadOnlyList<Player> players)
    {
        _lsCount++;

        var bestSolution = previousSolution;

        foreach (var player in previousSolution.FirstTeam)
        {
            var solution = previousSolution.Clone();
            solution.FirstTeam.Remove(player);
            solution.Squad.Remove(player);

            var newPlayer = players
                .Except(solution.Squad)
                .Where(solution.CanAddToSquad)
                .Where(solution.CanAddToFirstTeam)
                .FirstOrDefault(x => x.Points > player.Points);

            if (newPlayer is null)
                continue;

            solution.FirstTeam.Add(newPlayer);
            solution.Squad.Add(newPlayer);

            if (bestSolution.Value < solution.Value)
            {
                solution = LocalSearch(solution, players);
                bestSolution = solution;
            }
        }

        return bestSolution;
    }
}
