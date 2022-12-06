using Common;
using Lab1.Solvers;

namespace Lab2.Solvers;

public class SaSolver : ISolver
{
    private readonly IReadOnlyList<Player> _instance;

    public SaSolver(IReadOnlyList<Player> instance)
    {
        _instance = instance;
    }

    public Func<IReadOnlyList<Player>, Solution> InitialSolutionProvider { get; set; }
        = instance => new GreedySolver(instance).Solve();

    public Solution Solve()
    {
        var solution = InitialSolutionProvider(_instance);
        return solution;
    }
}
