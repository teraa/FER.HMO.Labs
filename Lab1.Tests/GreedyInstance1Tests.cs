namespace Lab1.Tests;

[UsedImplicitly]
public class GreedyInstance1Tests : Tests, IClassFixture<GreedyInstance1Tests.Fixture>
{
    public GreedyInstance1Tests(Fixture fixture)
        => Solution = fixture.Solution;

    [UsedImplicitly]
    public class Fixture
    {
        public Solution Solution { get; }

        public Fixture()
        {
            var solver = new GreedySolver();
            var players = InstanceLoader.LoadFromFile(Instances.Instance1);
            Solution = solver.Solve(players);
        }
    }
}
