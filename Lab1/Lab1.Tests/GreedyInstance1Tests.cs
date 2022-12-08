namespace Lab1.Tests;

[UsedImplicitly]
public class GreedyInstance1Tests : SolutionTests, IClassFixture<GreedyInstance1Tests.Fixture>
{
    public GreedyInstance1Tests(Fixture fixture)
        => Solution = fixture.Solution;

    [UsedImplicitly]
    public class Fixture
    {
        public Solution Solution { get; }

        public Fixture()
        {
            var players = InstanceLoader.LoadFromFile(Instances.Instance1);
            var solver = new GreedySolver();
            Solution = solver.Solve(players);
        }
    }
}
