using System.Diagnostics;
using Azure.Storage.Queues;
using Microsoft.AspNetCore.Mvc;
using SuperImportant.Service;
using WireMockSample.Api.Models;

namespace WireMockSample.Api.Controllers;

public class HomeController : ControllerBase
{
    private readonly ILogger<HomeController> _logger;
    private readonly SuperImportantService _client;

    public HomeController(ILogger<HomeController> logger, SuperImportantService client)
    {
        _logger = logger;
        _client = client;
    }

    [HttpGet("api/get")]
    public async Task<IActionResult> Index(CancellationToken cancellationToken)
    {
        // Put on queue here and send back a accepted with a location header.

        // var queueServiceClient = new QueueServiceClient(
        //     "DefaultEndpointsProtocol=https;AccountName=devstoreaccount1;AccountKey=Eby8vdM02xNOcqFlqUwJPLlmEtlCDXJ1OUzFT50uSRZ6IFsuFq2UVErCz4I6tq/K1SZFPTOtr/KBHBeksoGMGw==;BlobEndpoint=http://127.0.0.1:10000/devstoreaccount1;QueueEndpoint=http://127.0.0.1:10001/devstoreaccount1;TableEndpoint=http://127.0.0.1:10002/devstoreaccount1;");
        //
        // //var queue  = await queueServiceClient.CreateQueueAsync("incoming", cancellationToken: cancellationToken);
        // var qc = queueServiceClient.GetQueueClient("incoming");
        // _ = await qc.CreateIfNotExistsAsync(cancellationToken: cancellationToken);
        //
        var id = Guid.NewGuid();
        // await qc.SendMessageAsync(id.ToString(), cancellationToken);
        //

        
        var result = await _client.GetImportantStuffAsync(id, cancellationToken);
        return AcceptedAtAction("Index", new { id }, value: new PostData
        {
            Id = id,
        });
    }

    // public async Task<IActionResult> Get(Guid id, CancellationToken cancellationToken)
    // {
    //     return null;
    // }
 
}