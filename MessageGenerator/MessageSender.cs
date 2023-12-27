using MessageGenerator.Model;
using MessageGenerator.ResourceAccess.ServiceBus;
using Microsoft.Extensions.Options;
using System.Text.Json;
using System.Linq;

namespace MessageGenerator;

public class MessageSender
{
    private readonly ServiceBus _serviceBus;
    
    public MessageSender(ServiceBus serviceBus)
    {
        this._serviceBus = serviceBus;
    }

    public Task SendAsync(Deposant deposant)
    {
        var deposanten = new List<Deposant>{deposant};
        return SendBatchAsync(deposanten);
    }

    public Task SendBatchAsync(List<Deposant> deposanten)
    {
        var messages = new List<string>();
        deposanten.ForEach(deposant => messages.Add(JsonSerializer.Serialize(deposant)));
        return this._serviceBus.SendAsync(messages);
    }
}