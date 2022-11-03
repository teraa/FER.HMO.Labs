using Common;

namespace Lab1;

public class GreedySolver : ISolver
{
    private readonly IReadOnlyList<Player> _instance;

    public GreedySolver(IReadOnlyList<Player> instance)
    {
        _instance = instance;
    }

    public Solution Solve()
    {
        throw new NotImplementedException();
    }
}
