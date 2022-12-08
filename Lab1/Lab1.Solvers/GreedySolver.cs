using System.Diagnostics;
using Common;

namespace Lab1.Solvers;

public class GreedySolver : ISolver
{
    public Func<Player, double> PlayerValue { get; set; } = x => Math.Pow(x.Points, 2) / (double) x.Price;

    public Solution Solve(IReadOnlyList<Player> instance)
    {
        var solution = new SolutionBuilder();

        var players = instance.OrderByDescending(PlayerValue);
        foreach (var player in players)
        {
            if (solution.Squad.Count == Constants.SquadCount)
                break;

            if (solution.CanAddToSquad(player))
                solution.Squad.Add(player);
        }

        foreach (var player in solution.Squad)
        {
            if (solution.FirstTeam.Count == Constants.FirstTeamCount)
                break;

            if (solution.CanAddToFirstTeam(player))
                solution.FirstTeam.Add(player);
        }

        Debug.Assert(solution is {Squad.Count: Constants.SquadCount, FirstTeam.Count: Constants.FirstTeamCount});
        return solution.ToSolution();
    }
}
