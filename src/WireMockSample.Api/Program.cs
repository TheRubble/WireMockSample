using SuperImportant.Service;
using WireMockSample.Api.Dispatcher;
using WireMockSample.Api.QueueListener.Step1;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
//builder.Services.AddControllersWithViews();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddHttpClient<SuperImportantService>(x =>
{
    x.BaseAddress = new Uri(builder.Configuration["SomeServiceUrl"]);
});

builder.Services.AddHostedService<ProcessIncomingRequests>();
builder.Services.AddSingleton<IWebHookDispatcher, WebHook>();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseSwagger();
app.UseSwaggerUI();

app.MapControllers();

app.Run();

public interface IApiMarker
{
}