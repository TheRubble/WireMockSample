namespace SuperImportant.Service;

public class SuperImportantService
{
    private readonly HttpClient _client;

    public SuperImportantService(HttpClient client)
    {
        _client = client;
    }

    public async Task<string> GetImportantStuffAsync(CancellationToken cancellationToken)
    {
        var result = await _client.GetAsync("some/thing");
        return await result.Content.ReadAsStringAsync(cancellationToken);
    }
}