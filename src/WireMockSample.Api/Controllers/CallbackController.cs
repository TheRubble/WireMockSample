using Microsoft.AspNetCore.Mvc;
using WireMockSample.Api.Dispatcher;

namespace WireMockSample.Api.Controllers;

public class CallbackController : ControllerBase
{
    private readonly IWebHookDispatcher _webHookDispatcher;

    public CallbackController(IWebHookDispatcher webHookDispatcher)
    {
        _webHookDispatcher = webHookDispatcher;
    }
    
    [HttpGet("callback/{id}")]
    public async Task<IActionResult> Index(string id, CancellationToken cancellationToken)
    {
        _webHookDispatcher.SendToClient(id);
       // var result = await _client.GetImportantStuffAsync(cancellationToken);
        return Ok("yay");
    } 
}