using Common;
using JetBrains.Annotations;
using Lab1.Solvers;

namespace Lab2.Solvers;

[PublicAPI]
public class TabuSolver : ISolver
{
    public ISolver InitialSolver { get; set; } = new GreedySolver();

    public Solution Solve(Instance instance)
    {
        var incumbent = InitialSolver.Solve(instance);
        return LocalSearch(incumbent, instance, out var iterations);
    }

    private Solution LocalSearch(Solution incumbent, Instance instance, out int iterations)
    {
        iterations = 0;
        return LocalSearchInternal(new SolutionBuilder(incumbent), instance, ref iterations).ToSolution();
    }

    private SolutionBuilder LocalSearchInternal(SolutionBuilder previous, Instance instance, ref int iterations)
    {
        iterations++;

        var incumbent = previous;

        foreach (var player in previous.FirstTeam)
        {
            var current = previous.Clone();
            current.FirstTeam.Remove(player);
            current.Squad.Remove(player);

            var newPlayer = instance.Players
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
                current = LocalSearchInternal(current, instance, ref iterations);
                incumbent = current;
            }
        }

        return incumbent;
    }
}
