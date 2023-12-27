using System.Text.Json.Serialization;

namespace QueueReaderWriteToCosmos.Models;

public class Item
{
    [JsonPropertyName("Id")]
    public string id { get; set; }
    //public string Partitionkey { get; set; }
    
}