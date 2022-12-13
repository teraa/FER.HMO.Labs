namespace Common.Tests;

[AttributeUsage(AttributeTargets.Class)]
public sealed class TestInstanceAttribute : Attribute
{
    public TestInstanceAttribute(string fileName)
        => FileName = fileName;

    public string FileName { get; }
}
