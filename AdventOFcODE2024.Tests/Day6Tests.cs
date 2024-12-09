namespace AdventOfCode2024.Tests;
public class Day6Tests : TestBase<Day6>
{
    public override string Exercise1TestInput => """
        ....#.....
        .........#
        ..........
        ..#.......
        .......#..
        ..........
        .#..^.....
        ........#.
        #.........
        ......#...
        """;

    public override object Exercise1TestResult => 41;

    public override object Exercise2TestResult => 6;

    [Fact]
    public void Exercise2AdditionalTest()
    {
        string input = """
            .##..
            ....#
            .....
            .^.#.
            .....
            """;

        var result = Day.Exercise2(input);

        Assert.Equal(1, result);
    }
}
