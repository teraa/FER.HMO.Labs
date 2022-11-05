﻿using System.Diagnostics;
using Common;

namespace Lab1;

public class GreedySolver : ISolver
{
    private readonly IReadOnlyList<Player> _instance;

    public GreedySolver(IReadOnlyList<Player> instance)
    {
        _instance = instance;
    }

    public Func<Player, double> OrderFunc { get; set; } = x => x.Points / Math.Log10((double) x.Price);

    public Solution Solve()
    {
        var solution = new PartialSolution();

        var players = _instance.OrderByDescending(OrderFunc);
        foreach (var player in players)
        {
            if (solution.Squad.Count == 15)
                break;

            if (solution.CanAddToSquad(player))
                solution.Squad.Add(player);
        }

        foreach (var player in solution.Squad)
        {
            if (solution.FirstTeam.Count == 11)
                break;

            if (solution.CanAddToFirstTeam(player))
                solution.FirstTeam.Add(player);
        }

        Debug.Assert(solution is {Squad.Count: 15, FirstTeam.Count: 11});
        return new Solution(solution.Squad, solution.FirstTeam);
    }
}
