
using StubServer.Engine;
using WireMock.Server;
using WireMock.Settings;

Console.WriteLine("Starting Server");
using var server = WireMockServer.Start(new WireMockServerSettings
{
    StartAdminInterface = true,
    Port = 57308
});

Console.WriteLine(server.Url);

var configuration = new Configuration(server);
configuration.Setup();

Console.WriteLine("Server Started");
Console.WriteLine("Press a key to exit");

// Not recommended you should use IHostedService - it's good enough for my testing etc.
await Task.Run(() => Thread.Sleep(Timeout.Infinite));
