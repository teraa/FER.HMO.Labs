// ReSharper disable UnusedType.Global
namespace Lab2.Tests;

file static class Instances
{
    public const string
        Instance1 = "Lab2_inst1.csv",
        Instance2 = "Lab2_inst2.csv";
}

[TestInstance(Instances.Instance1)] public class RandomInstance1Tests : SolverTests<RandomSolver> { }
[TestInstance(Instances.Instance2)] public class RandomInstance2Tests : SolverTests<RandomSolver> { }
[TestInstance(Instances.Instance1)] public class SaInstance1Tests : SolverTests<SaSolver> { }
[TestInstance(Instances.Instance2)] public class SaInstance2Tests : SolverTests<SaSolver> { }
[TestInstance(Instances.Instance1)] public class TabuInstance1Tests : SolverTests<TabuSolver> { }
[TestInstance(Instances.Instance2)] public class TabuInstance2Tests : SolverTests<TabuSolver> { }
