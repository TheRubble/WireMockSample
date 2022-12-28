using WireMock;
using WireMock.RequestBuilders;
using WireMock.ResponseBuilders;
using WireMock.Server;
using WireMock.Types;

namespace StubServer.Engine;

public class Configuration
{
    private readonly WireMockServer _server;

    private string? Url => _server.Url;
    
    public Configuration(WireMockServer server)
    {
        _server = server;
    }
    
    public void Setup()
    {
        _server
            .Given(
                Request.Create().WithPath("/some/thing").UsingGet()
            )
            .RespondWith(
                Response.Create()
                    .WithRandomDelay(500,1000)
                    .WithStatusCode(200)
                    .WithHeader("Content-Type", "application/json")
                    .WithBodyAsJson(new
                    {
                        name = "chriss",
                        lastseen = DateTime.Now.Subtract(TimeSpan.FromMinutes(30))
                    })
            );
    }
    
}