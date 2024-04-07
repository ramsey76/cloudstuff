namespace SQLWriter.Models;

public class Bank
{
    public int Id {get;set;}
    public required string Name {get;set;}

    public 
    ICollection<BankAccount> BankAccounts {get;set;} = [];
}
