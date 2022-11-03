namespace Common.Tests;

public class PlayerParserTests
{
    [Fact]
    public void Parse()
    {
        var player = PlayerParser.Parse("1,GK,Mendy,Chelsea,140,6.0");
        player.Should().Be(new Player(1, Position.GK, "Mendy", "Chelsea", 140, 6.0m));
    }
}
