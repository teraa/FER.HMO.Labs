using Common;
using JetBrains.Annotations;

namespace Lab2.Solvers;

[PublicAPI]
public class RandomSolver : ISolver
{
    public Solution Solve(Instance instance)
    {
        while (true)
        {
            var solution = new SolutionBuilder();

            foreach (var player in instance.Players.OrderBy(_ => Random.Shared.NextDouble()))
            {
                if (solution.CanAddToSquad(player, false))
                {
                    solution.Squad.Add(player);

                    if (solution.FirstTeam.Count < Constants.FirstTeamCount && solution.CanAddToFirstTeam(player))
                    {
                        solution.FirstTeam.Add(player);
                    }
                }

                // Incomplete solution
                if (solution.Squad.Count != Constants.SquadCount)
                    continue;

                // Invalid solution
                if (solution.FirstTeam.Count != Constants.FirstTeamCount)
                    break;

                return solution.ToSolution();
            }
        }
    }
}
