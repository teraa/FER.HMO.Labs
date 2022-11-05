namespace Lab1.Tests;

[UsedImplicitly]
public class GraspInstance1Tests : SolutionTests, IClassFixture<GraspInstance1Tests.Fixture>
{
    public GraspInstance1Tests(Fixture fixture)
        => Solution = fixture.Solution;

    [UsedImplicitly]
    public class Fixture
    {
        public Solution Solution { get; }

        public Fixture()
        {
            var players = InstanceLoader.LoadFromFile(Instances.Instance1);
            var solver = new GraspSolver(players);
            Solution = solver.Solve();
        }
    }
}
