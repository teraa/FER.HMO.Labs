namespace Lab1.Tests;

[UsedImplicitly]
public class GraspOldInstance1Tests : SolutionTests, IClassFixture<GraspOldInstance1Tests.Fixture>
{
    public GraspOldInstance1Tests(Fixture fixture)
        => Solution = fixture.Solution;

    [UsedImplicitly]
    public class Fixture
    {
        public Solution Solution { get; }

        public Fixture()
        {
            var players = InstanceLoader.LoadFromFile(Instances.OldInstance1);
            var solver = new GraspSolver();
            Solution = solver.Solve(players);
        }
    }
}
