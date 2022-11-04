namespace Common;

public class Formation
{
    private readonly int[] _formation;

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
