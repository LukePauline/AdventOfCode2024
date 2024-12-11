namespace AdventOfCode2024.Tests;
public class Day9Tests : TestBase<Day9>
{
    public override string Exercise1TestInput => "2333133121414131402";
    public override object Exercise1TestResult => 1928L;
    public override object Exercise2TestResult => 2858L;

    [Theory]
    [InlineData("43623251202", 636)]
    [InlineData("2333133121414131499", 6204)]
    public void AdditionalEx2Tests(string input, long expected)
    {
        var result = Day.Exercise2(input);
        Assert.Equal(expected, result);
    }
}
