namespace Common;

public interface ISolver
{
    Solution Solve(IReadOnlyList<Player> instance);
}
