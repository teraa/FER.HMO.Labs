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
        var squad = Squad.Concat(new[] {player}).ToList();

        if (averagePriceCheck)
        {
            if (squad.Average(x => x.Price) * Constants.SquadCount > Constants.MaxSquadCost)
                return false;
        }
        else
        {
            if (squad.Sum(x => x.Price) > Constants.MaxSquadCost)
                return false;
        }

        if (squad.Count(x => x.Club == player.Club) > Constants.MaxPlayersPerClub)
            return false;

        var formation = Formation.FromPlayers(squad);

        return formation.Values.Zip(Formation.SquadFormation.Values).All(x => x.First <= x.Second);
    }

    public bool CanAddToFirstTeam(Player player)
    {
        var firstTeam = FirstTeam.Concat(new[] {player});
        var formation = Formation.FromPlayers(firstTeam);

        return Formation.ValidFormations.Any(validFormation =>
            formation.Values.Zip(validFormation.Values).All(x => x.First <= x.Second));
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
