using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AdventOfCode2024;
public partial class Day3 : IDay
{
    public object Exercise1(string input)
    {
        var matches = ExtractData().Matches(input);
        return matches.Sum(x => int.Parse(x.Groups[1].Value) * int.Parse(x.Groups[2].Value));
    }

    public object Exercise2(string input)
    {
        var reduced = GetDonts().Replace(input + "do()", "");
        var matches = ExtractData().Matches(reduced);
        return matches.Sum(x => int.Parse(x.Groups[1].Value) * int.Parse(x.Groups[2].Value));
    }

    [GeneratedRegex(@"mul\(([0-9]{1,3}),([0-9]{1,3})\)")]
    private static partial Regex ExtractData();

    [GeneratedRegex(@"(?<=don't\(\)).*?(?=do\(\))", RegexOptions.Singleline)]
    private static partial Regex GetDonts();
}
