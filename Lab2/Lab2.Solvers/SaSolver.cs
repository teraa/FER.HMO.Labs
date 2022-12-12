using System.Diagnostics;
using Common;
using JetBrains.Annotations;
using Lab1.Solvers;

namespace Lab2.Solvers;

[PublicAPI]
public class SaSolver : ISolver
{
    public ISolver InitialSolver { get; set; } = new GreedySolver();
    public double InitialTemperature { get; set; } = 4;
    public double FinalTemperature { get; set; } = 0.01;
    public DecrementFunction Decrement { get; set; } = SaFunctions.VerySlowDecrement;
    public ProbabilityFunction Probability { get; set; } = SaFunctions.DefaultProbability;
    public int Seed { get; set; }

    public Solution Solve(Instance instance)
    {
        var rnd = new Random(Seed);

        // s0 = initial
        // s best = incumbent
        // s = previous
        // s' = current

        var initial = InitialSolver.Solve(instance);
        var incumbent = new SolutionBuilder(initial);
        var previous = incumbent;
        var t = InitialTemperature;
        int i = 0;

        while (t >= FinalTemperature)
        {
            i++;

            // random neighbor
            var removed = previous.Squad.OrderBy(_ => rnd.Next()).First();
            var current = previous.Clone();
            current.Squad.Remove(removed);
            bool isFirstTeam = current.FirstTeam.Remove(removed);

            var added = instance.Players
                .OrderBy(_ => rnd.Next())
                .Except(previous.Squad)
                .Where(current.CanAddToSquad)
                .FirstOrDefault();

            if (added is null)
                continue;

            current.Squad.Add(added);
            if (isFirstTeam)
                current.FirstTeam.Add(added);

            if (isFirstTeam)
            {
                var d = removed.Points - added.Points;
                var p = Probability(t, d);
                if (rnd.NextDouble() < p)
                {
                    previous = current;

                    if (incumbent.Value < previous.Value)
                        incumbent = previous;
                }
            }
            else
            {
                var d = (double)(added.Price - removed.Price);
                var p = Probability(t, d);
                if (rnd.NextDouble() < p)
                {
                    previous = current;

                    if (incumbent.Cost < previous.Cost)
                        incumbent = previous;
                }
            }

            if (previous == current)
                t = Decrement(t, i);
        }

        Debug.WriteLine($"[SA] completed after {i} iterations");
        // Debug.Assert(initial.Value <= incumbent.Value);
        return incumbent.ToSolution();
    }
}
