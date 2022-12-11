namespace Common;

public static class InstanceLoader
{
    public static Instance LoadFromFile(string fileName)
    {
        var players = File.ReadAllLines(fileName)
            .Select(PlayerParser.Parse)
            .ToList();

        return new Instance(players);
    }
}
