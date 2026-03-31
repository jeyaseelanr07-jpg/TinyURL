using System.Net.Http.Json;
using TinyURL.Models;

namespace TinyURL.Services;

public class UrlService
{
    private readonly HttpClient _http;
    public UrlService(HttpClient http) => _http = http;

    public async Task<List<ShortUrl>> GetPublicLinksAsync() =>
        await _http.GetFromJsonAsync<List<ShortUrl>>("api/links") ?? new();

    public async Task CreateLinkAsync(string url, bool isPrivate) =>
        await _http.PostAsJsonAsync("api/shorten", new { Url = url, IsPrivate = isPrivate });

    public async Task DeleteLinkAsync(int id) =>
        await _http.DeleteAsync($"api/links/{id}");
}