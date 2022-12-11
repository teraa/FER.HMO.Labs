using System.Diagnostics;
using Common;
using JetBrains.Annotations;

namespace Lab2.Solvers;

[PublicAPI]
public class TabuSolver : ISolver
{
    public ISolver InitialSolver { get; set; } = new RandomSolver();
    public int Tenure { get; set; } = 5000;
    public int TerminateAfter { get; set; } = 100;

    public Solution Solve(Instance instance)
    {
        var initial = InitialSolver.Solve(instance);
        int improvedAt = 0;
        var previous = new SolutionBuilder(initial);
        var incumbent = previous;
        var tabu = new TabuList<Player>(Tenure);

        int i;
        for (i = 0; i - improvedAt < TerminateAfter; i++)
        {
            SolutionBuilder? bestInIteration = null;

            int n = 0;
            foreach (var removedSubstitute in previous.Substitutes)
            {
                foreach (var removedFirstTeam in previous.FirstTeam)
                {
                    var current = previous.Clone();
                    current.Squad.Remove(removedSubstitute);
                    current.FirstTeam.Remove(removedFirstTeam);
                    current.Squad.Remove(removedFirstTeam);

                    var aspiration = incumbent.Value - current.Value;

                    var bestEligible = instance.Players
                        .Where(current.CanAddToSquad)
                        .Where(current.CanAddToFirstTeam)
                        .Except(previous.Squad)
                        .OrderByDescending(x => x.Points)
                        .FirstOrDefault(x => !tabu.Contains(x) || x.Points > aspiration);

                    if (bestEligible is null)
                        continue;

                    if (tabu.Contains(bestEligible))
                        Debug.WriteLine("[TABU] picked tabu");

                    if (bestEligible.Points > aspiration)
                        Debug.WriteLine($"[TABU] aspiration: {bestEligible.Points - aspiration}");

                    current.FirstTeam.Add(bestEligible);
                    current.Squad.Add(bestEligible);

                    var cheapestSubstitute = instance.Players
                        .Where(current.CanAddToSquad)
                        .Except(previous.Squad)
                        .Except(tabu)
                        .MinBy(x => x.Price);

                    if (cheapestSubstitute is null)
                        continue;

                    current.Squad.Add(cheapestSubstitute);
                    n++;

                    if (bestInIteration is null ||
                        bestInIteration.Value < current.Value)
                    {
                        bestInIteration = current;
                    }
                }
            }

            Debug.WriteLine($"[TABU] {n} neighbors considered");

            if (bestInIteration is null)
                continue;

            var diff = bestInIteration.Squad
                .Except(previous.Squad)
                .ToList();
            Debug.Assert(diff.Count == 2);

            foreach (var player in diff)
                tabu.TryAdd(player);

            // always better than `previous` but not necessarily incumbent
            previous = bestInIteration;

            if (incumbent.Value < bestInIteration.Value)
            {
                incumbent = bestInIteration;
                improvedAt = i;
            }
        }

        Debug.WriteLine($"[TABU] Completed in {i} iterations");
        return incumbent.ToSolution();
    }
}
