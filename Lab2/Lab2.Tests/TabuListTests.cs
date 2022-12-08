using FluentAssertions;

namespace Lab2.Tests;

public class TabuListTests
{
    [Fact]
    public void TryAddTests()
    {
        var tl = new TabuList<int>(4);
        tl.TryAdd(1).Should().BeTrue();
        tl.TryAdd(1).Should().BeFalse();

        tl.TryAdd(2).Should().BeTrue();

        tl.TryAdd(3).Should().BeTrue();

        tl.TryAdd(4).Should().BeTrue();
        tl.TryAdd(1).Should().BeFalse();

        tl.TryAdd(5).Should().BeTrue();
        tl.TryAdd(1).Should().BeTrue();
    }
}
