namespace Common.Tests;

public abstract class SolutionTests
{
    protected Solution Solution { get; init; } = null!;

    [Fact]
    public void Squad_PlayerCount()
        => Solution.Squad
            .Should().HaveCount(Constants.SquadCount);

    [Fact]
    public void FirstTeam_PlayerCount()
        => Solution.FirstTeam
            .Should().HaveCount(Constants.FirstTeamCount);

    [Fact]
    public void Squad_UniquePlayers()
        => Solution.Squad.Should().OnlyHaveUniqueItems();

    [Fact]
    public void FirstTeam_SubsetOf_Squad()
        => Solution.FirstTeam.Should().BeSubsetOf(Solution.Squad);

    [Fact]
    public void Squad_Cost()
        => Solution.Squad.Sum(x => x.Price)
            .Should().BeLessThanOrEqualTo(Constants.MaxSquadCost);

    [Fact]
    public void Squad_MaxPlayersPerClub()
    {
        using var scope = new AssertionScope();
        foreach (var clubPlayers in Solution.Squad.GroupBy(x => x.Club))
        {
            clubPlayers.Should().HaveCountLessThanOrEqualTo(Constants.MaxPlayersPerClub);
        }
    }

    [Fact]
    public void Squad_GK_Count()
        => Solution.Squad.Where(x => x.Position == Position.GK)
            .Should().HaveCount(Constants.SquadGkCount);

    [Fact]
    public void Squad_DEF_Count()
        => Solution.Squad.Where(x => x.Position == Position.DEF)
            .Should().HaveCount(Constants.SquadDefCount);

    [Fact]
    public void Squad_MID_Count()
        => Solution.Squad.Where(x => x.Position == Position.MID)
            .Should().HaveCount(Constants.SquadMidCount);

    [Fact]
    public void Squad_FW_Count()
        => Solution.Squad.Where(x => x.Position == Position.FW)
            .Should().HaveCount(Constants.SquadFwCount);

    [Fact]
    public void FirstTeam_GK_Count()
        => Solution.FirstTeam.Where(x => x.Position == Position.GK)
            .Should().HaveCount(Constants.FirstTeamGkCount);

    [Fact]
    public void FirstTeam_DEF_Count()
        => Solution.FirstTeam.Where(x => x.Position == Position.DEF)
            .Should().HaveCountGreaterThanOrEqualTo(Constants.FirstTeamMinDefCount)
            .And.HaveCountLessThanOrEqualTo(Constants.SquadDefCount);

    [Fact]
    public void FirstTeam_FW_Count()
        => Solution.FirstTeam.Where(x => x.Position == Position.FW)
            .Should().HaveCountGreaterThanOrEqualTo(Constants.FirstTeamMinFwCount)
            .And.HaveCountLessThanOrEqualTo(Constants.SquadFwCount);

}
