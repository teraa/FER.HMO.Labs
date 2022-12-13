// ReSharper disable UnusedType.Global
namespace Lab1.Tests;

file static class Instances
{
    public const string
        Instance1 = "2022_instance1.csv",
        Instance2 = "2022_instance2.csv",
        Instance3 = "2022_instance3.csv",
        OldInstance1 = "2021_instance1.csv";
}

[TestInstance(Instances.Instance1)] public class GraspInstance1Tests : SolverTests<GraspSolver> { }
[TestInstance(Instances.Instance2)] public class GraspInstance2Tests : SolverTests<GraspSolver> { }
[TestInstance(Instances.Instance3)] public class GraspInstance3Tests : SolverTests<GraspSolver> { }
[TestInstance(Instances.OldInstance1)] public class GraspOldInstance1Tests : SolverTests<GraspSolver> { }
[TestInstance(Instances.Instance1)] public class GreedyInstance1Tests : SolverTests<GreedySolver> { }
[TestInstance(Instances.Instance2)] public class GreedyInstance2Tests : SolverTests<GreedySolver> { }
[TestInstance(Instances.Instance3)] public class GreedyInstance3Tests : SolverTests<GreedySolver> { }
[TestInstance(Instances.OldInstance1)] public class GreedyOldInstance1Tests : SolverTests<GreedySolver> { }
