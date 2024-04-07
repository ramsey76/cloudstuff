namespace Target.Database.Models;

public class Depositor
{
        public Guid Id { get; set; }
        public required string Name { get; set; }
        public Delivery? Delivery { get; set; }  
        public Guid DeliveryId { get; set; }
        public ICollection<DepositorAccount> DepositorAccounts { get; set; } = [];
        public ICollection<Account> Accounts { get; set; } = [];
}
