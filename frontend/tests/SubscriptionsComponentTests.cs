using Bunit;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using RSSFeedReader.UI.Models;
using RSSFeedReader.UI.Pages;
using RSSFeedReader.UI.Services;

namespace RSSFeedReader.UI.Tests;

public class SubscriptionsComponentTests : TestContext
{
    private Mock<SubscriptionApiService> CreateMockService(
        List<SubscriptionDto>? initial = null,
        SubscriptionDto? addResult = null)
    {
        var mock = new Mock<SubscriptionApiService>(Mock.Of<HttpClient>());
        mock.Setup(s => s.GetSubscriptionsAsync())
            .ReturnsAsync(initial ?? new List<SubscriptionDto>());
        mock.Setup(s => s.AddSubscriptionAsync(It.IsAny<string>()))
            .ReturnsAsync(addResult ?? new SubscriptionDto
            {
                Id = Guid.NewGuid().ToString(),
                Url = "https://example.com/feed",
                AddedAt = DateTime.UtcNow.ToString("o")
            });
        return mock;
    }

    [Fact]
    public void Page_WhenLoaded_ShowsSubscriptionsHeading()
    {
        var mock = CreateMockService();
        Services.AddScoped(_ => mock.Object);

        var cut = RenderComponent<Subscriptions>();

        cut.WaitForState(() => !cut.Find("h1").TextContent.Equals(string.Empty));
        Assert.Contains("Subscriptions", cut.Find("h1").TextContent);
    }

    [Fact]
    public void Page_WhenEmpty_ShowsEmptyMessage()
    {
        var mock = CreateMockService(initial: new List<SubscriptionDto>());
        Services.AddScoped(_ => mock.Object);

        var cut = RenderComponent<Subscriptions>();

        cut.WaitForState(() => cut.Markup.Contains("No subscriptions"));
        Assert.Contains("No subscriptions", cut.Markup);
    }

    [Fact]
    public void Page_WithExistingSubscriptions_DisplaysThem()
    {
        var existing = new List<SubscriptionDto>
        {
            new() { Id = "1", Url = "https://devblogs.microsoft.com/dotnet/feed/", AddedAt = DateTime.UtcNow.ToString("o") }
        };
        var mock = CreateMockService(initial: existing);
        Services.AddScoped(_ => mock.Object);

        var cut = RenderComponent<Subscriptions>();

        cut.WaitForState(() => cut.Markup.Contains("devblogs.microsoft.com"));
        Assert.Contains("devblogs.microsoft.com", cut.Markup);
    }

    [Fact]
    public void AddButton_WhenInputIsEmpty_IsDisabled()
    {
        var mock = CreateMockService();
        Services.AddScoped(_ => mock.Object);

        var cut = RenderComponent<Subscriptions>();

        cut.WaitForState(() => !cut.Markup.Contains("Loading"));
        var button = cut.Find("button");
        Assert.True(button.HasAttribute("disabled"));
    }

    [Fact]
    public void AddButton_WhenInputHasValidUrl_IsEnabled()
    {
        var mock = CreateMockService();
        Services.AddScoped(_ => mock.Object);

        var cut = RenderComponent<Subscriptions>();

        cut.WaitForState(() => !cut.Markup.Contains("Loading"));
        cut.Find("input").Input("https://example.com/feed");

        var button = cut.Find("button");
        Assert.False(button.HasAttribute("disabled"));
    }

    [Fact]
    public void AddButton_WhenInputIsInvalidUrl_IsDisabled()
    {
        var mock = CreateMockService();
        Services.AddScoped(_ => mock.Object);

        var cut = RenderComponent<Subscriptions>();

        cut.WaitForState(() => !cut.Markup.Contains("Loading"));
        cut.Find("input").Input("not-a-url");

        var button = cut.Find("button");
        Assert.True(button.HasAttribute("disabled"));
    }

    [Fact]
    public async Task ClickingAdd_WithValidUrl_AddsItemToList()
    {
        var added = new SubscriptionDto
        {
            Id = "new-1",
            Url = "https://example.com/feed",
            AddedAt = DateTime.UtcNow.ToString("o")
        };
        var mock = CreateMockService(addResult: added);
        Services.AddScoped(_ => mock.Object);

        var cut = RenderComponent<Subscriptions>();
        cut.WaitForState(() => !cut.Markup.Contains("Loading"));

        cut.Find("input").Input("https://example.com/feed");
        await cut.Find("button").ClickAsync(new Microsoft.AspNetCore.Components.Web.MouseEventArgs());

        cut.WaitForState(() => cut.Markup.Contains("example.com/feed"));
        Assert.Contains("example.com/feed", cut.Markup);
    }

    [Fact]
    public async Task ClickingAdd_WithValidUrl_ClearsInput()
    {
        var mock = CreateMockService();
        Services.AddScoped(_ => mock.Object);

        var cut = RenderComponent<Subscriptions>();
        cut.WaitForState(() => !cut.Markup.Contains("Loading"));

        cut.Find("input").Input("https://example.com/feed");
        await cut.Find("button").ClickAsync(new Microsoft.AspNetCore.Components.Web.MouseEventArgs());

        cut.WaitForState(() => cut.Find("input").GetAttribute("value") == null
                                || cut.Find("input").GetAttribute("value") == string.Empty);
        Assert.True(string.IsNullOrEmpty(cut.Find("input").GetAttribute("value")));
    }
}
