namespace Common;

public class Formation
{
    private readonly int[] _formation;

    static Formation()
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

        ValidFormations = formations;
    }

    public Formation(int[] formation)
    {
        if (formation is null)
            throw new ArgumentNullException(nameof(formation));
        if (formation.Length != 4)
            throw new ArgumentOutOfRangeException(nameof(formation));

        _formation = formation;
    }
    public Formation(int gk, int def, int mid, int fw)
    {
        _formation = new[] {gk, def, mid, fw};
    }

    public static IReadOnlyList<Formation> ValidFormations { get; }
    public static Formation SquadFormation { get; } = new(2, 5, 5, 3);

    public int this[Position position]
    {
        get => _formation[(int) position];
        private set => _formation[(int) position] = value;
    }

    public IReadOnlyList<int> Values
        => _formation;

    public override string ToString()
        => string.Join(',', _formation);

    public static Formation FromPlayers(IEnumerable<Player> players)
    {
        var formation = new Formation(0, 0, 0, 0);
        foreach (var player in players)
            formation[player.Position]++;

        return formation;
    }
}
