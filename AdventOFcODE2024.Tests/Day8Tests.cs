﻿namespace AdventOfCode2024.Tests;
public class Day8Tests : TestBase<Day8>
{
    public override string Exercise1TestInput => """
        ............
        ........0...
        .....0......
        .......0....
        ....0.......
        ......A.....
        ............
        ............
        ........A...
        .........A..
        ............
        ............
        """;

    public override object Exercise1TestResult => 14;

    public override object Exercise2TestResult => 34;
}