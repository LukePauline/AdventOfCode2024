namespace AdventOfCode2024.Tests;
public class Day2Tests : TestBase<Day2>
{
    public override string Exercise1TestInput => """
        7 6 4 2 1
        1 2 7 8 9
        9 7 6 2 1
        1 3 2 4 5
        8 6 4 4 1
        1 3 6 7 9
        """;

    public override object Exercise1TestResult => 2;

    public override object Exercise2TestResult => 4;
}
