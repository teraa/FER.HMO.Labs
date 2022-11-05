using System.Diagnostics;
using Common;

namespace Lab1;

public class GreedySolver : ISolver
{
    private readonly IReadOnlyList<Player> _instance;
    private State _state = null!;

    public GreedySolver(IReadOnlyList<Player> instance)
    {
        _instance = instance;
    }

    public Func<Player, double> OrderFunc { get; set; } = x => x.Points / Math.Log10((double) x.Price);

    public Solution Solve()
    {
        _state = new State();

        var players = _instance.OrderByDescending(OrderFunc);
        foreach (var player in players)
        {
            if (_state.Squad.Count == 15)
                break;

            if (CanPickForSquad(player))
                _state.Squad.Add(player);
        }

        foreach (var player in _state.Squad)
        {
            if (_state.FirstTeam.Count == 11)
                break;

            if (CanPickForFirstTeam(player))
                _state.FirstTeam.Add(player);
        }

        var solution = new Solution(_state.Squad, _state.FirstTeam);

        Debug.Assert(solution is {Squad.Count: 15, FirstTeam.Count: 11});
        return solution;
    }

    private bool CanPickForSquad(Player player)
    {
        var players = _state.Squad.Concat(new[] {player}).ToList();
        if (players.Sum(x => x.Price) > 100m)
            return false;

        if (players.Average(x => x.Price) * 15 > 100m)
            return false;

        if (players.Count(x => x.Club == player.Club) > 3)
            return false;

        var formation = Formation.FromPlayers(players);

        return formation.Values.Zip(Formation.SquadFormation.Values).All(x => x.First <= x.Second);
    }

    private bool CanPickForFirstTeam(Player player)
    {
        var players = _state.FirstTeam.Concat(new[] {player});
        var formation = Formation.FromPlayers(players);

        return Formation.ValidFormations.Any(validFormation =>
            formation.Values.Zip(validFormation.Values).All(x => x.First <= x.Second));
    }

    private class State
    {
        public List<Player> Squad { get; } = new();
        public List<Player> FirstTeam { get; } = new();
    }
}
