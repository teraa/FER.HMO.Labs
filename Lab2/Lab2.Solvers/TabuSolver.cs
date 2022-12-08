using Common;
using JetBrains.Annotations;
using Lab1.Solvers;

namespace Lab2.Solvers;

[PublicAPI]
public class TabuSolver : ISolver
{
    public Func<IReadOnlyList<Player>, Solution> InitialSolutionProvider { get; set; }
        = instance => new GreedySolver().Solve(instance);

    public Solution Solve(IReadOnlyList<Player> instance)
    {
        var solution = InitialSolutionProvider(instance);
        return solution;
    }
}
