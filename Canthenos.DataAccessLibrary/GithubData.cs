using System.Globalization;
using System.Net.Http.Headers;
using System.Text.Json;

namespace Canthenos.DataAccessLibrary;

public class GithubData : IGithubData
{
    private readonly ISessionStorage _sessionStorage;
    private readonly ILocalStorage _localStorage;

    public GithubData(ISessionStorage sessionStorage, ILocalStorage localStorage)
    {
        _sessionStorage = sessionStorage;
        _localStorage = localStorage;
    }

    public async Task<(string url, string version)> GetLatestVersion()
    {
        const string versionKey = "GitHubVersion";
        const string lastCheckKey = "GitHubVersionLastChecked";

        var firstCheck = false;
        var fromStorage = await _sessionStorage.Get(lastCheckKey);
        if (string.IsNullOrEmpty(fromStorage)) firstCheck = true;

        var ok = DateTime.TryParse(fromStorage, CultureInfo.InvariantCulture,
            DateTimeStyles.None, out var lastChecked);
        if (!ok)
        {
            lastChecked = DateTime.UtcNow;
            await _sessionStorage.Set(lastCheckKey, lastChecked.ToString(CultureInfo.InvariantCulture));
        }

        if (!firstCheck && DateTime.UtcNow < lastChecked.AddHours(1))
        {
            if (string.IsNullOrEmpty(await _localStorage.Get(versionKey)))
                await _localStorage.Set(versionKey, "latest");

            var version = await _localStorage.Get(versionKey);
            return (
                version == "latest"
                    ? "https://github.com/kimlukasmyrvold/Canthenos/releases/latest"
                    : $"https://github.com/kimlukasmyrvold/Canthenos/releases/tag/{version}", version);
        }
        
        await _sessionStorage.Set(lastCheckKey, DateTime.UtcNow.ToString(CultureInfo.InvariantCulture));

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
            if (release != null)
            {
                await _localStorage.Set(versionKey, release.tag_name!);
                return (release.html_url!, release.tag_name!);
            }
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