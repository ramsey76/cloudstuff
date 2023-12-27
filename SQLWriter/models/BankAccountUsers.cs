namespace SQLWriter.Models;

public class BankAccountUsers
{
        public Guid BankAccountId {get;set;}
        public required BankAccount BankAccount {get;set;}
        public Guid UserId {get;set;}
        public required User User {get;set;}
}
