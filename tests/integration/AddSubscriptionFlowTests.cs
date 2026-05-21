using System.Net;
using System.Net.Http.Json;
using Microsoft.AspNetCore.Mvc.Testing;
using RSSFeedReader.Api.Models;

namespace RSSFeedReader.Integration.Tests;

public class AddSubscriptionFlowTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly HttpClient _client;

    public AddSubscriptionFlowTests(WebApplicationFactory<Program> factory)
    {
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task Get_Subscriptions_WhenEmpty_Returns200WithEmptyArray()
    {
        var response = await _client.GetAsync("/api/subscriptions");

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        var body = await response.Content.ReadFromJsonAsync<List<Subscription>>();
        Assert.NotNull(body);
        Assert.Empty(body);
    }

    [Fact]
    public async Task Post_ValidUrl_Returns201WithCreatedSubscription()
    {
        var response = await _client.PostAsJsonAsync("/api/subscriptions",
            new { url = "https://devblogs.microsoft.com/dotnet/feed/" });

        Assert.Equal(HttpStatusCode.Created, response.StatusCode);
        var body = await response.Content.ReadFromJsonAsync<Subscription>();
        Assert.NotNull(body);
        Assert.False(string.IsNullOrEmpty(body.Id));
        Assert.Equal("https://devblogs.microsoft.com/dotnet/feed/", body.Url);
        Assert.False(string.IsNullOrEmpty(body.AddedAt));
    }

    [Fact]
    public async Task Post_ThenGet_AddedSubscriptionAppearsInList()
    {
        var factory = new WebApplicationFactory<Program>();
        var client = factory.CreateClient();
        const string url = "https://example.com/integration-test-feed/";

        await client.PostAsJsonAsync("/api/subscriptions", new { url });

        var response = await client.GetAsync("/api/subscriptions");
        var list = await response.Content.ReadFromJsonAsync<List<Subscription>>();

        Assert.NotNull(list);
        Assert.Contains(list, s => s.Url == url);
    }

    [Fact]
    public async Task Post_EmptyUrl_Returns400()
    {
        var response = await _client.PostAsJsonAsync("/api/subscriptions", new { url = "" });

        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }

    [Fact]
    public async Task Post_InvalidUrl_Returns400()
    {
        var response = await _client.PostAsJsonAsync("/api/subscriptions", new { url = "not-a-url" });

        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }
}
