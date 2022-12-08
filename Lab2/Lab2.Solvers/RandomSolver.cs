using Common;
using JetBrains.Annotations;

namespace Lab2.Solvers;

[PublicAPI]
public class RandomSolver : ISolver
{
    public Solution Solve(IReadOnlyList<Player> instance)
    {
        while (true)
        {
            var solution = new SolutionBuilder();

            foreach (var player in instance.OrderBy(_ => Random.Shared.NextDouble()))
            {
                if (solution.CanAddToSquad(player, false))
                {
                    solution.Squad.Add(player);

                    if (solution.FirstTeam.Count < Constants.FirstTeamCount && solution.CanAddToFirstTeam(player))
                    {
                        solution.FirstTeam.Add(player);
                    }
                }

                if (solution.Squad.Count == Constants.SquadCount)
                {
                    if (solution.FirstTeam.Count == Constants.FirstTeamCount)
                    {
                        return solution.ToSolution();
                    }

                    break;
                }
            }
        }
    }
}
