namespace Lab2.Tests;

[UsedImplicitly]
public class TabuInstance1Tests : SolutionTests, IClassFixture<TabuInstance1Tests.Fixture>
{
    public TabuInstance1Tests(Fixture fixture)
        => Solution = fixture.Solution;

    [UsedImplicitly]
    public class Fixture
    {
        public Solution Solution { get; }

        public Fixture()
        {
            var players = InstanceLoader.LoadFromFile(Instances.Instance1);
            var solver = new TabuSolver();
            Solution = solver.Solve(players);
        }
    }
}
