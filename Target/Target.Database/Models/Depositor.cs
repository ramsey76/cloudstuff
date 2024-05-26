namespace Target.Database.Models;

public class Depositor
{
        public Guid Id { get; set; }
        public Institution? Institution { get; set; }  
        public Guid InstitutionId { get; set; }
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public ICollection<DepositorAccount> DepositorAccounts { get; set; } = [];
        public ICollection<Account> Accounts { get; set; } = [];
}
