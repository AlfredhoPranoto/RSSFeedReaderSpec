using Microsoft.AspNetCore.Mvc;
using RSSFeedReader.Api.Models;
using RSSFeedReader.Api.Services;

namespace RSSFeedReader.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SubscriptionsController : ControllerBase
{
    private readonly SubscriptionStore _store;

    public SubscriptionsController(SubscriptionStore store)
    {
        _store = store;
    }

    [HttpGet]
    public ActionResult<IReadOnlyList<Subscription>> GetAll()
    {
        return Ok(_store.List());
    }

    [HttpPost]
    public ActionResult<Subscription> Add([FromBody] AddSubscriptionRequest request)
    {
        if (string.IsNullOrWhiteSpace(request?.Url))
        {
            return BadRequest(new { error = "url must not be empty." });
        }

        if (!Uri.TryCreate(request.Url, UriKind.Absolute, out var uri) ||
            (uri.Scheme != Uri.UriSchemeHttp && uri.Scheme != Uri.UriSchemeHttps))
        {
            return BadRequest(new { error = "url must be a valid http or https URL." });
        }

        var subscription = _store.Add(request.Url);
        return CreatedAtAction(nameof(GetAll), subscription);
    }
}

public class AddSubscriptionRequest
{
    public string Url { get; set; } = string.Empty;
}
