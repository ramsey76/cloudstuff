using System.ComponentModel.DataAnnotations;

namespace SQLWriter.Models;

public enum BankAccountType {
        Current,
        Savings,
        Mortgage
}

public class BankAccount
{
        public int Id {get;set;}
        public string AccountNumber {get;set;}
        public int BankId {get;set;}
        public Bank Bank {get;set;}
        public Decimal CurrentAmount {get;set;}    
        public Decimal DollarAmount {get;set;}
        public BankAccountType Type {get;set;}

        public List<BankAccountUser> BankAccountUsers {get;set;}
}
