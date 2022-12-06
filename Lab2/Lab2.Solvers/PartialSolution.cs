using Common;

namespace Lab2.Solvers;

internal class PartialSolution
{
    public List<Player> Squad { get; } = new();
    public List<Player> FirstTeam { get; } = new();

    public int Value => FirstTeam.Sum(x => x.Points);
    public decimal Cost => Squad.Sum(x => x.Price);

    public bool CanAddToSquad(Player player, bool averagePriceCheck = false)
    {
        var squad = Squad.Concat(new[] {player}).ToList();

        if (squad.Sum(x => x.Price) > 100m)
            return false;

        if (averagePriceCheck && squad.Average(x => x.Price) * 15 > 100m)
            return false;

        if (squad.Count(x => x.Club == player.Club) > 3)
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
        => new(Squad, FirstTeam);

    public PartialSolution Clone()
    {
        var clone = new PartialSolution();
        clone.Squad.AddRange(Squad);
        clone.FirstTeam.AddRange(FirstTeam);

        return clone;
    }
}
