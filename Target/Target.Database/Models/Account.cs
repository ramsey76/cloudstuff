using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Target.Database.Models
{
    public class Account
    {
        public required Guid Id { get; set; }
        [Column(TypeName = "nvarchar(50)")]
        public required string AccountNumber { get; set; }

        [Column(TypeName = "decimal(18, 2)")]
        public decimal Balance { get; set; }

        public int BankAccountType { get; set; }

        public required Guid ExternalId { get; set; }
        public Delivery? Delivery { get; set; }  
        public Guid DeliveryId { get; set; }
        public ICollection<DepositorAccount> DepositorAccounts { get; set; } = [];
        public ICollection<Depositor> Depositors { get; set; } = [];
    }
}
