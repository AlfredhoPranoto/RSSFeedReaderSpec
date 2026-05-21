using RSSFeedReader.Api.Models;

namespace RSSFeedReader.Api.Services;

public class SubscriptionStore
{
    private readonly List<Subscription> _subscriptions = new();

    public Subscription Add(string url)
    {
        var subscription = new Subscription
        {
            Id = Guid.NewGuid().ToString(),
            Url = url,
            AddedAt = DateTime.UtcNow.ToString("o")
        };
        _subscriptions.Add(subscription);
        return subscription;
    }

    public IReadOnlyList<Subscription> List() => _subscriptions.AsReadOnly();
}
