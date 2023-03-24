using Azure.Storage.Queues;
using Azure.Storage.Queues.Models;

namespace WireMockSample.Api.QueueListener.Step2;

public class ProcessServiceResponses : IHostedService, IDisposable
{
    private int executionCount = 0;
    private readonly ILogger<ProcessServiceResponses> _logger;
    private Timer? _timer = null;

    public ProcessServiceResponses(ILogger<ProcessServiceResponses> logger)
    {
        _logger = logger;
    }

    public async Task StartAsync(CancellationToken stoppingToken)
    {
        var queueServiceClient = new QueueServiceClient(
            "DefaultEndpointsProtocol=https;AccountName=devstoreaccount1;AccountKey=Eby8vdM02xNOcqFlqUwJPLlmEtlCDXJ1OUzFT50uSRZ6IFsuFq2UVErCz4I6tq/K1SZFPTOtr/KBHBeksoGMGw==;BlobEndpoint=http://127.0.0.1:10000/devstoreaccount1;QueueEndpoint=http://127.0.0.1:10001/devstoreaccount1;TableEndpoint=http://127.0.0.1:10002/devstoreaccount1;");

        //var queue  = await queueServiceClient.CreateQueueAsync("incoming", cancellationToken: cancellationToken);
        var qc = queueServiceClient.GetQueueClient("incoming");

        // Get messages from the queue
        QueueMessage[] messages = await qc.ReceiveMessagesAsync(maxMessages: 10);
        foreach (QueueMessage message in messages)
        {
            // "Process" the message
            Console.WriteLine($"Message: {message.MessageText}");

            // Let the service know we're finished with
            // the message and it can be safely deleted.
            await qc.DeleteMessageAsync(message.MessageId, message.PopReceipt);
        }
    }

    private void DoWork(object? state)
    {
        var count = Interlocked.Increment(ref executionCount);

        _logger.LogInformation(
            "Timed Hosted Service is working. Count: {Count}", count);
    }

    public Task StopAsync(CancellationToken stoppingToken)
    {
        _logger.LogInformation("Timed Hosted Service is stopping.");

        _timer?.Change(Timeout.Infinite, 0);

        return Task.CompletedTask;
    }

    public void Dispose()
    {
        _timer?.Dispose();
    }
}