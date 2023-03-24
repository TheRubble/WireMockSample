using WireMockSample.Api.Dispatcher;

namespace WireMockSample.Api.Tests;

public class WebHookDispatcherInterceptor : IWebHookDispatcher
{

    private object _lock = new();
    public HashSet<string> ReceivedIds { get; } = new HashSet<string>();

    public void SendToClient(string id)
    {
        lock (_lock)
        {
            ReceivedIds.Add(id);
        }
    }
}