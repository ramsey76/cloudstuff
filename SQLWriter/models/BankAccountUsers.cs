namespace SQLWriter.Models;

public class BankAccountUser
{
        public int Id {get;set;}
        public int BankAccountId {get;set;}
        public BankAccount BankAccount {get;set;}
        public Guid UserId {get;set;}
        public required User User {get;set;}
}
