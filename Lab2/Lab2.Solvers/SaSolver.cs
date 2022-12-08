using Common;
using JetBrains.Annotations;
using Lab1.Solvers;

namespace Lab2.Solvers;

[PublicAPI]
public class SaSolver : ISolver
{
    public Func<IReadOnlyList<Player>, Solution> InitialSolutionProvider { get; set; } = GreedySolution;

    public double InitialTemperature { get; set; } = 100;

    public Func<double, int, double> DecrementFunction { get; set; } = DefaultDecrement;

    public Func<double, double, double, double> ProbabilityFunction { get; set; } = DefaultProbability;

    public static Solution GreedySolution(IReadOnlyList<Player> instance)
        => new GreedySolver().Solve(instance);

    public static Solution RandomSolution(IReadOnlyList<Player> instance)
        => new RandomSolver().Solve(instance);

    public static double DefaultDecrement(double temperature, int iteration)
        => temperature * 0.5;

    /// <summary>
    /// Accept if improving else accept with certain probability.
    /// </summary>
    public static double DefaultProbability(double temperature, double currentValue, double newValue)
    {
        // minimization
        // < 0 better
        // = 0 equal
        // > 0 worse
        var delta = newValue - currentValue;
        if (delta <= 0)
            return 1;

        var exponent = -delta / temperature;
        var p = Math.Exp(exponent);
        return p;
    }

    /// <summary>
    /// Accept with a certain probability.
    /// </summary>
    public static double AlternativeProbability(double temperature, double currentValue, double newValue)
    {
        // minimization
        var delta = newValue - currentValue;
        var exponent = delta / temperature;
        var e = Math.Exp(exponent);
        var p = 1 / (1 + e);
        return p;
    }

    public Solution Solve(IReadOnlyList<Player> instance)
    {
        var solution = InitialSolutionProvider(instance);
        return solution;
    }
}
