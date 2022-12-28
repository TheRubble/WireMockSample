using Ductus.FluentDocker.Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using StubServer.Engine;
using WireMock.Server;

namespace WireMockSample.Api.Tests;

public class ApiFactory : WebApplicationFactory<IApiMarker>, IAsyncLifetime
{
    private static ICompositeService? _service;
    private static WireMockServer _wireMockServer;
    private Configuration _configuration;
    
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.UseEnvironment("Isolated");
        builder.ConfigureTestServices(services =>
        {
        });
    }

    
    public Task InitializeAsync()
    {
        _wireMockServer = WireMockServer.StartWithAdminInterface(57308);
        _configuration = new Configuration(_wireMockServer);
        _configuration.Setup();
        return Task.CompletedTask;
    }

    public new Task DisposeAsync()
    {
        _wireMockServer.Stop();
        return Task.CompletedTask;
    }
}