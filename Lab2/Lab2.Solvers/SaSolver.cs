using Common;
using JetBrains.Annotations;
using Lab1.Solvers;

namespace Lab2.Solvers;

[PublicAPI]
public class SaSolver : ISolver
{
    private readonly IReadOnlyList<Player> _instance;

    public SaSolver(IReadOnlyList<Player> instance)
    {
        _instance = instance;
    }

    public Func<IReadOnlyList<Player>, Solution> InitialSolutionProvider { get; set; }
        = instance => new GreedySolver(instance).Solve();

    public double InitialTemperature { get; set; } = 100;

    public Func<double, int, double> DecrementFunction { get; set; } = (t, _) => t * 0.5;

    public Solution Solve()
    {
        var solution = InitialSolutionProvider(_instance);
        return solution;
    }
}
