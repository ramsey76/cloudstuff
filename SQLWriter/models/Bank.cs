namespace SQLWriter.Models;

public class Bank
{
    public Guid Id {get;set;}
    public required string Name {get;set;}

    public required ICollection<BankAccount> BankAccounts {get;set;}
}
