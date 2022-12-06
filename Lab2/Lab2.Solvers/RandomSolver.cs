using Common;
using JetBrains.Annotations;

namespace Lab2.Solvers;

[PublicAPI]
public class RandomSolver : ISolver
{
    private readonly IReadOnlyList<Player> _instance;

    public RandomSolver(IReadOnlyList<Player> instance)
    {
        _instance = instance;
    }

    public Solution Solve()
    {
        while (true)
        {
            var solution = new SolutionBuilder();

            foreach (var player in _instance.OrderBy(_ => Random.Shared.NextDouble()))
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
