namespace Common;

public record Solution(
    IReadOnlyList<Player> Squad,
    IReadOnlyList<Player> FirstTeam)
{
    public int Value => FirstTeam.Sum(x => x.Points);
    public decimal Cost => Squad.Sum(x => x.Price);
}
