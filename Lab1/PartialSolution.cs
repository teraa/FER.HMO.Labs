﻿using Common;

namespace Lab1;

internal class PartialSolution
{
    public List<Player> Squad { get; } = new();
    public List<Player> FirstTeam { get; } = new();

    public bool CanAddToSquad(Player player)
    {
        var squad = Squad.Concat(new[] {player}).ToList();

        // if (squad.Sum(x => x.Price) > 100m)
        //     return false;

        if (squad.Average(x => x.Price) * 15 > 100m)
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
}
