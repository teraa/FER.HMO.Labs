namespace Lab2.Tests;

[UsedImplicitly]
public class RandomInstance2Tests : SolutionTests, IClassFixture<RandomInstance2Tests.Fixture>
{
    public RandomInstance2Tests(Fixture fixture)
        => Solution = fixture.Solution;

    [UsedImplicitly]
    public class Fixture
    {
        public Solution Solution { get; }

        public Fixture()
        {
            var players = InstanceLoader.LoadFromFile(Instances.Instance2);
            var solver = new SaSolver();
            Solution = solver.Solve(players);
        }
    }
}
