namespace Lab1.Tests;

[UsedImplicitly]
public class GreedyInstance3Tests : Tests, IClassFixture<GreedyInstance3Tests.Fixture>
{
    public GreedyInstance3Tests(Fixture fixture)
        => Solution = fixture.Solution;

    [UsedImplicitly]
    public class Fixture
    {
        public Solution Solution { get; }

        public Fixture()
        {
            var players = InstanceLoader.LoadFromFile(Instances.Instance3);
            var solver = new GreedySolver(players);
            Solution = solver.Solve();
        }
    }
}
