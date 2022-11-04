using System.Diagnostics;
using Common;

namespace Lab1;

public class GreedySolver : ISolver
{
    private static readonly IReadOnlyList<Formation> s_validFormations;
    private static readonly Formation s_squadFormation = new(2, 5, 5, 3);

    static GreedySolver()
    {
        var formations = new List<Formation>();

        int gk = 1;
        for (int def = 3; def <= 5; def++)
        for (int mid = 0; mid <= 5; mid++)
        for (int fw = 1; fw <= 3; fw++)
        {
            if (gk + def + mid + fw != 11)
                continue;

            formations.Add(new Formation(new[] {gk, def, mid, fw}));
        }

        s_validFormations = formations;
    }

    private readonly IReadOnlyList<Player> _instance;
    private State _state = null!;

    public GreedySolver(IReadOnlyList<Player> instance)
    {
        _instance = instance;
    }

    public Solution Solve()
    {
        _state = new State();

        // var players = _instance.OrderByDescending(x => Math.Sqrt(x.Points) / (double)x.Price);
        // var players = _instance.OrderByDescending(x => x.Points / x.Price);
        var players = _instance.OrderByDescending(x => x.Points / Math.Sqrt((double)x.Price));
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

        return formation.Values.Zip(s_squadFormation.Values, (x, y) => x <= y).All(x => x);
    }

    private bool CanPickForFirstTeam(Player player)
    {
        var players = _state.FirstTeam.Concat(new[] {player});
        var formation = Formation.FromPlayers(players);

        return s_validFormations.Any(validFormation =>
            formation.Values.Zip(validFormation.Values, (x, y) => x <= y).All(x => x));
    }

    private class State
    {
        public List<Player> Squad { get; } = new();
        public List<Player> FirstTeam { get; } = new();
    }
}
