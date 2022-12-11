namespace Common;

public class SolutionBuilder
{
    public SolutionBuilder()
    {
        Squad = new();
        FirstTeam = new();
    }

    public SolutionBuilder(Solution solution)
    {
        Squad = solution.Squad.ToList();
        FirstTeam = solution.FirstTeam.ToList();
    }

    public List<Player> Squad { get; }
    public List<Player> FirstTeam { get; }

    public int Value => FirstTeam.Sum(x => x.Points);
    public decimal Cost => Squad.Sum(x => x.Price);

    public bool CanAddToSquad(Player player)
        => CanAddToSquad(player, true);

    public bool CanAddToSquad(Player player, bool averagePriceCheck)
    {
        var cost = Cost + player.Price;

        if (averagePriceCheck)
        {
            if (cost / (Squad.Count + 1) > Constants.MaxSquadCost / Constants.SquadCount)
                return false;
        }
        else
        {
            if (cost > Constants.MaxSquadCost)
                return false;
        }

        if (Squad.Count(x => x.Club == player.Club) + 1 > Constants.MaxPlayersPerClub)
            return false;

        var formation = Formation.FromPlayers(Squad, player);
        return formation.IsIncompleteSquad();
    }

    public bool CanAddToFirstTeam(Player player)
    {
        var formation = Formation.FromPlayers(FirstTeam, player);
        return formation.IsIncompleteValid();
    }

    public Solution ToSolution()
        => new(Squad.ToList(), FirstTeam.ToList());

    public SolutionBuilder Clone()
    {
        var clone = new SolutionBuilder();
        clone.Squad.AddRange(Squad);
        clone.FirstTeam.AddRange(FirstTeam);

        return clone;
    }
}
