using System.Diagnostics;
using Common;

namespace Lab1.Solvers;

public class GraspSolver : ISolver
{
    public Func<Player, double> PlayerValue { get; set; } = x => Math.Pow(x.Points, 2) / (double) x.Price;
    public double Alpha { get; set; } = 0.2;
    public int Iterations { get; set; } = 10;

    public Solution Solve(Instance instance)
    {
        var players = instance
            .Players
            .OrderByDescending(PlayerValue)
            .ToList();

        var solutions = new List<SolutionBuilder>();
        int maxLsIterations = 0;

        for (int k = 0; k < Iterations; k++)
        {
            var solution = Construct(players);

            int lsIterations = 0;
            solution = LocalSearch(solution, players, ref lsIterations);
            maxLsIterations = Math.Max(lsIterations, maxLsIterations);

            Debug.Assert(solution is {Squad.Count: Constants.SquadCount, FirstTeam.Count: Constants.FirstTeamCount});

            solutions.Add(solution);
        }

        // #if DEBUG
        // Console.WriteLine($"{_instance.Count}, {maxLsCount}");
        // #endif

        var bestSolution = solutions.MaxBy(x => x.Value)!;
        return bestSolution.ToSolution();
    }

    private SolutionBuilder Construct(IReadOnlyList<Player> players)
    {
        var solution = new SolutionBuilder();

        for (int i = 0; i < Constants.SquadCount; i++)
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

            var player = rcl[Random.Shared.Next(rcl.Count)];
            solution.Squad.Add(player);
        }

        // Greedy pick the first team
        foreach (var player in solution.Squad)
        {
            if (solution.FirstTeam.Count == Constants.FirstTeamCount)
                break;

            if (solution.CanAddToFirstTeam(player))
                solution.FirstTeam.Add(player);
        }

        // Greedy reduce squad cost
        var substitutes = solution.Squad.Except(solution.FirstTeam).ToList();
        Debug.Assert(substitutes is {Count: Constants.SubstitutesCount});
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

    private SolutionBuilder LocalSearch(SolutionBuilder previous, IReadOnlyList<Player> players, ref int iterations)
    {
        iterations++;

        var incumbent = previous;

        foreach (var player in previous.FirstTeam)
        {
            var current = previous.Clone();
            current.FirstTeam.Remove(player);
            current.Squad.Remove(player);

            var newPlayer = players
                .Except(current.Squad)
                .Where(current.CanAddToSquad)
                .Where(current.CanAddToFirstTeam)
                .FirstOrDefault(x => x.Points > player.Points);

            if (newPlayer is null)
                continue;

            current.FirstTeam.Add(newPlayer);
            current.Squad.Add(newPlayer);

            if (incumbent.Value < current.Value)
            {
                current = LocalSearch(current, players, ref iterations);
                incumbent = current;
            }
        }

        return incumbent;
    }
}
