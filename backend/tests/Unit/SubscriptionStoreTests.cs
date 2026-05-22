using RSSFeedReader.Api.Services;

namespace RSSFeedReader.Api.Tests;

public class SubscriptionStoreTests
{
    [Fact]
    public void List_WhenEmpty_ReturnsEmptyCollection()
    {
        var store = new SubscriptionStore();

        var result = store.List();

        Assert.Empty(result);
    }

    [Fact]
    public void Add_ValidUrl_ReturnsSubscriptionWithNonEmptyId()
    {
        var store = new SubscriptionStore();

        var subscription = store.Add("https://example.com/feed");

        Assert.NotNull(subscription);
        Assert.False(string.IsNullOrEmpty(subscription.Id));
    }

    [Fact]
    public void Add_ValidUrl_StoresTheUrl()
    {
        var store = new SubscriptionStore();
        const string url = "https://example.com/feed";

        var subscription = store.Add(url);

        Assert.Equal(url, subscription.Url);
    }

    [Fact]
    public void Add_ValidUrl_StoresAddedAtTimestamp()
    {
        var store = new SubscriptionStore();

        var subscription = store.Add("https://example.com/feed");

        Assert.False(string.IsNullOrEmpty(subscription.AddedAt));
        Assert.True(DateTime.TryParse(subscription.AddedAt, out _));
    }

    [Fact]
    public void List_AfterAddingOne_ReturnsOneItem()
    {
        var store = new SubscriptionStore();
        store.Add("https://example.com/feed");

        var result = store.List();

        Assert.Single(result);
    }

    [Fact]
    public void List_AfterAddingMultiple_ReturnsAllItems()
    {
        var store = new SubscriptionStore();
        store.Add("https://example.com/feed1");
        store.Add("https://example.com/feed2");
        store.Add("https://example.com/feed3");

        var result = store.List();

        Assert.Equal(3, result.Count);
    }

    [Fact]
    public void Add_TwoUrls_GeneratesUniqueIds()
    {
        var store = new SubscriptionStore();

        var first = store.Add("https://example.com/feed1");
        var second = store.Add("https://example.com/feed2");

        Assert.NotEqual(first.Id, second.Id);
    }

    [Fact]
    public void List_ReturnsReadOnlyView_OfStoredSubscriptions()
    {
        var store = new SubscriptionStore();
        store.Add("https://example.com/feed");

        var list = store.List();

        Assert.IsAssignableFrom<IReadOnlyList<RSSFeedReader.Api.Models.Subscription>>(list);
    }
}
