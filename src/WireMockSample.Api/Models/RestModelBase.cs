namespace WireMockSample.Api.Models;

public abstract class RestModelBase
{
    public List<Link> Links { get; set; } = new();
}