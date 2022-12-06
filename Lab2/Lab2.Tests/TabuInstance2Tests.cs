namespace Lab2.Tests;

[UsedImplicitly]
public class TabuInstance2Tests : SolutionTests, IClassFixture<TabuInstance2Tests.Fixture>
{
    public TabuInstance2Tests(Fixture fixture)
        => Solution = fixture.Solution;

    [UsedImplicitly]
    public class Fixture
    {
        public Solution Solution { get; }

        public Fixture()
        {
            var players = InstanceLoader.LoadFromFile(Instances.Instance2);
            var solver = new TabuSolver(players);
            Solution = solver.Solve();
        }
    }
}
