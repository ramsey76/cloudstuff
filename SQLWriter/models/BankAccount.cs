using System.ComponentModel.DataAnnotations.Schema;

namespace SQLWriter.Models
{
    public enum BankAccountType {
            Current,
            Savings,
            Mortgage
    }

    public class BankAccount
    {
            public int Id {get;set;}
            public Bank Bank {get;set;}
            public int BankId {get;set;}
            public ICollection<User> Users {get;set;} = [];
            public ICollection<BankAccountUser> BankAccountUsers {get;set;} = [];
            
            public string BankAccountNumber {get;set;}
            [Column(TypeName = "decimal(18, 2)")]
            public Decimal CurrentAmount {get;set;}    
            public BankAccountType Type {get;set;}
        
    }
}
