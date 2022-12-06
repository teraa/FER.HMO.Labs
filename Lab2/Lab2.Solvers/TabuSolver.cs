using Common;
using JetBrains.Annotations;
using Lab1.Solvers;

namespace Lab2.Solvers;

[PublicAPI]
public class TabuSolver : ISolver
{
    private readonly IReadOnlyList<Player> _instance;

    public TabuSolver(IReadOnlyList<Player> instance)
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
