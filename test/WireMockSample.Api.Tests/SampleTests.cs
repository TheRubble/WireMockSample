using System.Net.Http.Json;
using FluentAssertions;
using WireMockSample.Api.Models;

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
        
        //  Act
        var response = await _client.GetAsync("api/get");
        var result = await response.Content.ReadFromJsonAsync<PostData>();
        
        // Assert

        for (int i = 0; i < 5; i++)
        {
            if (_apiFactory.WebHookDispatcher.ReceivedIds.Contains(result.Id.ToString()))
            {
                return;
            }

            await Task.Delay(1000);
        }

        throw new Exception("Didn't line up");

    }
}