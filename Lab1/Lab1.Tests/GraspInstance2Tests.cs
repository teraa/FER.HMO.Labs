namespace Lab1.Tests;

[UsedImplicitly]
public class GraspInstance2Tests : SolutionTests, IClassFixture<GraspInstance2Tests.Fixture>
{
    public GraspInstance2Tests(Fixture fixture)
        => Solution = fixture.Solution;

    [UsedImplicitly]
    public class Fixture
    {
        public Solution Solution { get; }

        public Fixture()
        {
            var players = InstanceLoader.LoadFromFile(Instances.Instance2);
            var solver = new GraspSolver();
            Solution = solver.Solve(players);
        }
    }
}
