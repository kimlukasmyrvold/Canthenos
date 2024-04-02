using System.Net.Http.Headers;
using System.Text.Json;

namespace Canthenos.DataAccessLibrary;

public class GithubData : IGithubData
{
    
    public async Task<(string, string)> GetLatestVersion()
    {
        const bool enabled = true;
        if (!enabled) return ("https://github.com/kimlukasmyrvold/Canthenos/releases/latest", "latest");

        new DotEnv().Load(Path.Combine(Directory.GetCurrentDirectory(), ".env"));

        const string apiUrl = "https://api.github.com/repos/kimlukasmyrvold/Canthenos/releases/latest";
        var token = Environment.GetEnvironmentVariable("GITHUB_TOKEN");

        using var client = new HttpClient();
        client.DefaultRequestHeaders.Accept.Add(
            new MediaTypeWithQualityHeaderValue("application/vnd.github.v3+json"));
        client.DefaultRequestHeaders.UserAgent.Add(new ProductInfoHeaderValue("Canthenos", "1.0"));
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

        var response = await client.GetAsync(apiUrl);

        if (response.IsSuccessStatusCode)
        {
            var jsonString = await response.Content.ReadAsStringAsync();
            var release = JsonSerializer.Deserialize<GithubRelease>(jsonString);
            if (release != null) return (release.html_url!, release.tag_name!);
        }

        Console.WriteLine($"Failed to get latest release. Status code: {response.StatusCode}");
        return ("https://github.com/kimlukasmyrvold/Canthenos/releases/latest", "latest");
    }

    private class GithubRelease
    {
        public string? html_url { get; init; }
        public string? tag_name { get; init; }
    }
}