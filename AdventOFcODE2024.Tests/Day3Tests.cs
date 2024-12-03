namespace AdventOfCode2024.Tests;
public class Day3Tests : TestBase<Day3>
{
    public override string Exercise1TestInput => "xmul(2,4)%&mul[3,7]!@^do_not_mul(5,5)+mul(32,64]then(mul(11,8)mul(8,5))";

    public override object Exercise1TestResult => 161;

    public override string Exercise2TestInput => "xmul(2,4)&mul[3,7]!^don't()_mul(5,5)+mul(32,64](mul(11,8)undo()?mul(8,5))";

    public override object Exercise2TestResult => 48;
}
