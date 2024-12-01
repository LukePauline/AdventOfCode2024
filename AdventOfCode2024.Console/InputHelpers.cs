namespace AdventOfCode2024.Console;
public static class InputHelpers
{
    public static async Task<string> GetInputFromUrl(int day)
    {
        string file = $"input/day{day}.txt";
        if (!Directory.Exists("input"))
            Directory.CreateDirectory("input");
        if (File.Exists(file))
            return File.ReadAllText(file);

        var sessionCookie = File.ReadAllText("cookie.txt");

        // fetch
        var client = new HttpClient();
        HttpRequestMessage request = new(HttpMethod.Get, $"https://adventofcode.com/2024/day/{day}/input");
        request.Headers.TryAddWithoutValidation("Cookie", $"session={sessionCookie}");
        var response = await client.SendAsync(request);

        // cache
        string content = await response.Content.ReadAsStringAsync();
        File.WriteAllText(file, content);

        return content;
    }
}
