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
    
    /// <summary>
    /// An httpclient to the place that needs a callback.
    /// </summary>
    /// <param name="client"></param>
    public void Setup(HttpClient client)
    {
        
        // This fakes an outbound call with a webhook callback
        _server
            .Given(
                Request.Create().WithPath("/drc/getratingid/*").UsingGet()
            )
            .RespondWith(
                Response.Create()
                    //
                    // .WithRandomDelay(500,1000)
                    // .WithStatusCode(200)
                    // .WithHeader("Content-Type", "application/json")
                    // .WithBodyAsJson(new
                    // {
                    //     name = "chriss",
                    //     lastseen = DateTime.Now.Subtract(TimeSpan.FromMinutes(30))
                    // }
                    .WithCallback(x =>
                    {
                        // Id from the query string.
                        var idSegment = x.AbsolutePathSegments[2];
                        
                        Task.Run(async () =>
                        {
                            await Task.Delay(100);
                            // I'm just simulating here.
                            await client.GetAsync($"/callback/{idSegment}");
                        });

                        var resp = new ResponseMessage
                        {
                            StatusCode = 200
                        };

                        return resp;
                    })
            );
        
        _server.Given(Request.Create().WithPath("/json").UsingGet()).RespondWith(Response.Create().WithBodyAsJson(new
        {
            firstname = "chriss"
        }));
        
    }
    
}