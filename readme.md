# Sample wire mock

Code that demostrates how to host a wiremock server inside a docker network, this allows:

* Testing on build servers
* Deployment of services for test into isolated environments
* More realistic unit test code.

## Demo
**Run from the root of the solution.**
```shell
docker compose up --force-recreate --build --always-recreate-deps
```
The server faking the responses [Mockserver](http://localhost:5100/__admin/mappings). An [API](http://localhost:5000) that is calling the mock server.

## Unit tests

A sample test suit boots the api, and then uses the mock engine to setup the requests. It's the same engine used in both the mock host so there's no duplication of setup,
the main different is a port is set.

When the api is booted it's setup in **isolated** mode using an different asp.net core environment.
```csharp
// apifactory.cs
protected override void ConfigureWebHost(IWebHostBuilder builder)
{
    builder.UseEnvironment("Isolated");
    builder.ConfigureTestServices(services =>
    {
    });
}
```
If you wanted to test the service code, you could do it using this technique as well.

## Points of interest

* Test startup - apifactory.cs.
* StubServer.Engine - shared api defs.
* StubServer.Host - used for booting the engine when running locally and also in the docker network.
* SuperImportant service is just for a typed client with HttpClientFactory.

## Food for thought

* Using WithCallback with wiremock.net you could simulate failures as it's evaluated on every call. Good for wrapping resilience around with polly.

