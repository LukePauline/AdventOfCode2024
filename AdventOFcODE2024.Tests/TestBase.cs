namespace AdventOfCode2024.Tests;

public abstract class TestBase<TDay> where TDay : IDay, new()
{
    public TDay Day = new();

    [Fact]
    public void Exercise1()
    {
        // Act
        var result = Day.Exercise1(Exercise1TestInput);

        // Assert
        Assert.Equal(Exercise1TestResult, result);
    }

    [Fact]
    public void Exercise2()
    {
        // Act
        var result = Day.Exercise2(Exercise2TestInput);

        // Assert
        Assert.Equal(Exercise2TestResult, result);
    }

    public virtual string Exercise1TestInput { get; } = "";
    public virtual object Exercise1TestResult { get; } = "";
    public virtual string Exercise2TestInput => Exercise1TestInput;
    public virtual object Exercise2TestResult { get; } = "";
}

