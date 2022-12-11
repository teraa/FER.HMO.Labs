using Common;
using JetBrains.Annotations;
using Lab1.Solvers;

namespace Lab2.Solvers;

[PublicAPI]
public class SaSolver : ISolver
{
    public ISolver InitialSolver { get; set; } = new GreedySolver();
    public double InitialTemperature { get; set; } = 100;
    public double FinalTemperature { get; set; } = 0.01;
    public Func<double, int, double> DecrementFunction { get; set; } = SaFunctions.DefaultDecrement;
    public Func<double, double, double, double> ProbabilityFunction { get; set; } = SaFunctions.DefaultProbability;

    public Solution Solve(Instance instance)
    {
        var incumbent = InitialSolver.Solve(instance);
        return incumbent;
    }
}
