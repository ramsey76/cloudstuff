using Microsoft.Azure.Cosmos;
using Microsoft.Extensions.Options;
using QueueReaderWriteToCosmos.Config;
using QueueReaderWriteToCosmos.Models;

namespace QueueReaderWriteToCosmos.Access.CosmoDB;

public class CosmosWriter
{
    private CosmosDBSettings _cosmosDBSettings;
    private readonly CosmosClient _client;
    private readonly Container _container;

    public CosmosWriter(IOptions<CosmosDBSettings> cosmosDBSettings)
    {
        ArgumentNullException.ThrowIfNull(cosmosDBSettings);
        
        _cosmosDBSettings = cosmosDBSettings.Value;
        _client = new CosmosClient(_cosmosDBSettings.Uri, _cosmosDBSettings.Key);
        _container = _client.GetContainer("Bank", "Deposant");
    }
    
    public Task AddItem<T>(T item) where T : Item
    {
        return _container.CreateItemAsync<T>(item);
    }
    
}