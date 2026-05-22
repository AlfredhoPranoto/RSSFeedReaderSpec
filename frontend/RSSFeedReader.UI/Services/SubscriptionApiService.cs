using System.Net.Http.Json;
using RSSFeedReader.UI.Models;

namespace RSSFeedReader.UI.Services;

public class SubscriptionApiService
{
    private readonly HttpClient _http;

    public SubscriptionApiService(HttpClient http)
    {
        _http = http;
    }

    public virtual async Task<List<SubscriptionDto>> GetSubscriptionsAsync()
    {
        var result = await _http.GetFromJsonAsync<List<SubscriptionDto>>("subscriptions");
        return result ?? new List<SubscriptionDto>();
    }

    public virtual async Task<SubscriptionDto?> AddSubscriptionAsync(string url)
    {
        var response = await _http.PostAsJsonAsync("subscriptions", new { url });
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<SubscriptionDto>();
    }
}
