using Common;
using JetBrains.Annotations;
using Lab1.Solvers;

namespace Lab2.Solvers;

[PublicAPI]
public class TabuSolver : ISolver
{
    public ISolver InitialSolver { get; set; } = new GreedySolver();

    public Solution Solve(IReadOnlyList<Player> instance)
    {
        var solution = InitialSolver.Solve(instance);
        return solution;
    }
}
