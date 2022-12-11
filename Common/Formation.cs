using System.Diagnostics;

namespace Common;

public class Formation
{
    public int Gk { get; private set; }
    public int Def { get; private set; }
    public int Mid { get; private set; }
    public int Fw { get; private set; }

    static Formation()
    {
        var formations = new List<Formation>();

        const int gk = Constants.FirstTeamGkCount;
        for (int def = Constants.FirstTeamMinDefCount; def <= Constants.SquadDefCount; def++)
        for (int mid = Constants.FirstTeamMinMidCount; mid <= Constants.SquadMidCount; mid++)
        for (int fw = Constants.FirstTeamMinFwCount; fw <= Constants.SquadFwCount; fw++)
        {
            if (gk + def + mid + fw != Constants.FirstTeamCount)
                continue;

            formations.Add(new Formation(gk, def, mid, fw));
        }

        ValidFormations = formations;
    }

    public Formation(int gk, int def, int mid, int fw)
    {
        Gk = gk;
        Def = def;
        Mid = mid;
        Fw = fw;
    }

    private Formation() { }

    public static IReadOnlyList<Formation> ValidFormations { get; }

    public static Formation SquadFormation { get; } = new(
        gk: Constants.SquadGkCount,
        def: Constants.SquadDefCount,
        mid: Constants.SquadMidCount,
        fw: Constants.SquadFwCount);

    public int this[Position position] => position switch
    {
        Position.GK => Gk,
        Position.DEF => Def,
        Position.MID => Mid,
        Position.FW => Fw,
        _ => throw new UnreachableException()
    };

    public override string ToString()
        => $"{Gk},{Def},{Mid},{Fw}";

    public bool IsIncompleteValid()
    {
        foreach (var formation in ValidFormations)
        {
            if (Gk <= formation.Gk &&
                Def <= formation.Def &&
                Mid <= formation.Mid &&
                Fw <= formation.Fw)
                return true;
        }

        return false;
    }

    public bool IsIncompleteSquad()
    {
        return
            Gk <= Constants.SquadGkCount &&
            Def <= Constants.SquadDefCount &&
            Mid <= Constants.SquadMidCount &&
            Fw <= Constants.SquadFwCount;
    }

    private void AddPlayer(Player player)
    {
        switch (player.Position)
        {
            case Position.GK: Gk++; break;
            case Position.DEF: Def++; break;
            case Position.MID: Mid++; break;
            case Position.FW: Fw++; break;
            default: throw new UnreachableException();
        }
    }

    public static Formation FromPlayers(IReadOnlyList<Player> players, Player newPlayer)
    {
        var formation = new Formation();

        for (var i = 0; i < players.Count; i++)
            formation.AddPlayer(players[i]);

        formation.AddPlayer(newPlayer);
        return formation;
    }
}
