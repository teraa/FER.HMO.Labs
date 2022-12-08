using System.Text;

namespace Common;

public static class InstanceLoader
{
    public static Instance LoadFromFile(string fileName, Encoding? encoding = null)
    {
        using var stream = File.OpenRead(fileName);
        return LoadFromStream(stream, encoding);
    }

    public static Instance LoadFromStream(Stream stream, Encoding? encoding = null)
    {
        using var reader = new StreamReader(stream, encoding ?? Encoding.UTF8);

        var players = new List<Player>();
        while (reader.ReadLine() is { } line)
        {
            var player = PlayerParser.Parse(line);
            players.Add(player);
        }

        return new Instance(players);
    }
}

