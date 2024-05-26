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

        public required int ExternalId { get; set; }
        public Institution? Institution { get; set; }  
        public Guid InstitutionId { get; set; }
        public ICollection<DepositorAccount> DepositorAccounts { get; set; } = [];
        public ICollection<Depositor> Depositors { get; set; } = [];
    }
}
