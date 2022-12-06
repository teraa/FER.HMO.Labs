namespace Common;

public static class PlayerParser
{
    public static Player Parse(string input)
    {
        const char separator = ',';
        string[] parts = input.Split(separator);

        const int fields = 6;
        if (parts.Length != fields)
            throw new FormatException($"Invalid number of fields, expected: {fields}, got: {parts.Length}");

        var id = int.Parse(parts[0]);
        var position = Enum.Parse<Position>(parts[1]);
        var name = parts[2];
        var club = parts[3];
        var points = int.Parse(parts[4]);
        var price = decimal.Parse(parts[5]);

        return new Player
        (
            Id: id,
            Position: position,
            Name: name,
            Club: club,
            Points: points,
            Price: price
        );
    }
}
