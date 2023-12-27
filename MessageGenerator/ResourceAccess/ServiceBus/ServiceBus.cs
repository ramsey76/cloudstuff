using System.Runtime.CompilerServices;
using Azure.Messaging.ServiceBus;
using MessageGenerator.Model;
using Microsoft.Extensions.Options;
using Microsoft.FSharp.Control;
using Polly;

namespace MessageGenerator.ResourceAccess.ServiceBus;

public class ServiceBus
{
    private IOptions<MessageBusSettings> settings;
    private readonly ServiceBusSender _sender;
    private readonly ServiceBusClient _client;
    private readonly AsyncPolicy _retry;
    
    public ServiceBus(IOptions<MessageBusSettings> settings)
    {
        this.settings = settings;

        this._client = new ServiceBusClient(settings.Value.ConnectionString);
        this._sender = _client.CreateSender(settings.Value.QueueName);
        _retry = Policy
            .Handle<System.TimeoutException>()
            .WaitAndRetryAsync(3, retry => TimeSpan.FromSeconds(2));
    }

    public async Task SendAsync(IEnumerable<string> messages)
    {
        try
        {
            var batch = await _sender.CreateMessageBatchAsync().ConfigureAwait(false);
            var i = 0;
            foreach (var message in messages.ToList())
            {
                i++;
                if (batch.TryAddMessage(new ServiceBusMessage(message)) && i < 101) continue;
                
                i = 0;
                await SendMessages(batch).ConfigureAwait(false);
                batch = await _sender.CreateMessageBatchAsync().ConfigureAwait(false);
                batch.TryAddMessage(new ServiceBusMessage(message));
            }
            await SendMessages(batch);
        }
        finally
        {
            await _client.DisposeAsync().ConfigureAwait(false);
            await _sender.DisposeAsync().ConfigureAwait(false);
        }
    }

    private async Task SendMessages(ServiceBusMessageBatch batch)
    {
        if (batch.Count > 0)
        {
            await _retry.ExecuteAsync(async () =>
            {
                Console.WriteLine(batch.SizeInBytes);
                await _sender.SendMessagesAsync(batch).ConfigureAwait(false);
            }).ConfigureAwait(false);
        }
    }


}