namespace Lab1.Tests;

[UsedImplicitly]
public class GreedyInstance2Tests : Tests, IClassFixture<GreedyInstance2Tests.Fixture>
{
    public GreedyInstance2Tests(Fixture fixture)
        => Solution = fixture.Solution;

    [UsedImplicitly]
    public class Fixture
    {
        public Solution Solution { get; }

        public Fixture()
        {
            var solver = new GreedySolver();
            var players = InstanceLoader.LoadFromFile(Instances.Instance2);
            Solution = solver.Solve(players);
        }
    }
}
