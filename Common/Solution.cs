namespace Common;

public class Solution
{
    public List<Player> Squad { get; } = new();
    public IEnumerable<Player> FirstTeam => Squad.Take(11);
}
