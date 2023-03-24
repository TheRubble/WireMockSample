using Ductus.FluentDocker.Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using StubServer.Engine;
using WireMock.Server;
using WireMockSample.Api.Dispatcher;

namespace WireMockSample.Api.Tests;

public class ApiFactory : WebApplicationFactory<IApiMarker>, IAsyncLifetime
{
    private static ICompositeService? _service;
    private static WireMockServer _wireMockServer;
    private Configuration _configuration;
    
    
    protected HttpClient Client { get; private set; }
    public WebHookDispatcherInterceptor WebHookDispatcher { get; private set; }
    
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        this.WebHookDispatcher = new WebHookDispatcherInterceptor();
        builder.UseEnvironment("Isolated");
        builder.ConfigureTestServices(services =>
        {
            services.RemoveAll(typeof(IWebHookDispatcher));
            services.AddSingleton<IWebHookDispatcher>(provider => WebHookDispatcher);
        });
    }

    
    public Task InitializeAsync()
    {
        _wireMockServer = WireMockServer.StartWithAdminInterface(57308);
        _configuration = new Configuration(_wireMockServer);

        Client = this.CreateClient();
        
        _configuration.Setup(Client);
        
        
        
        return Task.CompletedTask;
    }

    public new Task DisposeAsync()
    {
        _wireMockServer.Stop();
        return Task.CompletedTask;
    }
}