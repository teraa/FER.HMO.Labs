namespace Lab2.Tests;

[UsedImplicitly]
public class SaInstance2Tests : SolutionTests, IClassFixture<SaInstance2Tests.Fixture>
{
    public SaInstance2Tests(Fixture fixture)
        => Solution = fixture.Solution;

    [UsedImplicitly]
    public class Fixture
    {
        public Solution Solution { get; }

        public Fixture()
        {
            var players = InstanceLoader.LoadFromFile(Instances.Instance2);
            var solver = new SaSolver(players);
            Solution = solver.Solve();
        }
    }
}
