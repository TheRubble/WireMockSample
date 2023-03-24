namespace SuperImportant.Service;

public class SuperImportantService
{
    private readonly HttpClient _client;

    public SuperImportantService(HttpClient client)
    {
        _client = client;
    }

    public async Task<string> GetImportantStuffAsync(Guid id, CancellationToken cancellationToken)
    {
        var result = await _client.GetAsync($"/drc/getratingid/{id}");
        return await result.Content.ReadAsStringAsync(cancellationToken);
    }
}