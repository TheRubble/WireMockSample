using System.Net.Http.Json;
using FluentAssertions;

namespace WireMockSample.Api.Tests;

[Collection("SharedTestCollection")]
public class SampleTests 
{
    private readonly ApiFactory _apiFactory;
    private readonly HttpClient _client;

    public SampleTests(ApiFactory apiFactory)
    {
        _apiFactory = apiFactory;
        _client = apiFactory.CreateClient();
    }

    [Fact]
    public async Task Given_a_simple_request_when_executed_a_simple_response_is_returned()
    {
        // Arrange 
        var id = Guid.NewGuid();
        
        // Act
        var response = await _client.GetAsync("/");
        var result = await response.Content.ReadAsStringAsync();
        
        // Assert
        result.Should().Contain("chriss");
    }
    
    [Fact]
    public async Task whatever()
    {
        // Arrange 
        var id = Guid.NewGuid();
        
        // Act
        var response = await _client.PostAsync("/", JsonContent.Create(new {
            emailAddress =  id.ToString()
        }));

        var location = response.Headers.Location.Should().NotBeNull();
        // Assert
    }
}