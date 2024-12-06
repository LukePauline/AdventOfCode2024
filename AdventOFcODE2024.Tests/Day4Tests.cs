using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2024.Tests;
public class Day4Tests : TestBase<Day4>
{
    public override string Exercise1TestInput => """
        MMMSXXMASM
        MSAMXMSMSA
        AMXSXMAAMM
        MSAMASMSMX
        XMASAMXAMM
        XXAMMXXAMA
        SMSMSASXSS
        SAXAMASAAA
        MAMMMXMMMM
        MXMXAXMASX
        """;

    public override object Exercise1TestResult => 18;

    public override string Exercise2TestInput => base.Exercise2TestInput;

    public override object Exercise2TestResult => 9;
}
