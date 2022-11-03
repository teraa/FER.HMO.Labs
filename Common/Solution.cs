namespace Common;

public class Solution
{
    public Solution(IReadOnlyList<Player> squad)
    {
        Squad = squad;
    }

    public IReadOnlyList<Player> Squad { get; }
    public IEnumerable<Player> FirstTeam => Squad.Take(11);
}
