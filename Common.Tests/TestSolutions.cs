using System.Collections.Concurrent;

namespace Common.Tests;

public static class TestSolutions
{
    private static readonly IReadOnlyDictionary<string, Instance> s_instances =
        Directory.GetFiles("../../../../instances")
            .ToDictionary(x => Path.GetFileName(x)!, InstanceLoader.LoadFromFile);

    private static readonly ConcurrentDictionary<(Type SolverType, string InstanceFile), Solution> s_solutions = new();

    public static Solution Get<TSolver>(string instanceFile)
        where TSolver : ISolver, new()
        => s_solutions.GetOrAdd((typeof(TSolver), instanceFile),
            x => new TSolver().Solve(s_instances[x.InstanceFile]));
}
