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
            var solver = new GreedySolver();
            var players = InstanceLoader.LoadFromFile(Instances.Instance3);
            Solution = solver.Solve(players);
        }
    }
}
