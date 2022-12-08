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

                    if (solution.FirstTeam.Count < 11 && solution.CanAddToFirstTeam(player))
                    {
                        solution.FirstTeam.Add(player);
                    }
                }

                if (solution.Squad.Count == 15)
                {
                    if (solution.FirstTeam.Count == 11)
                    {
                        return solution.ToSolution();
                    }

                    break;
                }
            }
        }
    }
}
