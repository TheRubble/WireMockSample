using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using SuperImportant.Service;
using WireMockSample.Api.Models;

namespace WireMockSample.Api.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly SuperImportantService _client;

    public HomeController(ILogger<HomeController> logger, SuperImportantService client)
    {
        _logger = logger;
        _client = client;
    }

    public async Task<IActionResult> Index(CancellationToken cancellationToken)
    {
        var result = await _client.GetImportantStuffAsync(cancellationToken);
        return Ok(result);
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}