namespace Lab1.Tests;

[UsedImplicitly]
public class GreedyOldInstance1Tests : SolutionTests, IClassFixture<GreedyOldInstance1Tests.Fixture>
{
    public GreedyOldInstance1Tests(Fixture fixture)
        => Solution = fixture.Solution;

    [UsedImplicitly]
    public class Fixture
    {
        public Solution Solution { get; }

        public Fixture()
        {
            var players = InstanceLoader.LoadFromFile(Instances.OldInstance1);
            var solver = new GreedySolver();
            Solution = solver.Solve(players);
        }
    }
}
