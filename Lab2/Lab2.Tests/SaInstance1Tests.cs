namespace Lab2.Tests;

[UsedImplicitly]
public class SaInstance1Tests : SolutionTests, IClassFixture<SaInstance1Tests.Fixture>
{
    public SaInstance1Tests(Fixture fixture)
        => Solution = fixture.Solution;

    [UsedImplicitly]
    public class Fixture
    {
        public Solution Solution { get; }

        public Fixture()
        {
            var players = InstanceLoader.LoadFromFile(Instances.Instance1);
            var solver = new SaSolver(players);
            Solution = solver.Solve();
        }
    }
}
