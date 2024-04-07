using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Azure.Core;

namespace Target.Database.Models;

public class Institution
{
    public required Guid Id { get; set; }
    
    [Column(TypeName = "nvarchar(100)")]
    public required string Name { get; set; }

    public required int ExternalId { get; set; }

    public ICollection<Delivery> Deliveries { get; set; } = [];
}
