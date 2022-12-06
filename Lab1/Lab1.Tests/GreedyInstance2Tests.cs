namespace Lab1.Tests;

[UsedImplicitly]
public class GreedyInstance2Tests : SolutionTests, IClassFixture<GreedyInstance2Tests.Fixture>
{
    public GreedyInstance2Tests(Fixture fixture)
        => Solution = fixture.Solution;

    [UsedImplicitly]
    public class Fixture
    {
        public Solution Solution { get; }

        public Fixture()
        {
            var players = InstanceLoader.LoadFromFile(Instances.Instance2);
            var solver = new GreedySolver(players);
            Solution = solver.Solve();
        }
    }
}