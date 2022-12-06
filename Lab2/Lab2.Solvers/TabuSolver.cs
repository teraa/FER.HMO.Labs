using Common;

namespace Lab2.Solvers;

public class TabuSolver : ISolver
{
    private readonly IReadOnlyList<Player> _instance;

    public TabuSolver(IReadOnlyList<Player> instance)
    {
        _instance = instance;
    }

    public Solution Solve()
    {
        var solver = new RandomSolver(_instance);
        return solver.Solve();
    }
}
