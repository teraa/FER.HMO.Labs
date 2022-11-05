namespace Lab1.Tests;

[UsedImplicitly]
public class GraspInstance3Tests : SolutionTests, IClassFixture<GraspInstance3Tests.Fixture>
{
    public GraspInstance3Tests(Fixture fixture)
        => Solution = fixture.Solution;

    [UsedImplicitly]
    public class Fixture
    {
        public Solution Solution { get; }

        public Fixture()
        {
            var players = InstanceLoader.LoadFromFile(Instances.Instance3);
            var solver = new GraspSolver(players);
            Solution = solver.Solve();
        }
    }
}
