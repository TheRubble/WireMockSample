namespace WireMockSample.Api.Dispatcher;

public interface IWebHookDispatcher
{
    void SendToClient(string id);
}

public class WebHook : IWebHookDispatcher
{
    public void SendToClient(string id)
    {
        
    }
}