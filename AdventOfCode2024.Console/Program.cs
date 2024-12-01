using AdventOfCode2024;
using System.Reflection;
using static AdventOfCode2024.Console.InputHelpers;

Console.WriteLine("Enter day:");
string? dayInpt = Console.ReadLine();

if (string.IsNullOrWhiteSpace(dayInpt))
    dayInpt = DateTime.Now.Day.ToString();

string input = await GetInputFromUrl(int.Parse(dayInpt));

Type solution = Assembly.GetAssembly(typeof(IDay))?.GetType($"AdventOfCode2024.Day{dayInpt}")
    ?? throw new InvalidOperationException("Solution not found");

IDay instance = Activator.CreateInstance(solution) as IDay
    ?? throw new InvalidCastException("Solution does not implement IDay");

string? ex = "";
while (ex != "1" && ex != "2")
{
    Console.WriteLine("Enter exercise:");
    ex = Console.ReadLine();
}

if (ex == "1")
    Console.WriteLine(instance.Exercise1(input));
else
    Console.WriteLine(instance.Exercise2(input));

Console.WriteLine("Press any key to exit");
Console.ReadKey();
