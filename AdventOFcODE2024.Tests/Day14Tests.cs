namespace AdventOfCode2024.Tests;
public class Day14Tests : TestBase<Day14>
{
    public override string Exercise1TestInput => """
        p=0,4 v=3,-3
        p=6,3 v=-1,-3
        p=10,3 v=-1,2
        p=2,0 v=2,-1
        p=0,0 v=1,3
        p=3,0 v=-2,-2
        p=7,6 v=-1,-3
        p=3,0 v=-1,-2
        p=9,3 v=2,3
        p=7,3 v=-1,2
        p=2,4 v=2,-3
        p=9,5 v=-3,-3
        """;

    public override object Exercise1TestResult => 12;

    public override object Exercise2TestResult => base.Exercise2TestResult;

    public override void Exercise1()
    {
        int result = Day.Exercise1(Exercise1TestInput, 11, 7);

        Assert.Equal(Exercise1TestResult, result);
    }

    [Fact]
    public void Ex1Debug()
    {
        var input = """
            p=0,0 v=0,0
            p=2,0 v=0,0
            p=0,2 v=0,0
            p=2,2 v=0,0
            """;
        int result = Day.Exercise1(input, 3, 3);
        Assert.Equal(1, result);
    }
}
