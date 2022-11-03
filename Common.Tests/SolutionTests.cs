namespace Common.Tests;

public abstract class SolutionTests
{
    protected Solution Solution { get; init; } = null!;

    [Fact]
    public void Squad_PlayerCount()
        => Solution.Squad
            .Should().HaveCount(15);

    [Fact]
    public void FirstTeam_PlayerCount()
        => Solution.FirstTeam
            .Should().HaveCount(11);

    [Fact]
    public void Squad_UniquePlayers()
        => Solution.Squad.Should().OnlyHaveUniqueItems();

    [Fact]
    public void FirstTeam_SubsetOf_Squad()
        => Solution.FirstTeam.Should().BeSubsetOf(Solution.Squad);

    [Fact]
    public void Squad_Cost()
        => Solution.Squad.Sum(x => x.Price)
            .Should().BeLessThanOrEqualTo(100m);

    [Fact]
    public void Squad_MaxPlayersPerClub()
    {
        using var scope = new AssertionScope();
        foreach (var clubPlayers in Solution.Squad.GroupBy(x => x.Club))
        {
            clubPlayers.Should().HaveCountLessThanOrEqualTo(3);
        }
    }

    [Fact]
    public void Squad_GK_Count()
        => Solution.Squad.Where(x => x.Position == Position.GK)
            .Should().HaveCount(2);

    [Fact]
    public void Squad_DEF_Count()
        => Solution.Squad.Where(x => x.Position == Position.DEF)
            .Should().HaveCount(5);

    [Fact]
    public void Squad_MID_Count()
        => Solution.Squad.Where(x => x.Position == Position.MID)
            .Should().HaveCount(5);

    [Fact]
    public void Squad_FW_Count()
        => Solution.Squad.Where(x => x.Position == Position.FW)
            .Should().HaveCount(3);

    [Fact]
    public void FirstTeam_GK_Count()
        => Solution.FirstTeam.Where(x => x.Position == Position.GK)
            .Should().HaveCount(1);

    [Fact]
    public void FirstTeam_DEF_Count()
        => Solution.FirstTeam.Where(x => x.Position == Position.DEF)
            .Should().HaveCountGreaterThanOrEqualTo(3)
            .And.HaveCountLessThanOrEqualTo(5);

    [Fact]
    public void FirstTeam_FW_Count()
        => Solution.FirstTeam.Where(x => x.Position == Position.FW)
            .Should().HaveCountGreaterThanOrEqualTo(1)
            .And.HaveCountLessThanOrEqualTo(3);

}
