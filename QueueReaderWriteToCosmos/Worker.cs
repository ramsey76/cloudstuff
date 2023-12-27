using System.Text.Json;
using Microsoft.Extensions.Options;
using QueueReaderWriteToCosmos.Config;
using QueueReaderWriteToCosmos.Models;
using Azure.Messaging.ServiceBus;
using QueueReaderWriteToCosmos.Access.CosmoDB;

namespace QueueReaderWriteToCosmos;

public class Worker : BackgroundService, IDisposable
{
    private readonly ILogger<Worker> _logger;
    private readonly MessageBusSettings _messageBusSettings;
    private ServiceBusClient _serviceBusClient;
    private ServiceBusProcessor _serviceBusProcessor;
    private CosmosWriter _cosmosWriter;
    private Guid _bankStreamId = Guid.NewGuid();

    public Worker(ILogger<Worker> logger, IOptions<MessageBusSettings> messageBusSettings, CosmosWriter cosmoWriter)
    {
        _logger = logger;
        _messageBusSettings = messageBusSettings.Value;
        _cosmosWriter = cosmoWriter;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var options = new ServiceBusProcessorOptions 
        {
            AutoCompleteMessages = false,
            MaxConcurrentCalls = 20
        };
        
        _serviceBusClient = new ServiceBusClient(_messageBusSettings.ConnectionString);
        _serviceBusProcessor = _serviceBusClient.CreateProcessor(_messageBusSettings.QueueName, options);

        _serviceBusProcessor.ProcessMessageAsync += MessageHandler;
        _serviceBusProcessor.ProcessErrorAsync += ErrorHandler;

        // start processing 
        await _serviceBusProcessor.StartProcessingAsync().ConfigureAwait(false);
    }

    public override async Task StopAsync(CancellationToken cancellationToken)
    {
        await _serviceBusProcessor.StopProcessingAsync().ConfigureAwait(false);
        
        await this._serviceBusClient.DisposeAsync().ConfigureAwait(false);
        await this._serviceBusProcessor.DisposeAsync().ConfigureAwait(false);
    }
    
    // handle received messages
    private async Task MessageHandler(ProcessMessageEventArgs args)
    {
        var body = args.Message.Body.ToString();
        Console.WriteLine($"Received: {body}");

        var deposant = JsonSerializer.Deserialize<Deposant>(body);
        deposant.BankStreamId = _bankStreamId;

        await _cosmosWriter.AddItem(deposant).ConfigureAwait(false);
        
        // complete the message. message is deleted from the queue. 
        await args.CompleteMessageAsync(args.Message);
    }

    // handle any errors when receiving messages
    private Task ErrorHandler(ProcessErrorEventArgs args)
    {
        Console.WriteLine(args.Exception.ToString());
        return Task.CompletedTask;
    }
    
}
