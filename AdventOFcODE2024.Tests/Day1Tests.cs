namespace AdventOfCode2024.Tests;
public class Day1Tests : TestBase<Day1>
{
    public override string Exercise1TestInput => """
        3   4
        4   3
        2   5
        1   3
        3   9
        3   3
        """;

    public override object Exercise1TestResult => 11;

    public override string Exercise2TestInput => base.Exercise2TestInput;

    public override object Exercise2TestResult => 31;
}
