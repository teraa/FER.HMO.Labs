namespace Common;

public interface ISolver
{
    Solution Solve(IEnumerable<Player> players);
}
