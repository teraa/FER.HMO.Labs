using System.Diagnostics;
using Common;
using JetBrains.Annotations;

namespace Lab2.Solvers;

[PublicAPI]
public class TabuSolver : ISolver
{
    public ISolver InitialSolver { get; set; } = new RandomSolver();
    public int Tenure { get; set; } = 10;

    public Solution Solve(Instance instance)
    {
        var incumbent = InitialSolver.Solve(instance);
        return TabuSearch(incumbent, instance, out var iterations);
    }

    private Solution TabuSearch(Solution initial, Instance instance, out int iteration)
    {
        const int terminateAfter = 10;

        int improvedAt = iteration = 0;
        var previous = new SolutionBuilder(initial);
        var incumbent = previous;
        var tabu = new TabuList<Player>(Tenure);

        for (iteration = 0; iteration - improvedAt < terminateAfter; iteration++)
        {
            SolutionBuilder? bestInIteration = null;

            foreach (var removed in previous.FirstTeam)
            {
                var current = previous.Clone();
                current.FirstTeam.Remove(removed);
                current.Squad.Remove(removed);

                var bestEligible = instance.Players
                    .Except(previous.Squad)
                    .Except(tabu)
                    .Where(current.CanAddToSquad)
                    // .Where(current.CanAddToFirstTeam) // Unnecessary if we are modifying only ONE player
                    .OrderByDescending(x => x.Points)
                    .FirstOrDefault(x => x.Points >= removed.Points);

                if (bestEligible is null)
                    continue;

                current.FirstTeam.Add(bestEligible);
                current.Squad.Add(bestEligible);

                if (bestInIteration is null ||
                    bestInIteration.Value < current.Value)
                {
                    bestInIteration = current;
                }
            }

            if (bestInIteration is { })
            {
                var diff = bestInIteration.Squad
                    .Except(previous.Squad)
                    .Single();
                tabu.TryAdd(diff);

                // always better than `previous` but not necessarily incumbent
                previous = bestInIteration;

                if (incumbent.Value < bestInIteration.Value)
                {
                    incumbent = bestInIteration;
                    improvedAt = iteration;
                }
            }
        }

        Debug.WriteLine($"[TABU] Completed in {iteration} iterations");
        return incumbent.ToSolution();
    }
}
